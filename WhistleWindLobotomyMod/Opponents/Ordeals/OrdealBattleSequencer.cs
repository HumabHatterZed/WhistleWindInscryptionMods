using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using EncounterBuilder = DiskCardGame.EncounterBuilder;

namespace WhistleWindLobotomyMod.Opponents {
    public abstract class OrdealBattleSequencer : LobotomyBattleSequencer {
        public override Opponent.Type BossType => OrdealUtils.OpponentID;
        public override StoryEvent DefeatedStoryEvent => LobotomyPlugin.OrdealDefeated;
        public override int HighestPositiveScaleBalance { get => 4; set => base.HighestPositiveScaleBalance = value; }
        public virtual List<Ability> BlacklistedAbilities { get; set; }
        public List<string> ValidCards { get; protected set; } = new();
        public List<Ability> AllBlacklistedAbilities { get; private set; }
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

        public override IEnumerator OnOpponentTurnEnd(bool opponentTurnSkipped) {
            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] OpponentTurnEnd skipped: {opponentTurnSkipped} | amountKilled: {amountKilledThisTurn}");
            if (amountKilledThisTurn != 0) {
                //LobotomyPlugin.Log.LogDebug($"[OrdealBattle] update amount left");
                yield return HelperMethods.ChangeCurrentView(OrdealUtils.ViewCounter, endDelay: 0.5f);
                yield return OrdealCounterManager.Instance.UpdateAmountLeft(amountKilledThisTurn);
                yield return new WaitForSeconds(0.75f);
            }

