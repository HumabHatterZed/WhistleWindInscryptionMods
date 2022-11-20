using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWind.AbnormalSigils.Core.Helpers
{
    public static class AbnormalMethods
    {
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

        public static IEnumerator QueueCreatedCard(CardInfo cardToQueue)
        {
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
            List<CardSlot> openSlots = Singleton<BoardManager>.Instance.OpponentSlotsCopy.Where(s => !Singleton<TurnManager>.Instance.Opponent.QueuedSlots.Contains(s)).ToList();
            if (openSlots.Count() == 0)
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
            }
            yield return new WaitForSeconds(0.45f);
        }
    }
}
