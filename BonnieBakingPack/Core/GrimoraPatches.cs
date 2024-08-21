using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using GrimoraMod;
using HarmonyLib;
using Infiniscryption.PackManagement;
using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BonniesBakingPack
{
    public static class GrimoraPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(ActivatedDealDamageGrimora), nameof(ActivatedDealDamageGrimora.Activate))]
        private static IEnumerator DeadtectiveDealDamage(IEnumerator enumerator, ActivatedDealDamageGrimora __instance)
        {
            bool panda = __instance.Card.HasSpecialAbility(PandaAbility.SpecialAbility);
            
            if (panda) __instance.Card.SwitchToAlternatePortrait();
            yield return enumerator;
            if (panda) __instance.Card.SwitchToDefaultPortrait();
        }

        [HarmonyPrefix, HarmonyPatch(typeof(GraveControllerExt), "PlayAttackAnimation")]
        private static bool DeadtectiveFingerGun(GraveControllerExt __instance, CardSlot targetSlot)
        {
            PlayableCard _playableCard = (PlayableCard)typeof(GraveControllerExt).GetField("_playableCard", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            if (_playableCard.HasSpecialAbility(PandaAbility.SpecialAbility))
            {
                //bool impactFrameReached = false;
                _playableCard.Anim.PlaySpecificAttackAnimation("attack_sentry", attackPlayer: false, targetSlot, delegate
                {

                });
                return false;
            }
            return true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ElectricChairSequencer), "GetValidCards")]
        private static void RemoveEternalLady(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.name == "bbp_eternalLady");
        }
    }
}
