using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Infiniscryption.Spells.Patchers;
namespace WhistleWindLobotomyMod
{
    [HarmonyPatch(typeof(CardMergeSequencer))]
    public static class CardMergePatches
    {
        // Prevents cards from being sacrificed / transferring their sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForSacrifice))]
        public static void RemoveFromValidCardsForSacrifice(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.metaCategories.Any((CardMetaCategory mc) => mc == CardHelper.CANNOT_GIVE_SIGILS)
            || x.SpecialAbilities.Any((SpecialTriggeredAbility sa) => sa == Mimicry.specialAbility)
            || x.Abilities.Any((Ability ab) => ab == TheTrain.ability || ab == TimeMachine.ability || ab == Scrambler.ability || ab == TargetGainStats.ability || ab == TargetGainSigils.ability || ab == TargetGainStatsSigils.ability));
        }

        // Prevents card from being merged / gaining sigils
        [HarmonyPostfix, HarmonyPatch(nameof(CardMergeSequencer.GetValidCardsForHost))]
        public static void RemoveFromValidCardsForHost(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.metaCategories.Any((CardMetaCategory mc) => mc == CardHelper.CANNOT_GAIN_SIGILS)
            || x.SpecialAbilities.Any((SpecialTriggeredAbility sa) => sa == Mimicry.specialAbility)
            || x.Abilities.Any((Ability ab) => ab == TheTrain.ability || ab == TimeMachine.ability || ab == Scrambler.ability || ab == TargetGainStats.ability));
        }
    }

    [HarmonyPatch(typeof(CardStatBoostSequencer))]
    public static class StatBoostPatch
    {
        // Prevents cards from having their stats boostable
        [HarmonyPostfix, HarmonyPatch(nameof(CardStatBoostSequencer.GetValidCards))]
        public static void RemoveFromValidCardsForStatBoost(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.metaCategories.Any((CardMetaCategory mc) => mc == CardHelper.CANNOT_BUFF_STATS)
            || x.SpecialAbilities.Any((SpecialTriggeredAbility sa) => sa == Mimicry.specialAbility)
            || x.Abilities.Any((Ability ab) => ab == TheTrain.ability || ab == TimeMachine.ability || ab == TargetGainSigils.ability));
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
        // Prevents card from being copied by Goo (onePerDeck cards are removed automatically)
        [HarmonyPostfix, HarmonyPatch(nameof(CopyCardSequencer.GetValidCards))]
        public static void RemoveFromValidCardsForCopyCard(ref List<CardInfo> __result)
        {
            __result.RemoveAll((CardInfo x) => x.metaCategories.Any((CardMetaCategory mc) => mc == CardHelper.CANNOT_COPY_CARD)
           || x.SpecialAbilities.Any((SpecialTriggeredAbility sa) => sa == Mimicry.specialAbility)
           || x.Abilities.Any((Ability ab) => ab == TheTrain.ability || ab == TimeMachine.ability || ab == Scrambler.ability || ab == TargetGainStats.ability || ab == TargetGainSigils.ability || ab == TargetGainStatsSigils.ability));
        }
    }
}
