using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.LobotomyMod.Core.Helpers;

namespace WhistleWind.LobotomyMod.Patches
{
    [HarmonyPatch(typeof(CardMergeSequencer))]
    internal class CardMergePatches
    {
        // Prevents cards from being sacrificed / transferring their sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForSacrifice))]
        private static void RemoveFromValidCardsForSacrifice(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.HasCardMetaCategory(LobotomyCardHelper.CannotGiveSigils)
            || x.HasSpecialAbility(Mimicry.specialAbility)
            || x.HasAnyOfAbilities(TheTrain.ability, TimeMachine.ability));
        }

        // Prevents card from being merged / gaining sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForHost))]
        private static void RemoveFromValidCardsForHost(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.HasCardMetaCategory(LobotomyCardHelper.CannotGainSigils) ||
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
            __result.RemoveAll(x => x.HasCardMetaCategory(LobotomyCardHelper.CannotBoostStats) ||
            x.HasSpecialAbility(Mimicry.specialAbility) ||
            x.HasAnyOfAbilities(TheTrain.ability, TimeMachine.ability));
        }
    }
    [HarmonyPatch(typeof(CopyCardSequencer))]
    internal class CopyCardPatch
    {
        // Prevents card from being copied by Goo (onePerDeck cards are removed automatically)
        [HarmonyPostfix, HarmonyPatch(nameof(CopyCardSequencer.GetValidCards))]
        private static void RemoveFromValidCardsForCopyCard(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.HasCardMetaCategory(LobotomyCardHelper.CannotCopyCard) ||
            x.HasSpecialAbility(Mimicry.specialAbility) ||
            x.HasAnyOfAbilities(TheTrain.ability, TimeMachine.ability));
        }
    }
}
