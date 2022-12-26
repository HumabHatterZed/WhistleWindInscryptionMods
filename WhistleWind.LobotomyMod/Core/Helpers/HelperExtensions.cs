using DiskCardGame;
using System.Collections;
using System.Collections.Generic;

namespace WhistleWind.LobotomyMod.Core.Helpers
{
    public static class LobotomyHelperExtensions
    {
        /// <summary>
        /// Replaces the current blueprint with a custom blueprint.
        /// </summary>
        /// <param name="encounter">Blueprint to add.</param>
        /// <param name="removeLockedCards">Removes locks cards.</param>
        public static IEnumerator ReplaceWithCustomBlueprint(this Opponent opponent, EncounterBlueprintData encounter, bool removeLockedCards = false)
        {
            opponent.Blueprint = encounter;
            int difficulty = 0;
            if (Singleton<TurnManager>.Instance.BattleNodeData != null)
                difficulty = Singleton<TurnManager>.Instance.BattleNodeData.difficulty + RunState.Run.DifficultyModifier;

            List<List<CardInfo>> plan = EncounterBuilder.BuildOpponentTurnPlan(opponent.Blueprint, difficulty, removeLockedCards);
            opponent.ReplaceAndAppendTurnPlan(plan);
            yield return opponent.QueueNewCards();
        }
    }
}
