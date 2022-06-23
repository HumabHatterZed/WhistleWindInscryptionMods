using InscryptionAPI;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public static class CardPatcher
    {
        // Removes certain cards from valid pool of hosts for card merges
        [HarmonyPatch(typeof(CardMergeSequencer), "GetValidCardsForHost")]
        [HarmonyPostfix]
        public static void RemoveFromValidCardsForHost(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.SpecialAbilities.Contains(NothingThere.specialAbility));
        }

        // Removes certain cards from valid pool of sacrifices for card merges
        [HarmonyPatch(typeof(CardMergeSequencer), "GetValidCardsForSacrifice")]
        [HarmonyPostfix]
        public static void RemoveFromValidCardsForSacrifice(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.SpecialAbilities.Contains(NothingThere.specialAbility));
            __result.RemoveAll((CardInfo x) => x.name.ToLowerInvariant().Contains("plaguedoctor"));
        }

        // Makes WhiteNight, its Apostles, and Hundreds of Good Deeds immune to Touch of Death
        // Effectively gives them Made of Stone but without the whole 'they're not made of stone' thing
        [HarmonyPatch(typeof(Deathtouch), "RespondsToDealDamage")]
        [HarmonyPrefix]
        public static bool ImmunetoDeathTouch(ref int amount, ref PlayableCard target)
        {
            bool whiteNightEvent = !target.HasAbility(TrueSaviour.ability) && !target.HasAbility(Apostle.ability) && !target.HasAbility(Confession.ability);
            if (amount > 0 && target != null && !target.Dead)
            {
                return whiteNightEvent && !target.HasAbility(Ability.MadeOfStone);
            }
            return false;
        }

        // Forces cards to render their emissive texture
        // Only for a select few cards
        // Let's not get too liberal with the 'make everything glowy' button
        // Might be a better way of doing this, but oh well
        [HarmonyPatch(typeof(Card), "ApplyAppearanceBehaviours")]
        [HarmonyPostfix]
        public static void ForcedEmissions(ref Card __instance)
        {
            string instanceName = __instance.Info.name.ToLowerInvariant();
            List<Ability> instanceAbilities = __instance.Info.abilities;

            if (instanceName == "wstl_spiderling" || instanceName == "wstl_spiderbrood")
            {
                __instance.RenderInfo.forceEmissivePortrait = true;
                __instance.StatsLayer.SetEmissionColor(GameColors.Instance.glowRed);
                __instance.RenderCard();
            }

            if (instanceAbilities.Contains(TrueSaviour.ability) ||
                instanceAbilities.Contains(Apostle.ability) ||
                instanceAbilities.Contains(Confession.ability)
                )
            {
                __instance.RenderInfo.forceEmissivePortrait = true;
                __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                __instance.RenderCard();
            }

            // For testing emissions
            __instance.RenderInfo.forceEmissivePortrait = true;
            __instance.RenderCard();
        }
    }
}
