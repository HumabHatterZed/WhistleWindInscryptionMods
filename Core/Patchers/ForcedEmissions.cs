using APIPlugin;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public static class ForcedEmissions
    {
        // Forces cards to render their emissive texture
        // Only for a select few cards
        // Let's not get too liberal with the 'make everything glowy' button
        [HarmonyPatch(typeof(Card), "ApplyAppearanceBehaviours")]
        [HarmonyPostfix]
        public static void Emissions(ref Card __instance)
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
            }
        }
    }
}
