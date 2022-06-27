using InscryptionAPI;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
	public class CombatPhasePatcher
	{
		private static CombatPhasePatcher w_instance;
		public static CombatPhasePatcher Instance => w_instance ??= new CombatPhasePatcher();

		private List<GameObject> sniperIcons = new List<GameObject>();
		private GameObject sniperIconPrefab;

		// nothing I love more than just ripping code verbatim
		// basically just adds the Marksman ability to the attack sequence code, plus all the special code
		[HarmonyPatch(typeof(CombatPhaseManager), nameof(CombatPhaseManager.SlotAttackSequence))]
		[HarmonyPostfix]
		public static IEnumerator SlotAttackSequence(IEnumerator enumerator, CombatPhaseManager __instance, CardSlot slot)
		{
			if (slot.Card.OpponentCard)
			{
				// Returns normal behaviour for opponent cards
				yield return enumerator;
				yield break;
			}
			bool hasMarksman = slot.Card != null && slot.Card.HasAbility(Marksman.ability);
			if (!hasMarksman)
            {
				// Same as above but checks for Marksman
				yield return enumerator;
				yield break;
            }
			bool isJudge = slot.Card != null && slot.Card.name.ToLowerInvariant().Contains("judgementbird");
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
				CombatPhasePatcher.Instance.VisualizeStartSniperAbility(slot);
				CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;

				if (cardSlot != null && opposingSlots.Contains(cardSlot))
				{
					CombatPhasePatcher.Instance.VisualizeAimSniperAbility(slot, cardSlot);
				}
				yield return Singleton<BoardManager>.Instance.ChooseTarget(Singleton<BoardManager>.Instance.OpponentSlotsCopy, Singleton<BoardManager>.Instance.OpponentSlotsCopy, delegate (CardSlot s)
				{
					opposingSlots.Add(s);
					CombatPhasePatcher.Instance.VisualizeConfirmSniperAbility(s, isJudge);
				}, null, delegate (CardSlot s)
				{
					CombatPhasePatcher.Instance.VisualizeAimSniperAbility(slot, s);

				}, () => false, isJudge ? CursorType.Sacrifice : CursorType.Target);
			}
			foreach (CardSlot item in opposingSlots)
			{
				Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
				if (isJudge && item.Card != null)
                {
					yield return new WaitForSeconds(0.1f);
					yield return CombatPhasePatcher.Instance.Execution(item.Card);
				}
				else
                {
					yield return Singleton<CombatPhaseManager>.Instance.SlotAttackSlot(slot, item, (opposingSlots.Count > 1) ? 0.1f : 0f);
				}
			}
			Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode);
			Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
			CombatPhasePatcher.Instance.VisualizeClearSniperAbility();
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
			AscensionStatsData.TryIncrementStat(AscensionStat.Type.SacrificesMade);
			target.Anim.PlaySacrificeSound();
			target.Anim.DeactivateSacrificeHoverMarker();
			yield return target.Die(wasSacrifice: false);
		}
	}
}
