using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;

namespace BonniesBakingPack
{
    [HarmonyPatch(typeof(CardMergeSequencer))]
    internal class CardMergePatches
    {
        // Prevents cards from being sacrificed / transferring their sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForSacrifice))]
        private static void RemoveFromValidCardsForSacrifice(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.name == "bbp_bingus");
        }

        // Prevents card from being merged / gaining sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForHost))]
        private static void RemoveFromValidCardsForHost(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.name == "bbp_bingus");
        }
    }

    [HarmonyPatch(typeof(CardStatBoostSequencer))]
    internal class StatBoostPatch
    {
        // Prevents cards from having their stats boostable
        [HarmonyPostfix, HarmonyPatch(nameof(CardStatBoostSequencer.GetValidCards))]
        private static void RemoveFromValidCardsForStatBoost(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.name == "bbp_bingus");
        }
    }
    [HarmonyPatch(typeof(CopyCardSequencer))]
    internal class CopyCardPatch
    {
        [HarmonyPostfix, HarmonyPatch(nameof(CopyCardSequencer.GetValidCards))]
        private static void RemoveFromValidCardsForCopyCard(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.name == "bbp_bingus");
        }
    }
}
