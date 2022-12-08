using WhistleWind.Core.Helpers;
using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;

// Patches to make abilities function properly
namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(PlayableCard))]
    internal class PlayableCardPatches
    {
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.TakeDamage))]
        private static void ModifyTakenDamage(ref PlayableCard __instance, ref int damage, PlayableCard attacker)
        {
            if (__instance.HasAbility(Apostle.ability))
            {
                if (damage >= __instance.Health)
                {
                    int damageToOne = __instance.Health - 1;
                    damage = __instance.Health - damageToOne;
                }
            }
        }

        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.GetOpposingSlots))]
        private static List<CardSlot> BlueStarAbility(List<CardSlot> list, PlayableCard __instance)
        {
            // Blue Star gets old Omni Strike functionality
            // Make sure we have AllStrike still (haven't lost it)
            if (__instance.Info.name.StartsWith("wstl_blueStar") && __instance.HasAbility(Ability.AllStrike))
            {
                ProgressionData.SetAbilityLearned(Ability.AllStrike);
                List<CardSlot> list2 = HelperMethods.GetSlotsCopy(!__instance.OpponentCard);

                // if there's an attackable card, return original list
                if (list2.Exists((x) => x.Card != null && !__instance.CanAttackDirectly(x)))
                    return list;

                // otherwise return the entire opposing side
                list2.Sort((a, b) => a.Index - b.Index);
                return list2;
            }
            return list;
        }
    }
}