            amountKilledThisTurn = 0; // reset here so we can modify it in MoveOpponentCards (see Amber Dusk for ex)
            if (!defeated) {
                if (OrdealCounterManager.Instance.amountLeft == 0) {
                    defeated = true;
                    OrdealBannerManager.Instance.UpdateBannerOutro(ordealType, ordealTier);
                    OrdealBannerManager.Instance.DisplayBanner(ordealType, false);
                }
                else if (ShouldExtendBattle()) {
                    //LobotomyPlugin.Log.LogDebug("[OrdealBattle] OpponentTurnEnd: Extend turn plan");
                    Opponent.ReplaceAndAppendTurnPlan(Opponent.ModifyTurnPlan(EncounterBluePrint));
                    yield return Opponent.QueueNewCards();
                }

                if (!opponentTurnSkipped) {
                    yield return MoveOpponentCards();
                }
            }
            currentExcessBones = 0;
            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] OpponentTurnEnd: [{OrdealCounterManager.Instance.amountLeft}] left");
            if (amountKilledThisTurn != 0) {
                yield return OnOpponentTurnEnd(true);
            }
        }

        /// <summary>
        /// Checks if the battle should be extended with additional Ordeal cards.
        /// </summary>
        /// <returns>True if the player runs out of Ordeal cards before meeting the kill requirement.</returns>
        public virtual bool ShouldExtendBattle() {
            //LobotomyPlugin.Log.LogDebug($"[ShouldExtendOrdeal] {Opponent.NumTurnsTaken} {Opponent.TurnPlan.Count}");
            return Opponent.NumTurnsTaken >= Opponent.TurnPlan.Count
                && BoardManager.Instance.GetOpponentCards(CardIsValidOrdeal).Count == 0
                && Opponent.Queue.Count(CardIsValidOrdeal) == 0;
        }

        /// <returns>True if the given card's death is counted towards the kill requirement.</returns>
        protected bool CardIsValidOrdeal(PlayableCard card) {
            //LobotomyPlugin.Log.LogInfo($"Ordeal: {card.HasTrait(LobotomyCardManager.Ordeal)} Valid: {ValidCards.Count == 0} || {ValidCards.Contains(card.Info.name)}");
            return card.HasTrait(LobotomyCardManager.Ordeal) && (ValidCards.Count == 0 || ValidCards.Contains(card.Info.name));
        }

        /// <summary>
        /// Only valid Ordeal cards will trigger 'this.OnOtherCardDie'.
        /// By default, any card with the Ordeal trait is valid.
        /// </summary>
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) => true;

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
            yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);

            // Ordeal has been defeated
            if (!defeated && OrdealCounterManager.Instance.amountLeft - amountKilledThisTurn < 1) {
                defeated = true;
                OrdealBannerManager.Instance.UpdateBannerOutro(ordealType, ordealTier);
                OrdealBannerManager.Instance.DisplayBanner(ordealType, false);
            }

            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] OnOtherCardDie: dead card:[{card.Info.displayedName}] total killed:[{amountKilledThisTurn}]");
            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Cards left: {OrdealCounterManager.Instance.amountLeft}");
        }

        /// <summary>
        /// If the player dealt excess damage, visualise the money gained.
        /// </summary>
        /// <returns></returns>
        public override IEnumerator PreCleanUp() {
            if (TotalExcessDamageDealt > 0) {
                ViewManager.Instance.SwitchToView(View.Default);
                RunState.Run.currency += TotalExcessDamageDealt;
                yield return Singleton<CombatPhaseManager>.Instance.VisualizeExcessLethalDamage(TotalExcessDamageDealt, this);
            }
        }

        /// <summary>
        /// Modifies a queued card BEFORE it is fully queued, and AFTER it is modified by the Opponent class.
        /// </summary>
        /// <param name="card">The card being queued.</param>
        public virtual void ModifyQueuedCard(PlayableCard card) {
            if (UnityEngine.Random.value <= (Opponent.Difficulty * 0.02f)) {
                if (UnityEngine.Random.value <= 0.2f) {
                    card.AddTemporaryMod(new(1, 0));
                }
                else {
                    card.AddTemporaryMod(new(0, 1));
                }
            }
        }
        /// <summary>
        /// Modifies a spawned card BEFORE it is fully spawned, and AFTER it is modified by the Opponent class.
        /// </summary>
        /// <param name="card"></param>
        public virtual void ModifySpawnedCard(PlayableCard card) {

        }

        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData) {
            OrdealCounterManager.ValidateCounter();
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
                Blueprint = EncounterManager.New("", false).SetDifficulty(0, 20),
                Difficulty = Mathf.Min(20, nodeData.difficulty + RunState.Run.DifficultyModifier)
            };

            //LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Difficulty: {encounterData.Difficulty}");
            //LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Base Difficulty: {ordealData.difficulty}");

            // set the dominant tribe and redundant abilities for each Ordeal type
            switch (ordealType) {
                case OrdealType.Green:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeMechanical).SetRedundantAbilities(Ability.Flying, Piercing.ability);
                    break;
                case OrdealType.Crimson:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeFae).SetRedundantAbilities(Ability.ExplodeOnDeath);
                    break;
                case OrdealType.Violet:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeDivine).SetRedundantAbilities(Scorching.ability, Ability.Evolve);
                    break;
                case OrdealType.Amber:
                    encounterData.Blueprint.AddDominantTribes(Tribe.Insect).SetRedundantAbilities(Ability.WhackAMole, Ability.Strafe, Ability.StrafePush, Cycler.ability, Barreler.ability);
                    break;
                default:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeAnthropoid).SetRedundantAbilities(Persistent.ability, Bloodfiend.ability);
                    break;
            }

            MinNumCardsRequired = ConstructOrdealBlueprint(encounterData, nodeData.difficulty);
            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, encounterData.Difficulty, false);

            if (totem) {
                GetAllBlacklistedAbilities(encounterData.Blueprint.redundantAbilities);
                encounterData.opponentTotem = EncounterBuilder.BuildOpponentTotem(encounterData.Blueprint.dominantTribes[0], encounterData.Difficulty, AllBlacklistedAbilities);
            }

            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Cards required: [{MinNumCardsRequired}] {encounterData.opponentTurnPlan.Count} {encounterData.opponentTurnPlan.FirstOrDefault()?.Count}");
            EncounterBluePrint = new(encounterData.opponentTurnPlan);
            return encounterData;
        }

        private void GetAllBlacklistedAbilities(List<Ability> redundantAbilities) {
            AllBlacklistedAbilities = new(redundantAbilities);
            if (BlacklistedAbilities != null) {
                AllBlacklistedAbilities.AddRange(BlacklistedAbilities);
            }

            // Since Ordeals are card-destruction, Waterborne cannot be a Totem sigil
            if (!AllBlacklistedAbilities.Contains(Ability.Submerge)) {
                AllBlacklistedAbilities.Add(Ability.Submerge);
            }
        }
    }
}
