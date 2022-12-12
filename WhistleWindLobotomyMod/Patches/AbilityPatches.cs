using DiskCardGame;
using HarmonyLib;
using System;
using Infiniscryption.Spells.Sigils;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using EasyFeedback.APIs;
using System.Collections.Generic;
using InscryptionAPI.Card;

// Patches to make abilities function properly
namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(PlayableCard))]
    internal class PlayableCardPatches
    {
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.TakeDamage))]
        private static void ModifyTakenDamage(PlayableCard __instance, ref int damage, PlayableCard attacker)
        {
            // damage from non-null sources is negated for downed apostles
            if (attacker != null && __instance.HasAbility(Apostle.ability) && __instance.Info.name.Contains("Down"))
            {
                // only be immune to damage if WhiteNight's on the card's side
                if (HelperMethods.GetSlotsCopy(__instance.OpponentCard).Exists(x => x.name == "wstl_whiteNight"))
                {
                    damage = 0;
                    __instance.Anim.LightNegationEffect();
                }
            }
        }

        [HarmonyPrefix, HarmonyPatch(nameof(PlayableCard.Die))]
        private static bool ApostleTransformOnDie(ref IEnumerator __result, PlayableCard __instance, bool wasSacrifice, PlayableCard killer)
        {
            if (__instance.HasAbility(Apostle.ability))
            {
                // if apostle is killed by Heretic or WhiteNight, do standard Die
                if (killer != null && (killer.Info.name == "wstl_apostleHeretic" || killer.Info.name == "wstl_whiteNight"))
                    return true;

                // if WhiteNight exists on the apostle's side
                if (HelperMethods.GetSlotsCopy(__instance.OpponentCard).Exists(x => x.name == "wstl_whiteNight"))
                {
                    __result = ApostleDie(__instance, wasSacrifice, killer);
                    return false;
                }
            }
            return true;
        }
        private static IEnumerator ApostleDie(PlayableCard instance, bool wasSacrifice, PlayableCard killer)
        {
            // play hit anim then trigger Die
            instance.Anim.PlayHitAnimation();
            instance.Anim.SetShielded(shielded: false);
            yield return instance.Anim.ClearLatchAbility();
            if (instance.TriggerHandler.RespondsToTrigger(Trigger.Die, wasSacrifice, killer))
            {
                yield return instance.TriggerHandler.OnTrigger(Trigger.Die, wasSacrifice, killer);
            }
            yield break;
        }
    }
}
