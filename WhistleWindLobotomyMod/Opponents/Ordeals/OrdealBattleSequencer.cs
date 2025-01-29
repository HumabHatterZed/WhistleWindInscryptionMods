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

        public abstract EncounterData ConstructOrdealBlueprint(EncounterData encounterData);

        public virtual int GetMinCardsRequired(EncounterData data)
        {
            int retval = 0;
            data.Blueprint.turns.ForEach(x => retval += x.Count(x => x.card.HasTrait(LobotomyCardManager.Ordeal)));
            return retval;
        }

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
                LobotomyPlugin.Log.LogWarning("NodeData is null!");
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
            LobotomyPlugin.Log.LogInfo($"Ordeal difficulty: {encounterData.Difficulty}");
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

            ConstructOrdealBlueprint(encounterData);
            MinNumCardsRequired = GetMinCardsRequired(encounterData);
            LobotomyPlugin.Log.LogInfo($"Encounter made: [{MinNumCardsRequired}] cards required");
            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, encounterData.Difficulty, false);
            return encounterData;
        }

        public override IEnumerator OpponentCombatEnd()
        {
            LobotomyPlugin.Log.LogInfo($"TurnEnd: {OrdealCounterManager.Instance.amountLeft}");
            return base.OpponentCombatEnd();
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return card.OpponentCard && card.HasTrait(LobotomyCardManager.Ordeal);
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            amountKilledThisTurn++;
            LobotomyPlugin.Log.LogInfo($"Ordeal [{card.Info.displayedName}] killed: {amountKilledThisTurn} | Left: {OrdealCounterManager.Instance.amountLeft}");
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
