using DiskCardGame;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public class TradeAbnormalCardsForPelts : TradeCardsForPelts
    {
        public new IEnumerator TradePhase(int numQueueCards = 4, int numOpponentSlotCards = 4, int queueCostTier = 3, int opponentSlotCostTier = 2, string preTradeDialogueId = "TrapperTraderPreTrade", string postTradeDialogueId = "TrapperTraderPostTrade")
        {
            (Singleton<BoardManager>.Instance as BoardManager3D).Bell.enabled = false;
            (Singleton<BoardManager>.Instance as BoardManager3D).Bell.SetEnabled(enabled: false);
            Singleton<ViewManager>.Instance.SwitchToView(View.Hand);
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardLoader.GetCardByName("PeltWolf"));
            yield return new WaitForSeconds(0.75f);
            foreach (CardSlot item in Singleton<BoardManager>.Instance.OpponentSlotsCopy)
            {
                if (item.Card != null)
                {
                    item.Card.RenderInfo.hiddenCost = false;
                    item.Card.RenderCard();
                }
            }
            foreach (PlayableCard item2 in Singleton<TurnManager>.Instance.Opponent.Queue)
            {
                item2.RenderInfo.hiddenCost = false;
                item2.RenderCard();
            }
            Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue, immediate: false, lockAfter: true);
            Singleton<ItemsManager>.Instance.SetSlotsInteractable(interactable: false);
            Singleton<PlayerHand>.Instance.PlayingLocked = true;
            yield return new WaitForSeconds(0.25f);
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<TurnManager>.Instance.TurnNumber * 100;
            List<CardInfo> cardInfos = this.GenerateTradeCards(numQueueCards, numOpponentSlotCards, queueCostTier, opponentSlotCostTier, randomSeed);
            for (int j = 0; j < numOpponentSlotCards; j++)
            {
                List<CardSlot> list = Singleton<BoardManager>.Instance.OpponentSlotsCopy.FindAll((CardSlot x) => x.Card == null);
                if (list.Count <= 0)
                {
                    continue;
                }
                CardSlot slot4 = list[SeededRandom.Range(0, list.Count, randomSeed++)];
                if (slot4.Card == null && j < cardInfos.Count)
                {
                    base.StartCoroutine(Singleton<BoardManager>.Instance.CreateCardInSlot(cardInfos[j], slot4, 0.1f, resolveTriggers: false));
                    yield return new WaitUntil(() => slot4.Card != null);
                    slot4.Card.RenderInfo.hiddenCost = false;
                    slot4.Card.RenderCard();
                    yield return new WaitForSeconds(0.1f);
                }
            }
            for (int j = 0; j < numQueueCards; j++)
            {
                List<CardSlot> list2 = Singleton<BoardManager>.Instance.OpponentSlotsCopy.FindAll((CardSlot x) => !Singleton<TurnManager>.Instance.Opponent.QueuedSlots.Contains(x));
                int num = j + numOpponentSlotCards;
                if (list2.Count > 0 && num < cardInfos.Count)
                {
                    CardSlot slot3 = list2[SeededRandom.Range(0, list2.Count, randomSeed++)];
                    yield return Singleton<TurnManager>.Instance.Opponent.QueueCard(cardInfos[num], slot3, doTween: true, changeView: false);
                    PlayableCard playableCard = Singleton<TurnManager>.Instance.Opponent.Queue.Find((PlayableCard x) => x.QueuedSlot == slot3);
                    playableCard.RenderInfo.hiddenCost = false;
                    playableCard.RenderCard();
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return new WaitForSeconds(0.15f);
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(ViewController.ControlMode.TraderCardsForPeltsPhase);
            if (!string.IsNullOrEmpty(preTradeDialogueId))
            {
                yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent(preTradeDialogueId, TextDisplayer.MessageAdvanceMode.Input);
            }
            yield return new WaitForSeconds(0.05f);
            foreach (CardSlot slot2 in Singleton<BoardManager>.Instance.OpponentSlotsCopy)
            {
                if (slot2.Card != null)
                {
                    CardSlot cardSlot = slot2;
                    cardSlot.CursorSelectStarted = (Action<MainInputInteractable>)Delegate.Combine(cardSlot.CursorSelectStarted, (Action<MainInputInteractable>)delegate
                    {
                        this.OnTradableSelected(slot2, slot2.Card);
                    });
                    slot2.HighlightCursorType = CursorType.Pickup;
                }
            }
            foreach (PlayableCard card in Singleton<TurnManager>.Instance.Opponent.Queue)
            {
                HighlightedInteractable slot = Singleton<BoardManager>.Instance.OpponentQueueSlots[Singleton<BoardManager>.Instance.OpponentSlotsCopy.IndexOf(card.QueuedSlot)];
                HighlightedInteractable highlightedInteractable = slot;
                highlightedInteractable.CursorSelectStarted = (Action<MainInputInteractable>)Delegate.Combine(highlightedInteractable.CursorSelectStarted, (Action<MainInputInteractable>)delegate
                {
                    this.OnTradableSelected(slot, card);
                });
                slot.HighlightCursorType = CursorType.Pickup;
            }
            Singleton<TextDisplayer>.Instance.ShowMessage("Trade for what you can, but know this: the rest will stay and fight for me.");
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            yield return new WaitWhile(() => this.PeltInHand() && (Singleton<BoardManager>.Instance.OpponentSlotsCopy.Exists((CardSlot x) => x.Card != null) || Singleton<TurnManager>.Instance.Opponent.Queue.Count > 0));
            Singleton<TextDisplayer>.Instance.Clear();
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            foreach (CardSlot item3 in Singleton<BoardManager>.Instance.OpponentSlotsCopy)
            {
                item3.ClearDelegates();
                item3.HighlightCursorType = CursorType.Default;
            }
            foreach (HighlightedInteractable opponentQueueSlot in Singleton<BoardManager>.Instance.OpponentQueueSlots)
            {
                opponentQueueSlot.ClearDelegates();
                opponentQueueSlot.HighlightCursorType = CursorType.Default;
            }
            yield return new WaitForSeconds(0.75f);
            if (!string.IsNullOrEmpty(postTradeDialogueId))
            {
                yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent(postTradeDialogueId, TextDisplayer.MessageAdvanceMode.Input);
            }
            Tween.Rotation(Singleton<PlayerHand>.Instance.transform, (Singleton<PlayerHand>.Instance as PlayerHand3D).startingEulers, 0.25f, 0f, Tween.EaseInOut);
            Tween.Position(Singleton<PlayerHand>.Instance.transform, PlayerHand3D.DEFAULT_HAND_POS, 0.25f, 0f, Tween.EaseInOut);
            yield return new WaitForSeconds(0.15f);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            Singleton<ItemsManager>.Instance.SetSlotsInteractable(interactable: true);
            Singleton<PlayerHand>.Instance.PlayingLocked = false;
            (Singleton<BoardManager>.Instance as BoardManager3D).Bell.enabled = true;
            (Singleton<BoardManager>.Instance as BoardManager3D).Bell.SetEnabled(enabled: true);
            yield return new WaitForSeconds(0.5f);
            foreach (CardSlot item4 in Singleton<BoardManager>.Instance.OpponentSlotsCopy)
            {
                if (item4.Card != null)
                {
                    item4.Card.RenderInfo.hiddenCost = true;
                    item4.Card.RenderCard();
                }
            }
            foreach (PlayableCard item5 in Singleton<TurnManager>.Instance.Opponent.Queue)
            {
                if (item5 != null)
                {
                    item5.RenderInfo.hiddenCost = true;
                    item5.RenderCard();
                }
            }
        }
        private new List<CardInfo> GenerateTradeCards(int numQueueCards, int numOpponentSlotCards, int queueCostTier, int opponentSlotCostTier, int randomSeed)
        {
            List<CardInfo> list = new();
            list.AddRange(this.GenerateTradeCardsWithCostTier(numOpponentSlotCards, opponentSlotCostTier, randomSeed));
            randomSeed *= 2;
            list.AddRange(this.GenerateTradeCardsWithCostTier(numQueueCards, queueCostTier, randomSeed));
            return list;
        }

        private new List<CardInfo> GenerateTradeCardsWithCostTier(int numCards, int tier, int randomSeed)
        {
            bool flag = tier > 0;
            tier = Mathf.Max(1, tier);
            List<CardInfo> learnedCards = CardLoader.LearnedCards.Where(x => x.name.StartsWith("wstl")).ToList();
            learnedCards.RemoveAll((CardInfo x) => x.temple != 0 || x.CostTier != tier || x.Abilities.Exists((Ability a) => !AbilitiesUtil.GetInfo(a).opponentUsable));
            if (!ProgressionData.LearnedMechanic(MechanicsConcept.Bones))
            {
                learnedCards.RemoveAll((CardInfo x) => x.BonesCost > 0);
            }
            List<CardInfo> distinctCardsFromPool = CardLoader.GetDistinctCardsFromPool(randomSeed, numCards, learnedCards, flag ? 1 : 0, opponentUsableAbility: true);
            while (distinctCardsFromPool.Count < numCards)
            {
                CardInfo cardByName;
                CardModificationInfo cardModificationInfo;
                if (tier == 2)
                {
                    cardByName = CardLoader.GetCardByName("wstl_singingMachine");
                    cardModificationInfo = new CardModificationInfo(Ability.Sharp);
                }
                else
                {
                    cardByName = CardLoader.GetCardByName("wstl_alriune");
                    cardModificationInfo = new CardModificationInfo(Ability.Reach);
                }
                if (flag)
                {
                    cardModificationInfo.fromCardMerge = true;
                    cardByName.Mods.Add(cardModificationInfo);
                }
                distinctCardsFromPool.Add(cardByName);
            }
            return distinctCardsFromPool;
        }
    }
}
