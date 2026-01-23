using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace Core.Helpers {
    public static class CombatHelpers {
        /// <summary>
        /// Static version of OpponentDrawCreatedCard logic. Draws a card to hoof or adds it to the opponent's queue.
        /// </summary>
        public static IEnumerator QueueOrCreateDrawnCard(CardInfo cardToDraw, bool opponent, List<CardModificationInfo> tempMods = null, View view = View.Default) {
            if (opponent) {
                yield return QueueCreatedCard(cardToDraw);
            }
            else {
                yield return CreateDrawnCard(cardToDraw, tempMods, view);
            }
        }

        /// <summary>
        /// Changes the current view to the specific View then creates a new card in the player's hoof.
        /// </summary>
        public static IEnumerator CreateDrawnCard(CardInfo cardToDraw, List<CardModificationInfo> tempMods = null, View view = View.Default) {
            if (Singleton<ViewManager>.Instance.CurrentView != view) {
                yield return new WaitForSeconds(0.2f);
                Singleton<ViewManager>.Instance.SwitchToView(view);
                yield return new WaitForSeconds(0.2f);
            }
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardToDraw, tempMods);
            yield return new WaitForSeconds(0.45f);
        }

        /// <summary>
        /// Creates a card with the given CardInfo in the opponent queue.
        /// </summary>
        /// <param name="cardToQueue">CardInfo for the card to add to the opponent queue.</param>
        /// <param name="slot">The queue slot to add this card to. If null, select a random open slot.</param>
        public static IEnumerator QueueCreatedCard(CardInfo cardToQueue, CardSlot slot = null) {
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
            List<CardSlot> openSlots = BoardManager.Instance.OpponentSlotsCopy.Where(x => !TurnManager.Instance.Opponent.QueuedSlots.Contains(x)).ToList();
            openSlots.RemoveAll(x => x.Card != null && x.Card.HasTrait(Trait.Giant));
            if (openSlots.Count == 0) {
                List<List<CardInfo>> turnPlan = Singleton<TurnManager>.Instance.Opponent.TurnPlan;
                List<CardInfo> addInfo = new() { cardToQueue };
                turnPlan.Add(addInfo);
                yield return Singleton<TurnManager>.Instance.Opponent.ModifyTurnPlan(turnPlan);
            }
            else {
                CardSlot index = slot ?? openSlots[SeededRandom.Range(0, openSlots.Count, randomSeed++)];
                ViewManager.Instance.SwitchToView(View.OpponentQueue);
                PlayableCard playableCard = TurnManager.Instance.Opponent.Queue.Find(x => x.QueuedSlot == index);
                if (playableCard != null) {
                    playableCard.ExitBoard(0.25f, new Vector3(-1f, -2f, 5f));
                    TurnManager.Instance.Opponent.Queue.Remove(playableCard);
                }
                yield return Singleton<TurnManager>.Instance.Opponent.QueueCard(cardToQueue, index);
            }
            yield return new WaitForSeconds(0.45f);
        }

        /// <summary>
        /// Creates a card with the given CardInfo in a random slot select from the given list.
        /// If the list of valid slots is empty, queue the created card.
        /// </summary>
        /// <param name="info">The CardInfo the created card will have.</param>
        /// <param name="validSlots">List of CardSlots that the created card can be assigned to.</param>
        public static IEnumerator CreateCardInRandomSlot(CardInfo info, List<CardSlot> validSlots) {
            if (validSlots.Count > 0) {
                yield return HelperMethods.ChangeCurrentView(View.Board, 0.4f);
                yield return BoardManager.Instance.CreateCardInSlot(info, validSlots[SeededRandom.Range(0, validSlots.Count - 1, RunState.RandomSeed)], resolveTriggers: false);
            }
            else {
                yield return HelperMethods.ChangeCurrentView(View.OpponentQueue, 0.4f);
                yield return QueueCreatedCard(info);
            }
        }
    }
}
