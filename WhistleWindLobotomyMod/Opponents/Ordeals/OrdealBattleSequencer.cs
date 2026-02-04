using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using EncounterBuilder = DiskCardGame.EncounterBuilder;

namespace WhistleWindLobotomyMod.Opponents {
    public abstract class OrdealBattleSequencer : LobotomyBattleSequencer {
        public override Opponent.Type BossType => OrdealUtils.OpponentID;
        public override StoryEvent DefeatedStoryEvent => LobotomyPlugin.OrdealDefeated;

        public List<string> ValidCards { get; protected set; } = new();
        public int MinNumCardsRequired { get; protected set; }
        public List<List<CardInfo>> EncounterBluePrint { get; protected set; }
        protected OrdealOpponent Opponent => TurnManager.Instance.Opponent as OrdealOpponent;

        public bool defeated = false;
        public OrdealType ordealType;
        public int ordealTier;
        public int amountKilledThisTurn = 0;
        protected int TotalExcessDamageDealt = 0;

        /// <summary>
        /// Abstract method for constructing the battle blueprint for the current Ordeal.
        /// </summary>
        /// <param name="encounterData">The EncounterData for the current Ordeal battle.</param>
        /// <param name="baseDifficulty">The base difficulty value without the DifficultyModifier. Can be 0.</param>
        /// <returns>The minimum number of Ordeal cards that must be killed to complete the Ordeal.</returns>
        public abstract int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty);

        /// <summary>
        /// Checks if the encounter has been completed defeated.
        /// </summary>
        /// <returns>True if no Ordeal cards remain and have all been killed.</returns>
        public virtual bool PlayerHasDefeatedOrdeal() {
            return defeated || OrdealCounterManager.Instance.amountLeft == 0;
        }

        /// <summary>
        /// Tracks excess damage dealt then calls the base DigUpBones method.
        /// </summary>
        public override void DigUpBones(int damage, int bonesToGive, CardSlot targetSlot) {
            TotalExcessDamageDealt += damage - bonesToGive;
            base.DigUpBones(damage, bonesToGive, targetSlot);
        }

        public IEnumerator UpdateOrdealMonitor(int amountKilled) {
            yield return HelperMethods.ChangeCurrentView(OrdealUtils.ViewCounter, endDelay: 0.4f);
            if (OrdealCounterManager.Instance.Dirty) {
                OrdealCounterManager.Instance.EnableConsole(false);
                yield return new WaitForSeconds(0.8f);
                OrdealCounterManager.Instance.UpdateConsole(ordealTier, OrdealCounterManager.Instance.amountLeft);
                OrdealCounterManager.Instance.EnableConsole(true);
                if (!defeated) {
                    yield return new WaitForSeconds(0.8f);
                }
            }
            yield return OrdealCounterManager.Instance.UpdateAmountLeft(amountKilled);
            yield return new WaitForSeconds(0.75f);
        }

        public override IEnumerator OnOpponentTurnEnd(bool opponentTurnSkipped) {
            //LobotomyPlugin.Log.LogDebug($"[OrdealBattle] OpponentTurnEnd skipped: {opponentTurnSkipped} | amountKilled: {amountKilledThisTurn}");
            if (amountKilledThisTurn != 0) {
                //LobotomyPlugin.Log.LogDebug($"[OrdealBattle] update amount left");
                if (amountKilledThisTurn > OrdealCounterManager.Instance.amountLeft) {
                    yield return UpdateOrdealMonitor(OrdealCounterManager.Instance.amountLeft);
                }
                else {
                    yield return UpdateOrdealMonitor(amountKilledThisTurn);
                }
            }

            amountKilledThisTurn = 0; // reset here so we can modify it in MoveOpponentCards (see Amber Dusk for ex)
            if (!defeated) {
                if (OrdealCounterManager.Instance.amountLeft == 0) {
                    DefeatOrdealAndDisplayOutroBanner();
                }
                else if (ShouldExtendBattle()) {
                    LobotomyPlugin.Log.LogDebug("[OrdealBattle] OpponentTurnEnd: Extend turn plan");
                    Opponent.ReplaceAndAppendTurnPlan(Opponent.ModifyTurnPlan(EncounterBluePrint));
                    yield return Opponent.QueueNewCards();
                }

                if (!opponentTurnSkipped) {
                    yield return MoveOpponentCards();
                }
            }
            ResetPerRoundVariables();
            //LobotomyPlugin.Log.LogDebug($"[OrdealBattle] OpponentTurnEnd: [{OrdealCounterManager.Instance.amountLeft}] left");

            // if we killed more Ordeal cards after they were moved
            if (amountKilledThisTurn != 0) {
                yield return OnOpponentTurnEnd(true);
            }
        }

