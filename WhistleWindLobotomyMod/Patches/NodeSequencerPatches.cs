using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(CardMergeSequencer))]
    public static class CardMergePatches
    {
        // Prevents cards from being sacrificed / transferring their sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForSacrifice))]
        public static void RemoveFromValidCardsForSacrifice(ref List<CardInfo> __result)
        {
            __result.RemoveAll((x) => x.metaCategories.Any((mc) => mc == CardHelper.CANNOT_GIVE_SIGILS)
            || x.SpecialAbilities.Any((sa) => sa == Mimicry.specialAbility)
            || x.Abilities.Any((ab) => ab == TheTrain.ability || ab == TimeMachine.ability || ab == Scrambler.ability || ab == TargetGainStats.ability || ab == TargetGainSigils.ability || ab == TargetGainStatsSigils.ability));
        }

        // Prevents card from being merged / gaining sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForHost))]
        public static void RemoveFromValidCardsForHost(ref List<CardInfo> __result)
        {
            __result.RemoveAll((x) => x.metaCategories.Any((mc) => mc == CardHelper.CANNOT_GAIN_SIGILS)
            || x.SpecialAbilities.Any((sa) => sa == Mimicry.specialAbility)
            || x.Abilities.Any((ab) => ab == TheTrain.ability || ab == TimeMachine.ability || ab == Scrambler.ability || ab == TargetGainStats.ability));
        }
    }

    [HarmonyPatch(typeof(CardStatBoostSequencer))]
    public static class StatBoostPatch
    {
        // Prevents cards from having their stats boostable
        [HarmonyPostfix, HarmonyPatch(nameof(CardStatBoostSequencer.GetValidCards))]
        public static void RemoveFromValidCardsForStatBoost(ref List<CardInfo> __result)
        {
            __result.RemoveAll((x) => x.metaCategories.Any((mc) => mc == CardHelper.CANNOT_BUFF_STATS)
            || x.SpecialAbilities.Any((sa) => sa == Mimicry.specialAbility)
            || x.Abilities.Any((ab) => ab == TheTrain.ability || ab == TimeMachine.ability || ab == TargetGainSigils.ability));
        }
    }
    [HarmonyPatch(typeof(CopyCardSequencer))]
    public static class CopyCardPatch
    {
        // Prevents card from being copied by Goo (onePerDeck cards are removed automatically)
        [HarmonyPostfix, HarmonyPatch(nameof(CopyCardSequencer.GetValidCards))]
        public static void RemoveFromValidCardsForCopyCard(ref List<CardInfo> __result)
        {
            __result.RemoveAll((x) => x.metaCategories.Any((mc) => mc == CardHelper.CANNOT_COPY_CARD)
           || x.SpecialAbilities.Any((sa) => sa == Mimicry.specialAbility)
           || x.Abilities.Any((ab) => ab == TheTrain.ability || ab == TimeMachine.ability || ab == Scrambler.ability || ab == TargetGainStats.ability || ab == TargetGainSigils.ability || ab == TargetGainStatsSigils.ability));
        }
    }
}
