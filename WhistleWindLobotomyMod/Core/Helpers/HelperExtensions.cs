using DiskCardGame;
using System.Collections;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Core.Helpers
{
    public static class LobotomyHelpers
    {
        public static bool AllowInitiateCombat(bool initiate)
        {
            bool canInitiateCombat = TurnManager.Instance.PlayerCanInitiateCombat;
            TurnManager.Instance.PlayerCanInitiateCombat = initiate;
            return canInitiateCombat;
        }
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
