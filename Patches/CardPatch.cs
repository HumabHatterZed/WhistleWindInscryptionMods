using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    [HarmonyPatch(typeof(Card))]
    public static class CardPatch
    {
        // Controls custom emission rules for added cards
        // E.g., forced emissions (always glowy), custom colours
        [HarmonyPostfix, HarmonyPatch(nameof(Card.ApplyAppearanceBehaviours))]
        public static void CustomEmissions(ref Card __instance)
        {
            string instanceName = __instance.Info.name.ToLowerInvariant();
            switch(instanceName)
            {
                case "wstl_apostleheretic":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    break;
                case "wstl_apostlescythe":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    break;
                case "wstl_apostlescythedown":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    break;
                case "wstl_apostlespear":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    break;
                case "wstl_apostlespeardown":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    break;
                case "wstl_apostlestaff":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    break;
                case "wstl_apostlestaffdown":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    break;
                case "wstl_backwardclock":
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    break;
                case "blue_star2":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    break;
                case "wstl_hundredsgooddeeds":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    break;
                case "wstl_spiderbrood":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.glowRed);
                    break;
                case "wstl_spiderling":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.glowRed);
                    break;
                case "wstl_whitenight":
                    __instance.RenderInfo.forceEmissivePortrait = true;
                    __instance.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
                    break;
            }
            // For testing emissions
            __instance.RenderInfo.forceEmissivePortrait = true;
        }
    }
}
