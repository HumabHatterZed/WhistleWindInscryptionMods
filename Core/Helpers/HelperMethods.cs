using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class HelperMethods
    {
        public static void RemoveCardFromDeck(CardInfo info)
        {
            if (SaveManager.SaveFile.IsPart2)
            {
                SaveManager.SaveFile.gbcData.deck.RemoveCard(info);
                SaveManager.SaveFile.gbcData.collection.RemoveCardByName(info.name);
            }
            else
            {
                if (RunState.Run.playerDeck.Cards.Contains(info))
                    RunState.Run.playerDeck.RemoveCard(info);
                else
                    RunState.Run.playerDeck.RemoveCardByName(info.name);
            }
        }
        public static IEnumerator FlipFaceUp(this PlayableCard card, bool faceDown, float wait = 0.3f)
        {
            if (!faceDown)
                yield break;

            card.SetFaceDown(false);
            card.UpdateFaceUpOnBoardEffects();
            yield return new WaitForSeconds(wait);
        }
        public static IEnumerator FlipFaceDown(this PlayableCard card, bool setFaceDown, float wait = 0.3f)
        {
            // if set down and we're down OR set up and we're up
            if ((setFaceDown && card.FaceDown) || (!setFaceDown && !card.FaceDown))
                yield break;

            if (setFaceDown)
                card.SetCardbackSubmerged();

            card.SetFaceDown(setFaceDown);

            if (!setFaceDown)
                card.UpdateFaceUpOnBoardEffects();

            yield return new WaitForSeconds(wait);
        }

        public static CardInfo GetInfoWithMods(PlayableCard card, string name)
        {
            CardInfo cardByName = CardLoader.GetCardByName(name);
            foreach (CardModificationInfo item in card.Info.Mods.FindAll((x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardByName.Mods.Add(cardModificationInfo);
            }
            return cardByName;
        }
        public static IEnumerator ChangeCurrentView(View view, float startDelay = 0.2f, float endDelay = 0.2f)
        {
            if (Singleton<ViewManager>.Instance.CurrentView != view)
            {
                yield return new WaitForSeconds(startDelay);
                Singleton<ViewManager>.Instance.SwitchToView(view);
                yield return new WaitForSeconds(endDelay);
            }
        }

        public static IEnumerator QueueCreatedCard(CardInfo cardToQueue, bool triggerResolve = false)
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
                yield return Singleton<TurnManager>.Instance.Opponent.QueueCard(cardToQueue, index);
                if (triggerResolve)
                {
                    PlayableCard card = Singleton<TurnManager>.Instance.Opponent.Queue.Find(x => x.Info.name == cardToQueue.name);

                    if (card != null && card.TriggerHandler.RespondsToTrigger(Trigger.ResolveOnBoard))
                        yield return card.TriggerHandler.OnTrigger(Trigger.ResolveOnBoard);

                }
            }
            yield return new WaitForSeconds(0.45f);
        }
    }
}
