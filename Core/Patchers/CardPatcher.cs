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
    public static class CardPatcher
    {
        // Adds select Kaycee Mod sigils to the Part 1 rulebook
        [HarmonyPatch(typeof(RuleBookInfo), nameof(RuleBookInfo.AbilityShouldBeAdded))]
        [HarmonyPostfix]
        private static void AddKayceeAbilities(ref int abilityIndex, ref AbilityMetaCategory rulebookCategory, ref bool __result)
        {
            AbilityInfo info = AbilitiesUtil.GetInfo((Ability)abilityIndex);
            if (!SaveFile.IsAscension && info.metaCategories.Contains(AbilityMetaCategory.AscensionUnlocked))
            {
                if (info.name.Equals("BoneDigger") || //info.name.Equals("DeathShield") ||
                    info.name.Equals("DoubleStrike") || //info.name.Equals("OpponentBones")
                    info.name.Equals("StrafeSwap") || info.name.Equals("Morsel"))
                {
                    __result = true;
                }
            }
        }
        // Adds Nothing There to the deck when chosen in a card choice (Trader, Boss Box, etc.)
        [HarmonyPatch(typeof(DeckInfo), nameof(DeckInfo.AddCard))]
        [HarmonyPrefix]
        public static void AddNothing(ref CardInfo card)
        {
            if (card.Mods.Exists((CardModificationInfo x) => x.singletonId == "wstl_nothingThere"))
            {
                card = CardLoader.GetCardByName("wstl_nothingThere");
            }
        }
        // Makes WhiteNight, its Apostles, and Hundreds of Good Deeds immune to Touch of Death
        // Effectively gives them Made of Stone but without the whole 'they're not made of stone' thing
        [HarmonyPatch(typeof(Deathtouch), nameof(Deathtouch.RespondsToDealDamage))]
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
        // Controls custom emission rules for added cards
        // E.g., forced emissions (always glowy), custom colours
        [HarmonyPatch(typeof(Card), nameof(Card.ApplyAppearanceBehaviours))]
        [HarmonyPostfix]
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
            //__instance.RenderInfo.forceEmissivePortrait = true;
        }
    }
}
