using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(CardMergeSequencer))]
    internal class CardMergePatches
    {
        // Prevents cards from being sacrificed / transferring their sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForSacrifice))]
        private static void RemoveFromValidCardsForSacrifice(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.HasCardMetaCategory(AbnormalPlugin.CannotGiveSigils)
            || x.HasSpecialAbility(Mimicry.specialAbility)
            || x.HasAnyOfAbilities(TheTrain.ability, TimeMachine.ability));
        }

        // Prevents card from being merged / gaining sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForHost))]
        private static void RemoveFromValidCardsForHost(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.HasCardMetaCategory(AbnormalPlugin.CannotGainSigils) ||
            x.HasSpecialAbility(Mimicry.specialAbility) ||
            x.HasAnyOfAbilities(TheTrain.ability, TimeMachine.ability));
        }
    }

    [HarmonyPatch(typeof(CardStatBoostSequencer))]
    internal class StatBoostPatch
    {
        // Prevents cards from having their stats boostable
        [HarmonyPostfix, HarmonyPatch(nameof(CardStatBoostSequencer.GetValidCards))]
        private static void RemoveFromValidCardsForStatBoost(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.HasCardMetaCategory(AbnormalPlugin.CannotBoostStats) ||
            x.HasSpecialAbility(Mimicry.specialAbility) ||
            x.HasAnyOfAbilities(TheTrain.ability, TimeMachine.ability));
        }
    }
    [HarmonyPatch(typeof(CopyCardSequencer))]
    internal class CopyCardPatch
    {
        [HarmonyPostfix, HarmonyPatch(nameof(CopyCardSequencer.GetValidCards))]
        private static void RemoveFromValidCardsForCopyCard(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.HasCardMetaCategory(AbnormalPlugin.CannotCopyCard) ||
            x.HasSpecialAbility(Mimicry.specialAbility) ||
            x.HasAnyOfAbilities(TheTrain.ability, TimeMachine.ability));
        }
    }
}
