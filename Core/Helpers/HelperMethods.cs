using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class HelperMethods
    {
        public static IEnumerator FlipFaceUp(this PlayableCard card, bool faceDown, float wait = 0.3f)
        {
            if (!faceDown) yield break;

            card.SetFaceDown(false);
            card.UpdateFaceUpOnBoardEffects();
            yield return new WaitForSeconds(wait);
        }
        public static IEnumerator FlipFaceDown(this PlayableCard card, bool faceDown, float wait = 0.3f)
        {
            if (!faceDown) yield break;

            card.SetCardbackSubmerged();
            card.SetFaceDown(true);
            yield return new WaitForSeconds(wait);
        }

        public static List<CardSlot> GetSlotsCopy(bool isOpponentCard)
        {
            return isOpponentCard ? Singleton<BoardManager>.Instance.OpponentSlotsCopy : Singleton<BoardManager>.Instance.PlayerSlotsCopy;
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
        public static IEnumerator PlayAlternateDialogue(
            Emotion emotion = Emotion.Neutral,
            DialogueEvent.Speaker speaker = DialogueEvent.Speaker.Leshy, float delay = 0.2f,
            params string[] dialogue)
        {
            yield return new WaitForSeconds(delay);
            foreach (string s in dialogue)
            {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(s, emotion: emotion, speaker: speaker);
            }
            yield return new WaitForSeconds(delay);
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
                    PlayableCard card = Singleton<TurnManager>.Instance.Opponent.Queue.Find(x => x.Info == cardToQueue);
                    if (card.TriggerHandler.RespondsToTrigger(Trigger.ResolveOnBoard))
                        yield return card.TriggerHandler.OnTrigger(Trigger.ResolveOnBoard);

                    yield return Singleton<GlobalTriggerHandler>.Instance.TriggerCardsOnBoard(Trigger.OtherCardResolve, false, card);
                }
            }
            yield return new WaitForSeconds(0.45f);
        }
    }
}
