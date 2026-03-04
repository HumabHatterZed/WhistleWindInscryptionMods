using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;

namespace WhistleWind.AbnormalSigils {
    [HarmonyPatch(typeof(CardMergeSequencer))]
    internal class CardMergePatches {
        // Prevents cards from being sacrificed / transferring their sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForSacrifice))]
        private static void RemoveFromValidCardsForSacrifice(CardInfo host, ref List<CardInfo> __result) {
            __result.RemoveAll(x => x.HasCardMetaCategory(AbnormalPlugin.CannotGiveSigils));
            if (host != null) {
                RemoveIncompatibleMergeCandidates(host, __result);
            }
        }

        // Prevents card from being merged / gaining sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForHost))]
        private static void RemoveFromValidCardsForHost(CardInfo sacrifice, ref List<CardInfo> __result) {
            __result.RemoveAll(x => x.HasCardMetaCategory(AbnormalPlugin.CannotGainSigils));
            if (sacrifice != null) {
                RemoveIncompatibleMergeCandidates(sacrifice, __result);
            }
        }

        private static void RemoveIncompatibleMergeCandidates(CardInfo candidate, List<CardInfo> results) {
            if (candidate.HasAbility(Ability.Sniper)) {
                results.RemoveAll(x => x.HasAbility(ActivatedSniper.ID));
            }
            if (candidate.HasAbility(ActivatedSniper.ID)) {
                results.RemoveAll(x => x.HasAbility(Ability.Sniper));
            }
            if (candidate.HasAbility(Lonely.ID)) {
                results.RemoveAll(x => x.HasAbility(Ability.DrawCopyOnDeath));
            }
            if (candidate.HasAbility(Ability.DrawCopyOnDeath)) {
                results.RemoveAll(x => x.HasAbility(UnkillableWeak.ID));
            }
            if (candidate.HasAbility(UnkillableWeak.ID)) {
                results.RemoveAll(x => x.HasAbility(Ability.DrawCopyOnDeath));
            }
        }
    }

    [HarmonyPatch(typeof(CardStatBoostSequencer))]
    internal class StatBoostPatch {
        // Prevents cards from having their stats boostable
        [HarmonyPostfix, HarmonyPatch(nameof(CardStatBoostSequencer.GetValidCards))]
        private static void RemoveFromValidCardsForStatBoost(ref List<CardInfo> __result) {
            __result.RemoveAll(x => x.HasCardMetaCategory(AbnormalPlugin.CannotBoostStats));
        }
    }
    [HarmonyPatch(typeof(CopyCardSequencer))]
    internal class CopyCardPatch {
        [HarmonyPostfix, HarmonyPatch(nameof(CopyCardSequencer.GetValidCards))]
        private static void RemoveFromValidCardsForCopyCard(ref List<CardInfo> __result) {
            __result.RemoveAll(x => x.HasCardMetaCategory(AbnormalPlugin.CannotCopyCard));
        }
    }
}
