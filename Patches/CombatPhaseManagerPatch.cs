using InscryptionAPI;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		// Adds Marksman ability support, as well as support for opponent sniper/marksman
		[HarmonyPostfix, HarmonyPatch(nameof(CombatPhaseManager.SlotAttackSequence))]
		public static IEnumerator SlotAttackSequence(IEnumerator enumerator, CombatPhaseManager __instance, CardSlot slot)
		{
			bool hasSniper = slot.Card.HasAbility(Ability.Sniper);
			bool hasMarksman = slot.Card.HasAbility(Marksman.ability);
			// If does not have Marksman or Sniper, return normal sequence
			if (!hasMarksman && !hasSniper)
			{
				yield return enumerator;
				yield break;
			}
						bool isJudge = slot.Card.Info.name.Equals("wstl_judgementBird");
			// Change to combat view
			Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
			Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;

			// Create list of targets to attack
			List<CardSlot> targetedSlots = new();

			// Default to 1 attack
			int numAttacks = 1;
			if (slot.Card.HasTriStrike())
			{
				// Add 2 for TriStrike (total 3)
				numAttacks += 2;
			}
			if (slot.Card.HasAbility(Ability.SplitStrike))
			{
				// Add 1 for BiStrike (total 2)
				numAttacks++;
			}
			if(slot.Card.HasAbility(Ability.DoubleStrike))
            {
				// Add 1 for Double Strike
				numAttacks++;
            }
			if(slot.Card.HasAbility(Ability.AllStrike) && slot.Card.Info.name.StartsWith("wstl_blueStar"))
            {
				// Add 1 for every opposing card - 1, or 3 if no opposing cards (4 total)
				int opposingSlotsCount = Singleton<BoardManager>.Instance.GetSlots(slot.Card.OpponentCard).Where(s => s.Card != null).ToList().Count();
				if (opposingSlotsCount != 0)
                {
					numAttacks += opposingSlotsCount - 1;
                }
				else
                {
					numAttacks += 3;
                }
            }
			if (hasSniper && hasMarksman)
			{
				// Add 1 if both Sniper and Marksman are present
				numAttacks++;
			}

			// if card is an opponent
			if (slot.Card.OpponentCard)
			{
				List<CardSlot> validSlots = Singleton<BoardManager>.Instance.PlayerSlotsCopy;
				List<CardSlot> fullPlayerSlots = validSlots.Where((CardSlot s) => s.Card != null).ToList();
				List<CardSlot> emptyPlayerSlots = validSlots.Where((CardSlot s) => !(s.Card != null)).ToList();
				
				int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<TurnManager>.Instance.TurnNumber;

				// Attack a random null slot if this card can win the battle
				if (emptyPlayerSlots.Count() > 0 && slot.Card.Attack >= (10 - Singleton<LifeManager>.Instance.DamageUntilPlayerWin))
				{
					CardSlot cardSlot = emptyPlayerSlots[SeededRandom.Range(0, emptyPlayerSlots.Count(), randomSeed)];
					if (cardSlot != null && targetedSlots.Contains(cardSlot))
					{
						CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(slot, cardSlot);
					}
					targetedSlots.Add(cardSlot);
					CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(cardSlot, isJudge);
					numAttacks--;
				}

				// Attack all cards that can lose the battle for this card
				if (numAttacks > 0 && fullPlayerSlots.Count() > 0)
                {
					foreach (CardSlot item in fullPlayerSlots.Where(ss => !ss.Card.AttackIsBlocked(ss.opposingSlot) && ss.Card.Attack >= Singleton<LifeManager>.Instance.DamageUntilPlayerWin))
                    {
						if (numAttacks > 0)
                        {
							CombatPhaseManagerPatch.Instance.VisualizeStartSniperAbility(slot);
							if (item != null && targetedSlots.Contains(item))
							{
								CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(slot, item);
							}
							targetedSlots.Add(item);
							CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(item, isJudge);
							numAttacks--;
							if (numAttacks > 0 && item.Card.Health > slot.Card.Attack)
                            {
								CombatPhaseManagerPatch.Instance.VisualizeStartSniperAbility(slot);
								if (item != null && targetedSlots.Contains(item))
								{
									CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(slot, item);
								}
								targetedSlots.Add(item);
								CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(item, isJudge);
								numAttacks--;
							}
						}
                    }
                }

				// Attack the opposing card if it can kill this card
				if (numAttacks > 0 && slot.opposingSlot.Card != null && slot.opposingSlot.Card.Attack >= slot.Card.Health)
				{
					// check if opposing card has Sharp, Punisher, Serpent's Nest or Reflector
					bool opposingCanRetaliate = slot.opposingSlot.Card.HasAbility(Ability.Sharp) || slot.opposingSlot.Card.HasAbility(Punisher.ability) ||
						slot.opposingSlot.Card.HasAbility(SerpentsNest.ability) || slot.opposingSlot.Card.HasAbility(Reflector.ability);

					// if the opposing card can't retaliate or if it can but this card can kill it, add it to targets
					if (!opposingCanRetaliate || (opposingCanRetaliate && slot.Card.Attack >= slot.opposingSlot.Card.Health))
					{
						CombatPhaseManagerPatch.Instance.VisualizeStartSniperAbility(slot);
						if (slot.opposingSlot != null && targetedSlots.Contains(slot.opposingSlot))
						{
							CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(slot, slot.opposingSlot);
						}
						targetedSlots.Add(slot.opposingSlot);
						CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(slot.opposingSlot, isJudge);
						numAttacks--;
					}
				}
				// Attack a random slot with a bias towards cards
				for (int i = 0; i < numAttacks; i++)
                {
					CombatPhaseManagerPatch.Instance.VisualizeStartSniperAbility(slot);
					// default to an empty slot
					CardSlot cardSlot = emptyPlayerSlots.Count() > 0 ? emptyPlayerSlots[SeededRandom.Range(0, emptyPlayerSlots.Count(), randomSeed)] : validSlots[0];
					randomSeed++;

					// if there are valid cards, change target to one of them at a 75% chance
					if (fullPlayerSlots.Count() > 0 && SeededRandom.Range(0, 4, randomSeed) > 0)
                    {
						cardSlot = fullPlayerSlots[SeededRandom.Range(0, fullPlayerSlots.Count(), randomSeed)];
					}

					if (cardSlot != null && targetedSlots.Contains(cardSlot))
					{
						CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(slot, cardSlot);
					}
					targetedSlots.Add(cardSlot);
					CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(cardSlot, isJudge);
				}

				// for each target slot, do attack thing
				foreach (CardSlot item in targetedSlots)
				{
					Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
					if (isJudge && item.Card != null && !item.Card.AttackIsBlocked(slot))
					{
						if (slot.Card.FaceDown)
						{
							slot.Card.SetFaceDown(false);
							slot.Card.UpdateFaceUpOnBoardEffects();
							yield return new WaitForSeconds(0.2f);
						}
						slot.Card.Anim.StrongNegationEffect();
						yield return new WaitForSeconds(0.4f);
						yield return CombatPhaseManagerPatch.Instance.Execution(item.Card);
					}
					else
					{
						yield return Singleton<CombatPhaseManager>.Instance.SlotAttackSlot(slot, item, (targetedSlots.Count > 1) ? 0.1f : 0f);
					}
				}
				// End sequence
				CombatPhaseManagerPatch.Instance.VisualizeClearSniperAbility();
				yield break;
			}

			// Non-opponent sequence
			Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode);
			Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
			for (int i = 0; i < numAttacks; i++)
			{
				CombatPhaseManagerPatch.Instance.VisualizeStartSniperAbility(slot);
				CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;

				if (cardSlot != null && targetedSlots.Contains(cardSlot))
				{
					CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(slot, cardSlot);
				}
				yield return Singleton<BoardManager>.Instance.ChooseTarget(Singleton<BoardManager>.Instance.OpponentSlotsCopy, Singleton<BoardManager>.Instance.OpponentSlotsCopy, delegate (CardSlot s)
				{
					targetedSlots.Add(s);
					CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(s, isJudge);
				}, null, delegate (CardSlot s)
				{
					CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(slot, s);

				}, () => false, isJudge ? CursorType.Sacrifice : CursorType.Target);
			}
			foreach (CardSlot item in targetedSlots)
			{
				Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
				if (isJudge && item.Card != null && !item.Card.AttackIsBlocked(slot))
                {
					if (slot.Card.FaceDown)
					{
						slot.Card.SetFaceDown(false);
						slot.Card.UpdateFaceUpOnBoardEffects();
						yield return new WaitForSeconds(0.2f);
					}
					slot.Card.Anim.StrongNegationEffect();
					yield return new WaitForSeconds(0.4f);
					yield return CombatPhaseManagerPatch.Instance.Execution(item.Card);
				}
				else
                {
					yield return Singleton<CombatPhaseManager>.Instance.SlotAttackSlot(slot, item, (targetedSlots.Count > 1) ? 0.1f : 0f);
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
		}
	}
}
