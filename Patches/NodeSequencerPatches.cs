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
    [HarmonyPatch(typeof(CardMergeSequencer))]
    public static class CardMergePatches
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
            || x.Abilities.Contains(TimeMachine.ability)
            || x.name == "wstl_apocalypseBird");
        }
        /*
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.ModifyHostCard))]
        public static void Postfix(ref CardInfo hostCardInfo)
        {
            CardModificationInfo info = new(Ability.Sharp);
            info.singletonId = "wstl_test";
            RunState.Run.playerDeck.ModifyCard(hostCardInfo, info);
        }
        */
    }

    [HarmonyPatch(typeof(CardStatBoostSequencer))]
    public static class StatBoostPatch
    {
        // Removes cards from valid pool of hosts for stat boosts
        [HarmonyPostfix, HarmonyPatch(nameof(CardStatBoostSequencer.GetValidCards))]
        public static void RemoveFromValidCardsForStatBoost(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.SpecialAbilities.Contains(NothingThere.specialAbility)
            || x.Abilities.Contains(TheTrain.ability)
            || x.Abilities.Contains(TimeMachine.ability));
        }
    }
    /*
    [HarmonyPatch(typeof(DuplicateMergeSequencer))]
    public static class DuplicateMergePatch
    {
        [HarmonyPostfix, HarmonyPatch(nameof(DuplicateMergeSequencer.GetValidDuplicateCards))]
        public static void RemoveFromValidCardsForDuplication(ref List<CardInfo> __result)
        {
            // List of cards without the singleton (onePerDeck cards should already be removed, but you can add that check if desired)
            List<CardInfo> list = RunState.DeckList.FindAll((CardInfo x) => !x.Mods.Exists(x => x.singletonId == "wstl_test"));

            List<CardInfo> list2 = new();

            foreach (CardInfo card in list)
            {
                // if there is more than 1 card (can merge)
                if (list.Count((CardInfo x) => x.name == card.name) > 1)
                {
                    list2.Add(card);
                }
            }
            __result = list2;
        }
        
        [HarmonyPostfix, HarmonyPatch(nameof(DuplicateMergeSequencer.GetDuplicateCardChoices))]
        public static void Horse(ref List<CardChoice> __result)
        {
            // cards in deck
            List<CardInfo> list = new(RunState.DeckList);

            // cards without id or onePer
            List<CardInfo> list2 = list.FindAll((CardInfo x) => !x.onePerDeck && !x.Mods.Exists(x => x.singletonId == "wstl_test"));

            __result = new List<CardChoice>();

            int currentRandomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
            while (list2.Count > 0 && __result.Count < 3)
            {
                CardInfo cardInfo = list2[SeededRandom.Range(0, list.Count, currentRandomSeed++)];
                CardChoice cardChoice = new CardChoice();
                cardChoice.CardInfo = CardLoader.GetCardByName(cardInfo.name);
                __result.Add(cardChoice);
                list2.Remove(cardInfo);
            }
        }
    }
    */
    [HarmonyPatch(typeof(CopyCardSequencer))]
    public static class CopyCardPatch
    {
        // Removes cards from valid pool of hosts for stat boosts
        [HarmonyPostfix, HarmonyPatch(nameof(CopyCardSequencer.GetValidCards))]
        public static void RemoveFromValidCardsForCopyCard(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.Abilities.Contains(TheTrain.ability)
            || x.Abilities.Contains(TimeMachine.ability));
        }
    }
}
