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
    [HarmonyPatch(typeof(CardLoader))]
    public static class CardLoaderPatch
    {
        // Removes select cards following specific sequences
        [HarmonyPostfix, HarmonyPatch(nameof(CardLoader.GetUnlockedCards))]
        public static void RemoveUniqueCards(ref List<CardInfo> __result)
        {
            if (WstlSaveManager.HasUsedBackwardClock)
            {
                __result.RemoveAll((CardInfo x) => x.name == "wstl_backwardClock");
            }
            if (WstlSaveManager.HasApocalypse)
            {
                __result.RemoveAll((CardInfo x) => x.name == "wstl_punishingBird"
                || x.name == "wstl_bigBird" || x.name == "wstl_judgementBird");
            }
        }
    }
    [HarmonyPatch(typeof(DeckInfo))]
    public static class DeckInfoPatch
    {
        // Adds Nothing There to the deck when chosen in a card choice (Trader, Boss Box, etc.)
        [HarmonyPrefix, HarmonyPatch(nameof(DeckInfo.AddCard))]
        public static void AddNothing(ref CardInfo card)
        {
            if (card.Mods.Exists((CardModificationInfo x) => x.singletonId == "wstl_nothingThere"))
            {
                card = CardLoader.GetCardByName("wstl_nothingThere");
            }
        }
    }
}