        /// <summary>
        /// Checks if the battle should be extended with additional Ordeal cards.
        /// </summary>
        /// <returns>True if the player runs out of Ordeal cards before meeting the kill requirement.</returns>
        public virtual bool ShouldExtendBattle() {
            int numOfOrdeals = BoardManager.Instance.CardsOnBoard.Count(CardIsValidOrdeal) + Opponent.Queue.Count(CardIsValidOrdeal);
            return Opponent.NumTurnsTaken >= Opponent.TurnPlan.Count && numOfOrdeals < OrdealCounterManager.Instance.amountLeft;
        }

        /// <returns>True if the given card's death is counted towards the kill requirement.</returns>
        protected bool CardIsValidOrdeal(PlayableCard card) {
            //LobotomyPlugin.Log.LogInfo($"Ordeal: {card.HasTrait(LobotomyCardManager.Ordeal)} Valid: {ValidCards.Count == 0} || {ValidCards.Contains(card.Info.name)}");
            return card.HasTrait(LobotomyCardManager.Ordeal) && !card.IsCopycatImpostor() && (ValidCards.Count == 0 || ValidCards.Contains(card.Info.name));
        }

        /// <summary>
        /// Only valid Ordeal cards will trigger 'this.OnOtherCardDie'.
        /// By default, any card with the Ordeal trait is valid.
        /// </summary>
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) => fromCombat;

        /// <remarks>
        /// By default, only triggers when an opponent-owned Ordeal card dies.
        /// </remarks>
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            if (!CardIsValidOrdeal(card)) {
                if (card.OpponentCard) {
                    yield return DialogueHelper.PlayDialogueEvent("OrdealNonOrdealKilled");
                }
                yield break;
            }
            amountKilledThisTurn++;

            // Ordeal has been defeated
            if (!defeated && OrdealCounterManager.Instance.amountLeft - amountKilledThisTurn < 1) {
                DefeatOrdealAndDisplayOutroBanner();
            }

            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] OnOtherCardDie: dead card:[{card.Info.displayedName}] killer: {killer?.Info.name} total killed:[{amountKilledThisTurn}]");
            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Cards left: {OrdealCounterManager.Instance.amountLeft - amountKilledThisTurn}");
        }

        public virtual void DefeatOrdealAndDisplayOutroBanner() {
            defeated = true;
            OrdealBannerManager.Instance.UpdateBannerOutro(ordealType, ordealTier);
            OrdealBannerManager.Instance.DisplayBanner(ordealType, false);
        }

        /// <summary>
        /// If the player dealt excess damage, visualise the money gained.
        /// </summary>
        /// <returns></returns>
        public override IEnumerator PreCleanUp() {
            if (Opponent.GiveCurrencyOnDefeat && TotalExcessDamageDealt > 0 && TurnManager.Instance.PlayerIsWinner()) {
                ViewManager.Instance.SwitchToView(View.Default);
                RunState.Run.currency += TotalExcessDamageDealt;
                yield return Singleton<CombatPhaseManager>.Instance.VisualizeExcessLethalDamage(TotalExcessDamageDealt, this);
            }
        }

        public virtual void TryAddOrdealRandomBuff(PlayableCard card) {
            int rand = base.GetRandomSeed();
            if (SeededRandom.Value(rand++) <= Opponent.Difficulty * 0.02f) {
                if (SeededRandom.Value(rand) <= 0.2f + Opponent.Difficulty * 0.01f) {
                    // don't give power to cards that should not gain power, eg cards with 0 atk or giants that target multi
                    // instead give 2 hp
                    if (card.Attack == 0 || card.HasAnyOfTraits(Trait.Terrain, Trait.Giant, Trait.Structure)) {
                        card.AddTemporaryMod(new(0, 2) { singletonId = "OrdealRandomBuff" });
                    }
                    else {
                        card.AddTemporaryMod(new(1, 0) { singletonId = "OrdealRandomBuff" });
                    }
                }
                else {
                    card.AddTemporaryMod(new(0, 1) { singletonId = "OrdealRandomBuff" });
                }
            }
        }
        /// <summary>
        /// Modifies a queued card BEFORE it is fully queued, and AFTER it is modified by the Opponent class.
        /// By default, adds a random +1/+1 to a card (either or)
        /// </summary>
        public virtual void ModifyQueuedCard(PlayableCard card) {
            TryAddOrdealRandomBuff(card);
        }
        /// <summary>
        /// Modifies a spawned card BEFORE it is fully spawned, and AFTER it is modified by the Opponent class.
        /// By default, adds a random +1/+1 to a card (either or)
        /// </summary>
        public virtual void ModifySpawnedCard(PlayableCard card) {
            TryAddOrdealRandomBuff(card);
        }

        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData) {
            OrdealCounterManager.ValidateOrdealManagers();
            HighestPositiveScaleBalance = 4;
            int tier = -1;
            OrdealType type = OrdealType.Green;
            bool totem = false;
            if (nodeData is OrdealBattleNodeData ordealData) {
                tier = ordealData.tier;
                type = ordealData.ordealType;
                totem = ordealData.totemOpponent;
            }
            else if (nodeData is OrdealBossBattleNodeData bossData) {
                tier = bossData.tier;
                type = bossData.ordealType;
                totem = bossData.totemOpponent;
            }

            if (tier == -1) {
                LobotomyPlugin.Log.LogWarning("[OrdealBattle] nodeData is null!");
                return null;
            }

            ordealType = type;
            ordealTier = tier;
            EncounterData encounterData = new() {
                opponentType = OrdealUtils.OpponentID,
                Blueprint = EncounterManager.New("OrdealBlueprint", false).SetDifficulty(0, 20),
                Difficulty = Mathf.Min(20, nodeData.difficulty + RunState.Run.DifficultyModifier)
            };

            //LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Difficulty: {encounterData.Difficulty}");
            //LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Base Difficulty: {ordealData.difficulty}");

            // set the dominant tribe and redundant abilities for each Ordeal type
            switch (ordealType) {
                case OrdealType.Green:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeMechanical);
                    break;
                case OrdealType.Crimson:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeFae);
                    break;
                case OrdealType.Violet:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeDivine);
                    break;
                case OrdealType.Amber:
                    encounterData.Blueprint.AddDominantTribes(Tribe.Insect);
                    break;
                default:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeAnthropoid);
                    break;
            }

            MinNumCardsRequired = ConstructOrdealBlueprint(encounterData, nodeData.difficulty);
            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, encounterData.Difficulty, false);

            if (totem) {
                encounterData.opponentTotem = EncounterBuilder.BuildOpponentTotem(encounterData.Blueprint.dominantTribes[0], encounterData.Difficulty, null);
                AssignTotemAbility(encounterData);
            }

            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Cards required: [{MinNumCardsRequired}] turns: {encounterData.opponentTurnPlan.Count} first turn #: {encounterData.opponentTurnPlan.FirstOrDefault()?.Count}");
            EncounterBluePrint = new(encounterData.opponentTurnPlan);
            return encounterData;
        }

        private void AssignTotemAbility(EncounterData data) {
            Ability totemAbility = Ability.Sharp;
            switch (ordealType) {
                case OrdealType.Green: // Account for 0 Power cards
                    totemAbility = (Ability.Sentry);
                    break;
                case OrdealType.Crimson:
                    totemAbility = (Ability.TailOnHit); // HighStrung.ability
                    break;
                case OrdealType.Violet:
                    totemAbility = (Scorching.ability);
                    break;
                case OrdealType.Amber:
                    totemAbility = (OneSided.ability);
                    break;
                case OrdealType.Indigo: // infinite corpses
                    totemAbility = (Ability.IceCube);
                    break;
                case OrdealType.White:
                    totemAbility = (StressResponse.ability);
                    break;
            }

            data.opponentTotem.bottom.effectParams.ability = totemAbility;
        }
    }
}
