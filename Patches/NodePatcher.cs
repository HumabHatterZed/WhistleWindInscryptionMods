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
    public static class NodePatcher
    {
        // Removes cards from valid pool of hosts for stat boosts
        [HarmonyPatch(typeof(CardStatBoostSequencer), nameof(CardStatBoostSequencer.GetValidCards))]
        [HarmonyPostfix]
        public static void RemoveFromValidCards(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.SpecialAbilities.Contains(NothingThere.specialAbility) || x.Abilities.Contains(TheTrain.ability));
            //__result.RemoveAll((CardInfo x) => x.Abilities.Contains(TimeMachine.ability));
        }
        // Removes cards from valid pool of duplicate choices
        // Removes cards that are/will be part of events that predicate on there only ever being one copy
        [HarmonyPatch(typeof(DuplicateMergeSequencer), nameof(DuplicateMergeSequencer.GetValidDuplicateCards))]
        [HarmonyPostfix]
        public static void RemoveFromValidCardsForDuplication(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.name.Equals("wstl_plagueDoctor") || x.name.Equals("wstl_punishingBird") || x.name.Equals("wstl_bigBird") || x.name.Equals("wstl_judgementBird") ||
            x.name.Equals("wstl_magicalGirlHeart") || x.name.Equals("magicalGirlDiamond") || x.name.Equals("wstl_magicalGirlSpade"));// || x.name.Equals("wstl_magicalGirlClub"));
            __result.RemoveAll((CardInfo x) => x.SpecialAbilities.Contains(NothingThere.specialAbility) || x.Abilities.Contains(TheTrain.ability));
            //__result.RemoveAll((CardInfo x) => x.Abilities.Contains(TimeMachine.ability));
        }
        // Removes cards from valid pool of hosts for card merges
        [HarmonyPatch(typeof(CardMergeSequencer), nameof(CardMergeSequencer.GetValidCardsForHost))]
        [HarmonyPostfix]
        public static void RemoveFromValidCardsForHost(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.SpecialAbilities.Contains(NothingThere.specialAbility) || x.Abilities.Contains(TheTrain.ability));
            //__result.RemoveAll((CardInfo x) => x.Abilities.Contains(TimeMachine.ability));
        }
        // Removes cards from valid pool of sacrifices for card merges
        [HarmonyPatch(typeof(CardMergeSequencer), nameof(CardMergeSequencer.GetValidCardsForSacrifice))]
        [HarmonyPostfix]
        public static void RemoveFromValidCardsForSacrifice(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.SpecialAbilities.Contains(NothingThere.specialAbility) || x.Abilities.Contains(TheTrain.ability));
            //__result.RemoveAll((CardInfo x) => x.Abilities.Contains(TimeMachine.ability));
        }
    }
}
