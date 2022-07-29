﻿using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    [HarmonyPatch(typeof(PlayableCard))]
    public static class TakeDamagePatch
    {
        // Increases damage taken by amount of Prudence a card has
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.TakeDamage))]
        public static void TakePrudenceDamage(PlayableCard __instance, ref int damage)
        {
            int prudence = !(__instance.Info.GetExtendedPropertyAsInt("wstl:Prudence") != null) ? 0 : (int)__instance.Info.GetExtendedPropertyAsInt("wstl:Prudence");
            if (prudence > 0)
            {
                damage += prudence;
            }
        }
        // Fixes All Strike to attack the opposing space instead of just slot[0]
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.GetOpposingSlots))]
        public static List<CardSlot> AllStrikeFix(List<CardSlot> list, PlayableCard __instance)
        {
            if (__instance.HasAbility(Ability.AllStrike) && !__instance.OpponentCard && !__instance.HasTrait(Trait.Giant))
            {
				list = new List<CardSlot>();
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
				list.Add(__instance.Slot.opposingSlot);
				if (__instance.HasAbility(Ability.SplitStrike))
				{
					ProgressionData.SetAbilityLearned(Ability.SplitStrike);
					list.Remove(__instance.Slot.opposingSlot);
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
}