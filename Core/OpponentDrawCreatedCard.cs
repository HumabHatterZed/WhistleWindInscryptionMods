using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public abstract class OpponentDrawCreatedCard : DrawCreatedCard
    {
        protected IEnumerator QueueOrCreateDrawnCard()
        {
            // Queue card if opponent, otherwise draw card to hand
            if (base.Card.OpponentCard)
            {
		int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
                List<CardSlot> openSlots = Singleton<BoardManager>.Instance.OpponentSlotsCopy.Where(s => !Singleton<TurnManager>.Instance.Opponent.QueuedSlots.Contains(s)).ToList();
                if (openSlots.Count() < 1)
                {
                    WstlPlugin.Log.LogDebug($"Appending {CardToDraw.name} to end of turn plan.");
                    List<List<CardInfo>> turnPlan = Singleton<TurnManager>.Instance.Opponent.TurnPlan;
                    List<CardInfo> addInfo = new() { CardToDraw };
                    turnPlan.Add(addInfo);
                    yield return Singleton<TurnManager>.Instance.Opponent.ModifyTurnPlan(turnPlan);
                }
                else
                {
                    WstlPlugin.Log.LogDebug($"Adding {CardToDraw.name} to queue.");
                    CardSlot index = openSlots[SeededRandom.Range(0, openSlots.Count, randomSeed++)];
                    yield return Singleton<TurnManager>.Instance.Opponent.QueueCard(CardToDraw, index);
                }
                yield return new WaitForSeconds(0.45f);
	    }
            else
            {
                yield return base.CreateDrawnCard();
            }
        }
    }
}
