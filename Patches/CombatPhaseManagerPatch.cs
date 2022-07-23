using InscryptionAPI;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
	[HarmonyPatch(typeof(CombatPhaseManager))]
	public class CombatPhaseManagerPatch
	{
		private static CombatPhaseManagerPatch c_instance;
		public static CombatPhaseManagerPatch Instance => c_instance ??= new CombatPhaseManagerPatch();
		private List<GameObject> sniperIcons = new();
		private GameObject sniperIconPrefab;

		// nothing I love more than just ripping code verbatim
		// basically just adds the Marksman ability to the attack sequence code, plus all the special code
		[HarmonyPostfix, HarmonyPatch(nameof(CombatPhaseManager.SlotAttackSequence))]
		public static IEnumerator SlotAttackSequence(IEnumerator enumerator, CombatPhaseManager __instance, CardSlot slot)
		{
			bool hasMarksman = slot.Card != null && slot.Card.HasAbility(Marksman.ability);
			bool isJudge = slot.Card != null && slot.Card.Info.name.Equals("wstl_judgementBird");
			if (slot.Card.OpponentCard || !hasMarksman)
			{
				// Returns normal behaviour for opponent cards
				yield return enumerator;
				yield break;
			}

			List<CardSlot> opposingSlots = new();
			Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
			Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
			int numAttacks = 1;
			if (slot.Card.HasTriStrike())
			{
				numAttacks = 3;
			}
			else if (slot.Card.HasAbility(Ability.SplitStrike))
			{
				numAttacks = 2;
			}
			if (slot.Card.HasAbility(Ability.Sniper))
			{
				// If a card has both Sniper and Marksman, then it'll add an extra shot. How nice am I.
				numAttacks += 1;
			}
		    Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode);
			Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
			for (int i = 0; i < numAttacks; i++)
			{
				CombatPhaseManagerPatch.Instance.VisualizeStartSniperAbility(slot);
				CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;

				if (cardSlot != null && opposingSlots.Contains(cardSlot))
				{
					CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(slot, cardSlot);
				}
				yield return Singleton<BoardManager>.Instance.ChooseTarget(Singleton<BoardManager>.Instance.OpponentSlotsCopy, Singleton<BoardManager>.Instance.OpponentSlotsCopy, delegate (CardSlot s)
				{
					opposingSlots.Add(s);
					CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(s, isJudge);
				}, null, delegate (CardSlot s)
				{
					CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(slot, s);

				}, () => false, isJudge ? CursorType.Sacrifice : CursorType.Target);
			}
			foreach (CardSlot item in opposingSlots)
			{
				Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
				if (isJudge && item.Card != null)
                {
					yield return new WaitForSeconds(0.1f);
					yield return CombatPhaseManagerPatch.Instance.Execution(item.Card);
				}
				else
                {
					yield return Singleton<CombatPhaseManager>.Instance.SlotAttackSlot(slot, item, (opposingSlots.Count > 1) ? 0.1f : 0f);
				}
			}
			Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode);
			Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
			CombatPhaseManagerPatch.Instance.VisualizeClearSniperAbility();
			yield break;
		}
		public void VisualizeStartSniperAbility(CardSlot sniperSlot)
        {
		}
		public void VisualizeAimSniperAbility(CardSlot sniperSlot, CardSlot targetSlot)
		{
		}
		public void VisualizeConfirmSniperAbility(CardSlot targetSlot, bool IsJudge)
		{
			if (IsJudge && targetSlot.Card != null)
            {
				targetSlot.Card.Anim.SetMarkedForSacrifice(marked: true);
			}
			else
            {
				if (sniperIconPrefab == null)
				{
					sniperIconPrefab = ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/SniperTargetIcon");
				}
				GameObject gameObject = Object.Instantiate(sniperIconPrefab, targetSlot.transform);
				gameObject.transform.localPosition = new Vector3(0f, 0.25f, 0f);
				gameObject.transform.localRotation = Quaternion.identity;
				sniperIcons.Add(gameObject);
			}
		}
		public void VisualizeClearSniperAbility()
		{
			sniperIcons.ForEach(delegate (GameObject x)
			{
				Object.Destroy(x, 0.1f);
			});
			sniperIcons.Clear();
		}
		private IEnumerator Execution(PlayableCard target)
        {
			target.Anim.PlaySacrificeSound();
			target.Anim.DeactivateSacrificeHoverMarker();
			yield return target.Die(wasSacrifice: false);
			yield return Singleton<ResourcesManager>.Instance.AddBones(1, target.Slot);
		}
	}
}
