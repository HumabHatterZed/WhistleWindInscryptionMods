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
namespace WhistleWindLobotomyMod
{
	[HarmonyPatch(typeof(VariableStatBehaviour))]
	public static class VariableStatBehaviourPatch
	{
		[HarmonyPrefix, HarmonyPatch(nameof(VariableStatBehaviour.UpdateStats))]
		public static bool ShowStatsOnUpdate(ref VariableStatBehaviour __instance)
		{
			// if a spell that displays stats, show when in hand or on board
			if (__instance.PlayableCard.HasAnyOfAbilities(TargetGainStats.ability, TargetGainStatsSigils.ability))
            {
				int[] array = new int[2];
				if (__instance.PlayableCard.InHand || __instance.PlayableCard.OnBoard)
				{
					array = __instance.GetStatValues();
					__instance.statsMod.attackAdjustment = array[0];
					__instance.statsMod.healthAdjustment = array[1];
				}
				if (!__instance.StatValuesEqual(__instance.prevStatValues, array) || __instance.prevOnBoard != __instance.PlayableCard.OnBoard)
				{
					__instance.PlayableCard.RenderInfo.showSpecialStats = __instance.PlayableCard.InHand || __instance.PlayableCard.OnBoard;
					__instance.PlayableCard.OnStatsChanged();
				}
				__instance.prevStatValues = array;
				__instance.prevOnBoard = __instance.PlayableCard.OnBoard;
				return false;
			}
			return true;
		}

	}

	[HarmonyPatch(typeof(TargetedSpellAbility))]
	public static class TargetedSpellAbilityPatch
    {
		[HarmonyPostfix, HarmonyPatch(nameof(TargetedSpellAbility.GetStatValues))]
		public static void ShowTargetedStats(ref int[] __result, ref TargetedSpellAbility __instance)
        {
			// if has spell that shows stats, update stat values
			if (__instance.PlayableCard.HasAnyOfAbilities(TargetGainStats.ability, TargetGainStatsSigils.ability))
            {
				__instance.PlayableCard.Info.hideAttackAndHealth = false;
				__result = new int[2] { __instance.PlayableCard.Info.Attack, __instance.PlayableCard.Info.Health };
            }
		}

	}
	/*
	[HarmonyPatch(typeof(SpellBehavior))]
	public static class SpellBehaviourPatch
	{
		// Adds custom special abilities to bool that triggers spell behaviour
		[HarmonyPostfix, HarmonyPatch(nameof(SpellBehavior.IsSpell))]
		public static void ExpandSpellDefinition(ref bool __result, CardInfo card)
		{
			// Alter the bool to TRUE if the card has any of the below abilities
			if (card.SpecialAbilities.Any((SpecialTriggeredAbility ab) => ab == TargetGainsStats.specialAbility || ab == TargetGainsSigils.specialAbility || ab == TargetGainsStatsSigils.specialAbility))
            {
				__result = true;
            }
		}

		[HarmonyPostfix, HarmonyPatch(nameof(SpellBehavior.IsTargetedSpell))]
		public static void MoreTargetedSpellTypes(ref bool __result, CardInfo card)
		{
			// Alter the bool to TRUE if the card has any of the below abilities
			if (card.SpecialAbilities.Any((SpecialTriggeredAbility ab) => ab == TargetGainsStats.specialAbility || ab == TargetGainsSigils.specialAbility || ab == TargetGainsStatsSigils.specialAbility))
			{
				__result = true;
			}
		}
		[HarmonyPostfix, HarmonyPatch(nameof(SpellBehavior.IsValidTarget))]
		public static void FixValidTargeting(ref bool __result, PlayableCard card)
		{
			if (!card.OpponentCard)
            {
				// Alter the bool to TRUE if the card has any of the below abilities
				if (card.Info.SpecialAbilities.Any((SpecialTriggeredAbility ab) => ab == TargetGainsStats.specialAbility || ab == TargetGainsSigils.specialAbility || ab == TargetGainsStatsSigils.specialAbility))
				{
					__result = true;
				}
			}
		}

	}
	*/
}
