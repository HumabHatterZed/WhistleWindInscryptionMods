using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using EncounterBuilder = DiskCardGame.EncounterBuilder;

namespace WhistleWindLobotomyMod.Opponents
{
    public abstract class OrdealBattleSequencer : LobotomyBattleSequencer
    {
        public override Opponent.Type BossType => OrdealUtils.OpponentID;
        public override StoryEvent DefeatedStoryEvent => LobotomyPlugin.OrdealDefeated;
        public override int HighestPositiveScaleBalance { get => 4; set => base.HighestPositiveScaleBalance = value; }
        public OrdealOpponent Opponent => TurnManager.Instance.Opponent as OrdealOpponent;
        public int MinNumCardsRequired { get; protected set; }

        public OrdealType ordealType;
        public int ordealTier;
        public int amountKilledThisTurn = 0;
        private int TotalExcessDamageDealt = 0;
        public bool defeated = false;

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
        public virtual bool PlayerHasDefeatedOrdeal()
        {
            return defeated || OrdealCounterManager.Instance.amountLeft == 0;
        }

        /// <summary>
        /// Tracks excess damage dealt then calls the base DigUpBones method.
        /// </summary>
        public override void DigUpBones(int damage, int bonesToGive, CardSlot targetSlot)
        {
            TotalExcessDamageDealt += damage - bonesToGive;
            base.DigUpBones(damage, bonesToGive, targetSlot);
        }

        public override IEnumerator OnRoundEnd(bool opponentTurnSkipped)
        {
            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] OnRoundEnd: {OrdealCounterManager.Instance.amountLeft} left");
            if (amountKilledThisTurn > 0)
            {
                yield return HelperMethods.ChangeCurrentView(OrdealUtils.ViewCounter, endDelay: 0.5f);
                yield return OrdealCounterManager.Instance.UpdateAmountLeft(amountKilledThisTurn, 0.25f);
                yield return new WaitForSeconds(0.75f);
            }

            yield return MoveOpponentCards();

            amountKilledThisTurn = 0;
            yield return base.OnRoundEnd(opponentTurnSkipped);
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return card.HasTrait(LobotomyCardManager.Ordeal);
        }

        /// <remarks>
        /// By default, only triggers when an opponent-owned Ordeal card dies.
        /// </remarks>
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            amountKilledThisTurn++;
            yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);

            // Ordeal has been defeated
            if (!defeated && OrdealCounterManager.Instance.amountLeft - amountKilledThisTurn < 1)
            {
                defeated = true;
                OrdealBannerManager.Instance.UpdateBannerOutro(ordealType, ordealTier);
                OrdealBannerManager.Instance.DisplayBanner(ordealType, false);
            }

            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] OnOtherCardDie: dead card:[{card.Info.displayedName}] total killed:[{amountKilledThisTurn}]");
            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Cards left: {OrdealCounterManager.Instance.amountLeft}");
        }

        /*        public IEnumerator VerifyOrdealDefeated()
                {
                    if (OrdealCounterManager.Instance.amountLeft <= 0 && PlayerHasDefeatedOrdeal())
                    {
                        LobotomyPlugin.Log.LogInfo($"Defeated Ordeal [{ordealType}] on turn [{TurnNumber}]");
                        Opponent.NumLives--;
                        LifeManager.Instance.PlayerDamage = 0;
                        LifeManager.Instance.OpponentDamage = 10; // ensure end of battle sequence triggers
                        yield return Opponent.LifeLostSequence();
                        yield return OpponentLifeLost();
                        if (Opponent.NumLives > 0)
                        {
                            yield return LifeManager.Instance.ShowResetSequence();
                        }
                        yield return Opponent.PostResetScalesSequence();
                    }
                }*/

        /// <summary>
        /// If the player dealt excess damage, visualise the money gained.
        /// </summary>
        /// <returns></returns>
        public override IEnumerator PreCleanUp()
        {
            if (TotalExcessDamageDealt > 0)
            {
                ViewManager.Instance.SwitchToView(View.Default);
                yield return Singleton<CombatPhaseManager>.Instance.VisualizeExcessLethalDamage(TotalExcessDamageDealt, this);
            }
        }

        /// <summary>
        /// Modifies a queued card BEFORE it is fully queued, and AFTER it is modified by the Opponent class.
        /// </summary>
        /// <param name="card">The card being queued.</param>
        public virtual void ModifyQueuedCard(PlayableCard card)
        {

        }
        /// <summary>
        /// Modifies a spawned card BEFORE it is fully spawned, and AFTER it is modified by the Opponent class.
        /// </summary>
        /// <param name="card"></param>
        public virtual void ModifySpawnedCard(PlayableCard card)
        {

        }

        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
        {
            OrdealCounterManager.ValidateCounter();
            if (nodeData is not OrdealBattleNodeData ordealData)
            {
                LobotomyPlugin.Log.LogWarning("[OrdealBattle] NodeData is null!");
                return null;
            }

            ordealType = ordealData.ordealType;
            ordealTier = ordealData.tier;
            EncounterData encounterData = new()
            {
                opponentType = OrdealUtils.OpponentID,
                Blueprint = EncounterManager.New("", false).SetDifficulty(0, 20),
                Difficulty = ordealData.difficulty + RunState.Run.DifficultyModifier
            };

            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Difficulty: {encounterData.Difficulty}");
            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Base Difficulty: {ordealData.difficulty}");

            // set the dominant tribe and redundant abilities for each Ordeal type
            switch (ordealType) {
                case OrdealType.Green:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeMechanical).SetRedundantAbilities(Piercing.ability);
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

            MinNumCardsRequired = ConstructOrdealBlueprint(encounterData, ordealData.difficulty);
            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, encounterData.Difficulty, false);
            
            if (ordealData.totemOpponent) {
                GetAllBlacklistedAbilities(encounterData.Blueprint.redundantAbilities);
                encounterData.opponentTotem = EncounterBuilder.BuildOpponentTotem(encounterData.Blueprint.dominantTribes[0], encounterData.Difficulty, AllBlacklistedAbilities);
            }

            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Cards required: [{MinNumCardsRequired}] {encounterData.opponentTurnPlan.Count} {encounterData.opponentTurnPlan.FirstOrDefault()?.Count}");

            return encounterData;
        }

        public virtual List<Ability> BlacklistedAbilities { get; set; }
        public List<Ability> AllBlacklistedAbilities { get; private set; }

        private void GetAllBlacklistedAbilities(List<Ability> redundantAbilities) {
            AllBlacklistedAbilities = new(redundantAbilities);
            if (BlacklistedAbilities != null) {
                AllBlacklistedAbilities.AddRange(BlacklistedAbilities);
            }
        }
    }
}
