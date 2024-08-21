using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(CardChoicesSequencer))]
    internal class CardChoicePatch
    {
        [HarmonyPrefix, HarmonyPatch(nameof(CardChoicesSequencer.ExamineCardWithDialogue))]
        private static void ShowNothingThereDialogue(SelectableCard card, ref string message)
        {
            // if this isn't a disguised Nothing There, or is just the fallback Nothing There
            if (card?.Info == null || card.Info.LacksSpecialAbility(Mimicry.specialAbility) || card.Info.name == "wstl_nothingThere")
                return;

            // use final forme as dummy for the introduced bool
            CardInfo info = CardLoader.GetCardByName("wstl_nothingThereFinal");
            if (!ProgressionData.IntroducedCard(info))
            {
                message = "How did that get there?";
                ProgressionData.SetCardIntroduced(info);
            }
        }
    }
    [HarmonyPatch(typeof(CardMergeSequencer))]
    internal class CardMergePatches
    {
        // Prevents cards from being sacrificed / transferring their sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForSacrifice))]
        private static void RemoveFromValidCardsForSacrifice(ref List<CardInfo> __result)
        {
            RemoveInvalidCards(__result);
        }

        // Prevents card from being merged / gaining sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForHost))]
        private static void RemoveFromValidCardsForHost(ref List<CardInfo> __result)
        {
            RemoveInvalidCards(__result);
        }
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.ModifyHostCard))]
        private static void AddSapSpecialAbility(CardInfo hostCardInfo, CardInfo sacrificeCardInfo)
        {
            if (sacrificeCardInfo.HasSpecialAbility(Sap.specialAbility))
                RunState.Run.playerDeck.ModifyCard(hostCardInfo, new() { specialAbilities = { Sap.specialAbility } });
        }

        internal static void RemoveInvalidCards(List<CardInfo> result)
        {
            result.RemoveAll(x => x.HasSpecialAbility(Mimicry.specialAbility)
            || x.HasAnyOfAbilities(TheTrain.ability, TimeMachine.ability));
        }
    }

    [HarmonyPatch(typeof(CardStatBoostSequencer))]
    internal class StatBoostPatch
    {
        // Prevents cards from having their stats boostable
        [HarmonyPostfix, HarmonyPatch(nameof(CardStatBoostSequencer.GetValidCards))]
        private static void RemoveFromValidCardsForStatBoost(ref List<CardInfo> __result)
        {
            CardMergePatches.RemoveInvalidCards(__result);
        }
    }
    [HarmonyPatch(typeof(CopyCardSequencer))]
    internal class CopyCardPatch
    {
        [HarmonyPostfix, HarmonyPatch(nameof(CopyCardSequencer.GetValidCards))]
        private static void RemoveFromValidCardsForCopyCard(ref List<CardInfo> __result)
        {
            CardMergePatches.RemoveInvalidCards(__result);
        }
    }
}
