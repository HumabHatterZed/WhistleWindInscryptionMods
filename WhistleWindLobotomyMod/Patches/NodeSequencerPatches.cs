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
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.ModifyHostCard))]
        private static void AddSapSpecialAbility(CardInfo hostCardInfo, CardInfo sacrificeCardInfo)
        {
            if (sacrificeCardInfo.HasSpecialAbility(Sap.specialAbility))
                RunState.Run.playerDeck.ModifyCard(hostCardInfo, new() { specialAbilities = { Sap.specialAbility } });
        }
    }
}
