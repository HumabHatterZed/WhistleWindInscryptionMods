using InscryptionAPI;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pixelplacement;
using Pixelplacement.TweenSystem;

namespace WhistleWindLobotomyMod
{
/*    [HarmonyPatch(typeof(CardGainAbility))]
    public static class CardGainAbilityPatch
    {
        // Fixes a softlock that occurs when the opponent has a totem and a card dies via sentry
        // 
        // Adds a check for whether the card is dead or not
        [HarmonyPrefix, HarmonyPatch(nameof(CardGainAbility.RespondsToOtherCardAssignedToSlot))]
        public static bool RespondsToOtherCardAssignedToSlotNullCheck(ref PlayableCard otherCard)
        {
            if (!otherCard.Dead && otherCard.Slot.IsPlayerSlot && Singleton<CardGainAbility>.Instance.RespondsToOtherCardDrawn(otherCard) && !otherCard.HasAbility(Singleton<TotemTriggerReceiver>.Instance.Data.bottom.effectParams.ability))
            {
                return !otherCard.Info.Mods.Exists((CardModificationInfo x) => x.fromEvolve);
            }
            return false;
        }
    }*/
    [HarmonyPatch(typeof(Strafe))]
    public static class StrafePatch
    {
        // Adds a check for whether the card instance is null or not
        [HarmonyPostfix, HarmonyPatch(nameof(Strafe.PostSuccessfulMoveSequence))]
        public static IEnumerator PostSuccessfulMoveSequenceNullCheck(IEnumerator enumerator, PlayableCard __instance)
        {
            if (!(__instance != null))
            {
                yield break;
            }
            yield return enumerator;
        }
    }
    [HarmonyPatch(typeof(AbilityBehaviour))]
    public static class AbilityBehaviourPatch
    {
        // Adds a check for whether the ability behaviour instance is null or not
        [HarmonyPostfix, HarmonyPatch(nameof(AbilityBehaviour.PreSuccessfulTriggerSequence))]
        public static IEnumerator PreSuccessfulTriggerSequenceNullCheck(IEnumerator enumerator, AbilityBehaviour __instance)
        {
            if (!(__instance != null))
                yield break;

            yield return enumerator;
        }
        // Adds a check for whether the ability behaviour instance is null or not
        [HarmonyPostfix, HarmonyPatch(nameof(AbilityBehaviour.LearnAbility))]
        public static IEnumerator LearnAbilityNullCheck(IEnumerator enumerator, AbilityBehaviour __instance)
        {
            if (!(__instance != null))
                yield break;

            yield return enumerator;
        }
    }
}
