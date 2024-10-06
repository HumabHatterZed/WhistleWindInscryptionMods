using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core;
using EncounterBuilder = DiskCardGame.EncounterBuilder;

namespace WhistleWindLobotomyMod.Opponents
{
    public abstract class OrdealBattleSequencer : LobotomyBattleSequencer
    {
        public OrdealType ordealType;
        public OrdealOpponent Opponent => TurnManager.Instance.Opponent as OrdealOpponent;
        public override Opponent.Type BossType => OrdealUtils.OpponentID;
        public override StoryEvent DefeatedStoryEvent => LobotomyPlugin.OrdealDefeated;
        public abstract EncounterData ConstructOrdealBlueprint(EncounterData encounterData);

        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
        {
            if (nodeData is not OrdealBattleNodeData ordealData)
            {
                LobotomyPlugin.Log.LogWarning("NodeData is null!");
                return null;
            }

            ordealType = ordealData.ordealType;
            EncounterData encounterData = new()
            {
                opponentType = OrdealUtils.OpponentID,
                Blueprint = EncounterManager.New("", false).SetDifficulty(0, 20),
                Difficulty = ordealData.difficulty + RunState.Run.DifficultyModifier
            };

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
                    encounterData.Blueprint.AddDominantTribes(Tribe.Insect).SetRedundantAbilities(Ability.WhackAMole);
                    break;
                default:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeAnthropoid).SetRedundantAbilities(Persistent.ability, Bloodfiend.ability);
                    break;
            }

            if (ordealData.totemOpponent)
                encounterData.opponentTotem = EncounterBuilder.BuildOpponentTotem(encounterData.Blueprint.dominantTribes[0], nodeData.difficulty + RunState.Run.DifficultyModifier, encounterData.Blueprint.redundantAbilities);

            ConstructOrdealBlueprint(encounterData);
            
            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, encounterData.Difficulty, false);
            return encounterData;
        }
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => PlayerHasDefeatedOrdeal();
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) => card.OpponentCard && PlayerHasDefeatedOrdeal();
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) // if the last Ordeal card has been killed, end the encounter
        {
            Opponent.NumLives--;
            yield return Opponent.LifeLostSequence();
            yield return OpponentLifeLost();
            if (Opponent.NumLives > 0)
            {
                yield return LifeManager.Instance.ShowResetSequence();
            }
            yield return Opponent.PostResetScalesSequence();
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) => OnTurnEnd(false);

        public bool PlayerHasDefeatedOrdeal()
        {
            bool ordealsOnBoard = BoardManager.Instance.GetOpponentCards(x => !x.Dead && x.HasTrait(LobotomyCardManager.Ordeal)).Count > 0;
            bool ordealsInQueue = TurnManager.Instance.Opponent.Queue.Exists(x => x.HasTrait(LobotomyCardManager.Ordeal));
            bool turnPlanIsDone = TurnManager.Instance.Opponent.NumTurnsTaken >= TurnManager.Instance.Opponent.TurnPlan.Count - 1;
            return !ordealsOnBoard && !ordealsInQueue && turnPlanIsDone;
        }
    }
}
