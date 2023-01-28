using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public static class CustomMethods
    {
        public static IEnumerator QueueCreatedCard(CardInfo cardToQueue)
        {
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
            List<CardSlot> openSlots = Singleton<BoardManager>.Instance.OpponentSlotsCopy.Where(s => !Singleton<TurnManager>.Instance.Opponent.QueuedSlots.Contains(s)).ToList();
            if (openSlots.Count() == 0)
            {
                WstlPlugin.Log.LogDebug($"Appending {cardToQueue.name} to end of turn plan.");
                List<List<CardInfo>> turnPlan = Singleton<TurnManager>.Instance.Opponent.TurnPlan;
                List<CardInfo> addInfo = new() { cardToQueue };
                turnPlan.Add(addInfo);
                yield return Singleton<TurnManager>.Instance.Opponent.ModifyTurnPlan(turnPlan);
            }
            else
            {
                WstlPlugin.Log.LogDebug($"Adding {cardToQueue.name} to queue.");
                CardSlot index = openSlots[SeededRandom.Range(0, openSlots.Count, randomSeed++)];
                yield return Singleton<TurnManager>.Instance.Opponent.QueueCard(cardToQueue, index);
            }
            yield return new WaitForSeconds(0.45f);
        }
    }
}
