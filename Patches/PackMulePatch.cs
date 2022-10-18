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
    [HarmonyPatch(typeof(PackMule))]
    public static class PackMulePatch
    {
        // Fixes the PackMule special ability to not break the game when killed before fully resolving (eg, killed by Sentry)

        // The cause of this break is that PackMule's internal 'pack' is not properly instantiated at the time the card dies
        // Simply put, 'pack' is null so we get a null error
        // This patch instantiates 'pack' on death if it hasn't been already
        [HarmonyPostfix, HarmonyPatch(nameof(PackMule.OnDie))]
        public static IEnumerator FixPackMulePackNotExisting(IEnumerator enumerator, PackMule __instance)
        {
            // if pack is null, instantiate it then return the vanilla code
            if (__instance.pack == null)
            {
                // instantiate pack object
                yield return new WaitForSeconds(0.4f);
                GameObject gameObject = Object.Instantiate(ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/CardPack"));
                __instance.pack = gameObject.transform;
                Vector3 position = __instance.PlayableCard.Slot.transform.position;
                Vector3 position2 = position + Vector3.forward * 8f;
                __instance.pack.position = position2;
                Tween.Position(__instance.pack, position, 0.25f, 0f, Tween.EaseOut);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => !Tween.activeTweens.Exists((TweenBase t) => t.targetInstanceID == __instance.PlayableCard.transform.GetInstanceID()));
                __instance.pack.SetParent(__instance.PlayableCard.transform);
            }
            yield return enumerator;
        }

        [HarmonyPostfix, HarmonyPatch(nameof(PackMule.SpawnAndOpenPack))]
        public static IEnumerator FixPackMuleBreakingRealityOnDeath(IEnumerator enumerator, Transform pack)
        {
            // Prevents the game trying to open the pack when it doesn't exist
            if (pack == null)
            {
                yield break;
            }
            yield return enumerator;
        }
        [HarmonyPostfix, HarmonyPatch(nameof(PackMule.OnResolveOnBoard))]
        public static IEnumerator FixPackMuleVisualGlitch(IEnumerator enumerator, PackMule __instance)
        {
            // Prevent the game from running the OnResolve logic when this card is dead (fixes a minor visual glitch)
            if (__instance.PlayableCard.Dead)
            {
                yield break;
            }
            yield return enumerator;
        }
    }
}
