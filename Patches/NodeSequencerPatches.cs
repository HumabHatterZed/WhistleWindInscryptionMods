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
    [HarmonyPatch(typeof(CardStatBoostSequencer))]
    public class StatBoostPatcher
    {
        // Removes cards from valid pool of hosts for stat boosts
        [HarmonyPostfix, HarmonyPatch(nameof(CardStatBoostSequencer.GetValidCards))]
        public static void RemoveFromValidCards(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.SpecialAbilities.Contains(NothingThere.specialAbility)
            || x.Abilities.Contains(TheTrain.ability)
            || x.Abilities.Contains(TimeMachine.ability));
        }
    }
    [HarmonyPatch(typeof(DuplicateMergeSequencer))]
    public class DuplicateMergePatch
    {
        // Removes cards from valid pool of duplicate choices
        [HarmonyPostfix, HarmonyPatch(nameof(DuplicateMergeSequencer.GetValidDuplicateCards))]
        public static void RemoveFromValidCardsForDuplication(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.SpecialAbilities.Contains(NothingThere.specialAbility)
            || x.Abilities.Contains(TheTrain.ability)
            || x.Abilities.Contains(TimeMachine.ability));
        }
    }
    [HarmonyPatch(typeof(CardMergeSequencer))]
    public class CardMergePatch
    {
        // Removes cards from valid pool of hosts for card merges
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForHost))]
        public static void RemoveFromValidCardsForHost(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.SpecialAbilities.Contains(NothingThere.specialAbility)
            || x.Abilities.Contains(TheTrain.ability)
            || x.Abilities.Contains(TimeMachine.ability));
        }
        // Removes cards from valid pool of sacrifices for card merges
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForSacrifice))]
        public static void RemoveFromValidCardsForSacrifice(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.SpecialAbilities.Contains(NothingThere.specialAbility)
            || x.Abilities.Contains(TheTrain.ability)
            || x.Abilities.Contains(TimeMachine.ability));
        }
    }
}
