using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

namespace WhistleWindLobotomyMod
{
    public class LeshyAbnormalBossOpponent : LeshyBossOpponent
    {
		public new IEnumerator AdvanceMaskState()
		{
			if (base.NumLives > 1)
			{
				switch (this.maskState)
				{
					case MaskState.NoMask:
						yield return this.SwitchToMask(this.currentMaskIndex);
						this.maskState = MaskState.MaskEquipped;
						break;
					case MaskState.MaskEquipped:
						yield return this.ActivateCurrentMask();
						yield return new WaitForSeconds(0.1f);
						yield return this.CleanUpCurrentMask();
						this.IncrementMaskIndex();
						this.maskState = MaskState.NoMask;
						break;
				}
			}
		}
		public override IEnumerator StartNewPhaseSequence()
		{
			base.TurnPlan.Clear();
			switch (base.NumLives)
			{
				case 2:
					yield return this.StartDeathcardPhase();
					break;
				case 1:
					yield return this.StartMoonPhase();
					break;
			}
		}
		private new IEnumerator StartDeathcardPhase()
		{
			Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue);
			yield return new WaitForSeconds(0.1f);
			yield return base.ClearQueue();
			yield return new WaitForSeconds(0.1f);
			foreach (CardSlot item in Singleton<BoardManager>.Instance.OpponentSlotsCopy)
			{
				if (item.Card == null && item.opposingSlot.Card != null && item.opposingSlot.Card.Attack > 0 && !item.opposingSlot.Card.HasAbility(Ability.SplitStrike))
				{
					string text = (item.opposingSlot.Card.HasAbility(Ability.Flying) ? "Tree" : "Stump");
					yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName(text), item, 0.1f, resolveTriggers: false);
				}
			}
			yield return new WaitForSeconds(0.1f);
			yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("LeshyBossDeathcards1", TextDisplayer.MessageAdvanceMode.Input);
			List<CardInfo> target = this.CreateUsableDeathcards();
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
			this.TryAddDeathCardsToTurn(2, target, list[0]);
			list[0].Add(CardLoader.GetCardByName("wstl_nothingThere"));
			this.TryAddDeathCardsToTurn(1, target, list[1]);
			this.TryAddDeathCardsToTurn(1, target, list[2]);
			this.TryAddDeathCardsToTurn(2, target, list[4]);
			this.TryAddDeathCardsToTurn(1, target, list[6]);
			base.ReplaceAndAppendTurnPlan(list);
			yield return this.QueueNewCards();
			yield return new WaitForSeconds(0.1f);
			yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("LeshyBossDeathcards2", TextDisplayer.MessageAdvanceMode.Input);
		}
		private new List<CardInfo> CreateUsableDeathcards()
		{
			List<CardInfo> list = new();
			foreach (CardModificationInfo item2 in SaveFile.IsAscension ? DefaultDeathCards.CreateAscensionCardMods() : SaveManager.SaveFile.deathCardMods)
			{
				WstlPlugin.Log.LogDebug($"{item2.singletonId} {item2}");
				if (!item2.abilities.Exists((Ability x) => !AbilitiesUtil.GetInfo(x).opponentUsable) && item2.singletonId != null)
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
			this.currentMaskIndex = index;
			Singleton<ViewManager>.Instance.SwitchToView(View.DefaultUpwards);
			yield return new WaitForSeconds(0.1f);
			Singleton<OpponentAnimationController>.Instance.SetHeadSteady(steady: true);
			yield return this.maskOrbiter.RotateToMaskIndex(index);
			yield return new WaitForSeconds(0.1f);
			this.currentMask = this.maskOrbiter.DetachMask(this.currentMaskIndex);
			LeshyAnimationController.Instance.ParentObjectToFace(this.currentMask);
			Tween.Position(this.currentMask, this.currentMask.position + Vector3.forward * 0.5f, 0.1f, 0f, Tween.EaseInOut);
			switch (this.maskBossTypes[this.currentMaskIndex])
			{
				case Type.ProspectorBoss:
					this.FadeInSecondaryTrack(1);
					base.InstantiateBossBehaviour<PickAxeSlam>();
					break;
				case Type.AnglerBoss:
					this.FadeInSecondaryTrack(2);
					base.InstantiateBossBehaviour<FishHookGrab>();
					yield return new WaitForSeconds(1f);
					yield return this.AimAnglerHook();
					break;
				case Type.TrapperTraderBoss:
					this.FadeInSecondaryTrack(3);
					base.InstantiateBossBehaviour<TradeAbnormalCardsForPelts>();
					break;
			}
			yield return new WaitForSeconds(1f);
		}
		private new IEnumerator ActivateCurrentMask()
		{
			switch (this.maskBossTypes[this.currentMaskIndex])
			{
				case Type.ProspectorBoss:
					yield return this.ActivateProspector();
					break;
				case Type.AnglerBoss:
					yield return this.ActivateAngler();
					break;
				case Type.TrapperTraderBoss:
					yield return this.ActivateTrader();
					break;
			}
		}
		private new IEnumerator ActivateTrader()
		{
			yield return base.GetComponent<TradeAbnormalCardsForPelts>().TradePhase(1, 1, 2, 1, "", "");
		}
	}
}