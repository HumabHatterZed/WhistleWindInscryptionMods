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
        [HarmonyPatch(typeof(CardMergeSequencer), "GetValidCardsForHost")]
        [HarmonyPostfix]
        public static void RemoveFromValidCardsForHost(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.SpecialAbilities.Contains(NothingThere.specialAbility));
            __result.RemoveAll((CardInfo x) => x.name.ToLowerInvariant().Contains("plaguedoctor"));
        }

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
        // Update: I got too liberal with the 'make everything glowy' button
        [HarmonyPatch(typeof(Card), "ApplyAppearanceBehaviours")]
        [HarmonyPostfix]
        public static void ForcedEmissions(ref Card __instance)
        {
            switch (__instance.Info.name.ToLowerInvariant())
            {
                case "wstl_spiderling":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.glowRed);
                    __instance.RenderCard();
                    break;
                case "wstl_spiderbrood":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.glowRed);
                    __instance.RenderCard();
                    break;
                case "wstl_whitenight":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    __instance.RenderCard();
                    break;
                case "wstl_apostlescythe":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    __instance.RenderCard();
                    break;
                case "wstl_apostlescythedown":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    __instance.RenderCard();
                    break;
                case "wstl_apostlestaff":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    __instance.RenderCard();
                    break;
                case "wstl_apostlestaffdown":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    __instance.RenderCard();
                    break;
                case "wstl_apostlespear":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    __instance.RenderCard();
                    break;
                case "wstl_apostlespeardown":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    __instance.RenderCard();
                    break;
                case "wstl_apostleheretic":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    __instance.RenderCard();
                    break;
                case "wstl_hundredsgooddeeds":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    __instance.RenderCard();
                    break;
                default: // for testing only
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.RenderCard();
                    break; /* */
            }
        }
    }
}
