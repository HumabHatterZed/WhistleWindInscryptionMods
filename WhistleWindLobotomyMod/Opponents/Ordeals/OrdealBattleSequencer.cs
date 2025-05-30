using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core;
using EncounterBuilder = DiskCardGame.EncounterBuilder;

namespace WhistleWindLobotomyMod.Opponents
{
    public abstract class OrdealBattleSequencer : LobotomyBattleSequencer
    {
        public OrdealType ordealType;
        public int ordealTier;
        public int amountKilledThisTurn = 0;
        private int TotalExcessDamageDealt = 0;

        public int MinNumCardsRequired { get; private set; }
        public OrdealOpponent Opponent => TurnManager.Instance.Opponent as OrdealOpponent;
        //public override bool ShowScalesOnStart => false;
        public override Opponent.Type BossType => OrdealUtils.OpponentID;
        public override StoryEvent DefeatedStoryEvent => LobotomyPlugin.OrdealDefeated;
        public override int HighestPositiveScaleBalance { get => 4; set => base.HighestPositiveScaleBalance = value; }

        /// <summary>
        /// Abstract method for constructing the battle blueprint for the current Ordeal.
        /// </summary>
        /// <param name="encounterData">The EncounterData for the current Ordeal battle.</param>
        /// <param name="difficultyModifier">An additional difficulty modifier for the battle blueprint.</param>
        /// <returns>The minimum number of Ordeal cards that must be killed to progress.</returns>
        public abstract int ConstructOrdealBlueprint(EncounterData encounterData, int difficultyModifier);

        public virtual List<Ability> GetBlacklistedAbilities(List<Ability> redundantAbilities)
        {
            return redundantAbilities;
        }
        public virtual void ModifyQueuedCard(PlayableCard card)
        {

        }
        public virtual void ModifySpawnedCard(PlayableCard card)
        {

        }
        public override IEnumerator PreCleanUp()
        {
            if (TotalExcessDamageDealt > 0)
            {
                ViewManager.Instance.SwitchToView(View.Default);
                yield return Singleton<CombatPhaseManager>.Instance.VisualizeExcessLethalDamage(TotalExcessDamageDealt, this);
            }
        }
        public override void DigUpBones(int damage, int bonesToGive, CardSlot targetSlot)
        {
            TotalExcessDamageDealt += damage - bonesToGive;
            base.DigUpBones(damage, bonesToGive, targetSlot);
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
                Difficulty = ordealData.difficulty
            };
            if (RunState.Run.DifficultyModifier > 1)
            {
                encounterData.Difficulty += RunState.Run.DifficultyModifier - 1;
            }
            LobotomyPlugin.Log.LogInfo($"[OrdealBattle] Difficulty: {encounterData.Difficulty}");
            switch (ordealType)
            {
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

            if (ordealData.totemOpponent)
                encounterData.opponentTotem = EncounterBuilder.BuildOpponentTotem(encounterData.Blueprint.dominantTribes[0], nodeData.difficulty + RunState.Run.DifficultyModifier, GetBlacklistedAbilities(encounterData.Blueprint.redundantAbilities));

            MinNumCardsRequired = ConstructOrdealBlueprint(encounterData, ordealData.difficulty);
            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Cards required: [{MinNumCardsRequired}]");
            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, encounterData.Difficulty, false);
            return encounterData;
        }

        public override IEnumerator OpponentCombatEnd()
        {
            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] OpponentCombatEnd: {OrdealCounterManager.Instance.amountLeft} left");
            return base.OpponentCombatEnd();
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return card.OpponentCard && card.HasTrait(LobotomyCardManager.Ordeal);
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            amountKilledThisTurn++;
            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] OnOtherCardDie: attacker:[{card.Info.displayedName}] killed:[{amountKilledThisTurn}]");
            LobotomyPlugin.Log.LogDebug($"[OrdealBattle] Cards left: {OrdealCounterManager.Instance.amountLeft}");
            return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
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
        public virtual bool PlayerHasDefeatedOrdeal()
        {
            if (OrdealCounterManager.Instance.amountLeft <= 0)
            {
                return BoardManager.Instance.GetOpponentCards(x => !x.Dead && x.HasTrait(LobotomyCardManager.Ordeal)).Count == 0;
            }
            return false;
        }
    }
}
