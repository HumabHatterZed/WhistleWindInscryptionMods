using DiskCardGame;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.LobotomyMod.Core.Opponents.TrapperTrader;

namespace WhistleWind.LobotomyMod.Core.Opponents.Leshy
{
    public class LeshyAbnormalBossOpponent : LeshyBossOpponent
    {
        public new IEnumerator AdvanceMaskState()
        {
            if (NumLives > 1)
            {
                switch (maskState)
                {
                    case MaskState.NoMask:
                        yield return SwitchToMask(currentMaskIndex);
                        maskState = MaskState.MaskEquipped;
                        break;
                    case MaskState.MaskEquipped:
                        yield return ActivateCurrentMask();
                        yield return new WaitForSeconds(0.1f);
                        yield return CleanUpCurrentMask();
                        IncrementMaskIndex();
                        maskState = MaskState.NoMask;
                        break;
                }
            }
        }
        public override IEnumerator StartNewPhaseSequence()
        {
            TurnPlan.Clear();
            switch (NumLives)
            {
                case 2:
                    yield return StartDeathcardPhase();
                    break;
                case 1:
                    yield return StartMoonPhase();
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
        private new IEnumerator SwitchToMask(int index)
        {
            currentMaskIndex = index;
            Singleton<ViewManager>.Instance.SwitchToView(View.DefaultUpwards);
            yield return new WaitForSeconds(0.1f);
            Singleton<OpponentAnimationController>.Instance.SetHeadSteady(steady: true);
            yield return maskOrbiter.RotateToMaskIndex(index);
            yield return new WaitForSeconds(0.1f);
            currentMask = maskOrbiter.DetachMask(currentMaskIndex);
            LeshyAnimationController.Instance.ParentObjectToFace(currentMask);
            Tween.Position(currentMask, currentMask.position + Vector3.forward * 0.5f, 0.1f, 0f, Tween.EaseInOut);
            switch (maskBossTypes[currentMaskIndex])
            {
                case Type.ProspectorBoss:
                    FadeInSecondaryTrack(1);
                    InstantiateBossBehaviour<PickAxeSlam>();
                    break;
                case Type.AnglerBoss:
                    FadeInSecondaryTrack(2);
                    InstantiateBossBehaviour<FishHookGrab>();
                    yield return new WaitForSeconds(1f);
                    yield return AimAnglerHook();
                    break;
                case Type.TrapperTraderBoss:
                    FadeInSecondaryTrack(3);
                    InstantiateBossBehaviour<TradeAbnormalCardsForPelts>();
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
        private new IEnumerator ActivateCurrentMask()
        {
            switch (maskBossTypes[currentMaskIndex])
            {
                case Type.ProspectorBoss:
                    yield return ActivateProspector();
                    break;
                case Type.AnglerBoss:
                    yield return ActivateAngler();
                    break;
                case Type.TrapperTraderBoss:
                    yield return ActivateTrader();
                    break;
            }
        }
        private new IEnumerator ActivateTrader()
        {
            yield return GetComponent<TradeAbnormalCardsForPelts>().TradePhase(1, 1, 2, 1, "", "");
        }
    }
}