using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWindLobotomyMod.Opponents.Leshy
{
    public class LeshyAbnormalBossOpponent : LeshyBossOpponent
    {
        public override IEnumerator StartNewPhaseSequence()
        {
            // override this IEnum so it'll use the new Start...Phase IEnums
            TurnPlan.Clear();
            switch (NumLives)
            {
                case 2:
                    yield return this.StartDeathcardPhase();
                    break;
                case 1:
                    yield return base.StartMoonPhase();
                    break;
            }
        }
        private new IEnumerator StartDeathcardPhase()
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue);
            yield return new WaitForSeconds(0.1f);
            yield return ClearQueue();
            yield return new WaitForSeconds(0.1f);
            foreach (CardSlot item in Singleton<BoardManager>.Instance.OpponentSlotsCopy)
            {
                if (item.Card == null && item.opposingSlot.Card != null && item.opposingSlot.Card.Attack > 0 && !item.opposingSlot.Card.HasAbility(Ability.SplitStrike))
                {
                    string text = item.opposingSlot.Card.HasAbility(Ability.Flying) ? "Tree" : "Stump";
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName(text), item, 0.1f, resolveTriggers: false);
                }
            }
            yield return new WaitForSeconds(0.1f);
            yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("LeshyBossDeathcards1", TextDisplayer.MessageAdvanceMode.Input);
            List<CardInfo> target = CreateUsableDeathcards();
            target = new List<CardInfo>(target.Randomize());
            List<List<CardInfo>> list = new()
        {
            new List<CardInfo>(),
            new List<CardInfo>(),
            new List<CardInfo>(),
            new List<CardInfo>(),
            new List<CardInfo>(),
            new List<CardInfo>(),
            new List<CardInfo>(),
            new List<CardInfo>()
        };
            TryAddDeathCardsToTurn(2, target, list[0]);
            list[0].Add(CardLoader.GetCardByName("wstl_nothingThere"));
            TryAddDeathCardsToTurn(1, target, list[1]);
            TryAddDeathCardsToTurn(1, target, list[2]);
            TryAddDeathCardsToTurn(2, target, list[4]);
            TryAddDeathCardsToTurn(1, target, list[6]);
            ReplaceAndAppendTurnPlan(list);
            yield return QueueNewCards();
            yield return new WaitForSeconds(0.1f);
            yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("LeshyBossDeathcards2", TextDisplayer.MessageAdvanceMode.Input);
        }
        private new List<CardInfo> CreateUsableDeathcards()
        {
            List<CardInfo> list = new();
            foreach (CardModificationInfo item2 in SaveFile.IsAscension ? DefaultDeathCards.CreateAscensionCardMods() : SaveManager.SaveFile.deathCardMods)
            {
                LobotomyPlugin.Log.LogDebug($"{item2.singletonId} {item2}");
                if (!item2.abilities.Exists((x) => !AbilitiesUtil.GetInfo(x).opponentUsable) && item2.singletonId != null)
                {
                    if (item2.singletonId.StartsWith("wstl"))
                    {
                        CardInfo item = CardLoader.CreateDeathCard(item2);
                        list.Add(item);
                    }
                }
            }
            return list;
        }
    }
}