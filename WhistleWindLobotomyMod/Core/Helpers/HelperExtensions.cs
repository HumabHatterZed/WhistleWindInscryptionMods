using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWindLobotomyMod.Core.Helpers
{
    public static class HelperExtensions
    {
        /// <summary>
        /// Removes this card from the board.
        /// </summary>
        /// <param name="removeFromDeck">Whether to also remove from the player's deck. Does not affect opponent cards.</param>
        /// <param name="tweenLength">How long the ExitBoard animation should take to play.</param>
        public static void RemoveFromBoard(this PlayableCard item, bool removeFromDeck = false, float tweenLength = 0.3f)
        {
            // Remove from deck if this card is owned by the player
            if (removeFromDeck && !item.OpponentCard && !item.OriginatedFromQueue)
                RunState.Run.playerDeck.RemoveCard(item.Info);

            item.UnassignFromSlot();
            SpecialCardBehaviour[] components = item.GetComponents<SpecialCardBehaviour>();
            for (int i = 0; i < components.Length; i++)
            {
                components[i].OnCleanUp();
            }
            item.ExitBoard(tweenLength, Vector3.zero);
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
