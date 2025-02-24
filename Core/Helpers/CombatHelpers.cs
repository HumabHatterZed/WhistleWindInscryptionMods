using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using System.Collections;
using UnityEngine;

namespace Core.Helpers
{
    public static class CombatHelpers
    {
        public static IEnumerator QueueCreatedCard(CardInfo cardToQueue)
        {
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
            List<CardSlot> openSlots = Singleton<BoardManager>.Instance.OpponentSlotsCopy.FindAll(s => !Singleton<TurnManager>.Instance.Opponent.QueuedSlots.Contains(s));
            if (openSlots.Count == 0)
            {
                List<List<CardInfo>> turnPlan = Singleton<TurnManager>.Instance.Opponent.TurnPlan;
                List<CardInfo> addInfo = new() { cardToQueue };
                turnPlan.Add(addInfo);
                yield return Singleton<TurnManager>.Instance.Opponent.ModifyTurnPlan(turnPlan);
            }
            else
            {
                CardSlot index = openSlots[SeededRandom.Range(0, openSlots.Count, randomSeed++)];
                ViewManager.Instance.SwitchToView(View.OpponentQueue);
                yield return Singleton<TurnManager>.Instance.Opponent.QueueCard(cardToQueue, index);
            }
            yield return new WaitForSeconds(0.45f);
        }

        public static IEnumerator CreateCardInRandomSlot(CardInfo info, List<CardSlot> validSlots)
        {
            if (validSlots.Count > 0)
            {
                yield return HelperMethods.ChangeCurrentView(View.Board, 0.4f);
                yield return BoardManager.Instance.CreateCardInSlot(info, validSlots[SeededRandom.Range(0, validSlots.Count - 1, RunState.RandomSeed)], resolveTriggers: false);
            }
            else
            {
                yield return HelperMethods.ChangeCurrentView(View.OpponentQueue, 0.4f);
                yield return QueueCreatedCard(info);
            }
        }
    }
}
