using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Infiniscryption.Spells.Patchers;
using Infiniscryption.Spells.Sigils;
using System;

namespace WhistleWindLobotomyMod
{
	[HarmonyPatch(typeof(Deathtouch))]
    public static class DeathtouchPatch
    {
        // Makes WhiteNight and Hundred Deeds immune to Deathtouch
        [HarmonyPrefix, HarmonyPatch(nameof(Deathtouch.RespondsToDealDamage))]
        public static bool ImmunetoDeathTouch(ref int amount, ref PlayableCard target)
        {
            bool whiteNightEvent = target.HasAbility(TrueSaviour.ability) || target.HasAbility(Confession.ability);
            if (amount > 0 && target != null && !target.Dead)
            {
                return !whiteNightEvent && !target.HasAbility(Ability.MadeOfStone);
            }
            return false;
        }
    }
	[HarmonyPatch(typeof(PlayableCard))]
	public static class PlayableCardPatches
	{
		// Adds in logic for Prudence, Protector, Thick Skin
		[HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.TakeDamage))]
		public static void ModifyTakenDamage(PlayableCard __instance, ref int damage)
		{
			// Count number of adjacent cards with Protector
			int protector = Singleton<BoardManager>.Instance.GetAdjacentSlots(__instance.Slot).Where((CardSlot s) => s.Card != null && s.Card.HasAbility(Protector.ability)).Count();

			// Count number of stacks of Thick Skin
			int skin = __instance.GetAbilityStacks(ThickSkin.ability);

			// Count amount of Prudence this card has
			int prudence = !(__instance.Info.GetExtendedPropertyAsInt("wstl:Prudence") != null) ? 0 : (int)__instance.Info.GetExtendedPropertyAsInt("wstl:Prudence");

			damage += prudence - (protector + skin);
			if (damage < 0)
				damage = 0;
		}
		// Fixes All Strike to attack the opposing space instead of just slot[0] for non-giant player cards
		// Also re-adds Moon Strike functionality for Blue Star cards
		[HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.GetOpposingSlots))]
		public static List<CardSlot> AllStrikeFix(List<CardSlot> list, PlayableCard __instance)
		{
			if (__instance.HasAbility(Ability.AllStrike) && !__instance.HasTrait(Trait.Giant))
			{
                // reset list (may not work, but eh)
                list = new();
				ProgressionData.SetAbilityLearned(Ability.AllStrike);
				List<CardSlot> list2 = Singleton<BoardManager>.Instance.OpponentSlotsCopy;
				if (list2.Exists((CardSlot x) => x.Card != null && !__instance.CanAttackDirectly(x)))
				{
					foreach (CardSlot item in list2)
					{
						if (item.Card != null && !__instance.CanAttackDirectly(item))
						{
							list.Add(item);
						}
					}
				}
				else
				{
					if (__instance.Info.name.ToLowerInvariant().Contains("bluestar"))
					{
						foreach (CardSlot item in list2)
						{
							if (__instance.CanAttackDirectly(item))
							{
								list.Add(item);
							}
						}
						return list;
					}
					list.Add(__instance.Slot.opposingSlot);
				}
				if (__instance.HasAbility(Ability.SplitStrike))
				{
					ProgressionData.SetAbilityLearned(Ability.SplitStrike);
					list.AddRange(Singleton<BoardManager>.Instance.GetAdjacentSlots(__instance.Slot.opposingSlot));
				}
				if (__instance.HasTriStrike())
				{
					ProgressionData.SetAbilityLearned(Ability.TriStrike);
					list.AddRange(Singleton<BoardManager>.Instance.GetAdjacentSlots(__instance.Slot.opposingSlot));
					if (!list.Contains(__instance.Slot.opposingSlot))
					{
						list.Add(__instance.Slot.opposingSlot);
					}
				}
				if (__instance.HasAbility(Ability.DoubleStrike))
				{
					ProgressionData.SetAbilityLearned(Ability.DoubleStrike);
					list.Add(__instance.slot.opposingSlot);
				}
				list.Sort((CardSlot a, CardSlot b) => a.Index - b.Index);
			}
			return list;
		}
	}

	// Triggers card with Fungal Infector before other cards
	[HarmonyPatch(typeof(GlobalTriggerHandler))]
	public static class GlobalTriggerHandlerPatches
    {
		[HarmonyPostfix, HarmonyPatch(nameof(GlobalTriggerHandler.TriggerCardsOnBoard))]
		public static IEnumerator ChangeSporePriority(IEnumerator enumerator, GlobalTriggerHandler __instance, Trigger trigger, bool triggerFacedown, params object[] otherArgs)
        {
			if (trigger == Trigger.TurnEnd)
            {
				List<PlayableCard> list = new List<PlayableCard>(Singleton<BoardManager>.Instance.CardsOnBoard);
				if (list.Where(item => item.HasAbility(Spores.ability)).Count() > 0)
                {
					WstlPlugin.Log.LogDebug("Triggering Fungal Infector before other cards.");
					yield return __instance.TriggerNonCardReceivers(beforeCards: true, trigger, otherArgs);

					// Trigger remaining Spore cards
					foreach (PlayableCard item in list)
					{
						if (item != null && item.HasAbility(Spores.ability) && (!item.FaceDown || triggerFacedown) && item.TriggerHandler.RespondsToTrigger(trigger, otherArgs))
						{
							yield return item.TriggerHandler.OnTrigger(trigger, otherArgs);
						}
					}

					// Trigger remaining cards
					foreach (PlayableCard item in list)
					{
						if (item != null && item.LacksAbility(Spores.ability) && (!item.FaceDown || triggerFacedown) && item.TriggerHandler.RespondsToTrigger(trigger, otherArgs))
						{
							yield return item.TriggerHandler.OnTrigger(trigger, otherArgs);
						}
					}

					yield return __instance.TriggerNonCardReceivers(beforeCards: false, trigger, otherArgs);
					yield break;
				}
            }
			yield return enumerator;
		}
	}
}
