using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;

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
                card = CardLoader.GetCardByName("wstl_nothingThere");
        }
        [HarmonyPrefix, HarmonyPatch(nameof(DeckInfo.InitializeAsPlayerDeck))]
        private static bool Part1StarterDecks(ref DeckInfo __instance)
        {
            if (ConfigManager.Instance.StarterDeck <= 0 || ConfigManager.Instance.StarterDeck > 8)
                return true;

            int deckIdx = ConfigManager.Instance.StarterDeck;
            if (deckIdx == 1)
                deckIdx = UnityEngine.Random.Range(3, 9);

            List<string> cardsToAdd = deckIdx switch
            {
                2 => new(),
                3 => new()
                {
                    "wstl_oneSin",
                    "wstl_fairyFestival",
                    "wstl_oldLady"
                },
                4 => new()
                {
                    "wstl_scorchedGirl",
                    "wstl_laetitia",
                    "wstl_childOfTheGalaxy"
                },
                5 => new()
                {
                    "wstl_weCanChangeAnything",
                    "wstl_allAroundHelper",
                    "wstl_singingMachine"
                },
                6 => new()
                {
                    "WolfCub",
                    "wstl_warmHeartedWoodsman",
                    "wstl_wisdomScarecrow"
                },
                7 => new()
                {
                    "wstl_magicalGirlSpade",
                    "wstl_magicalGirlHeart",
                    "wstl_magicalGirlDiamond"
                },
                8 => new()
                {
                    "wstl_punishingBird",
                    "wstl_bigBird",
                    "wstl_judgementBird"
                },
                _ => null
            };

            // return vanilla deck
            if (cardsToAdd == null)
            {
                WstlPlugin.Log.LogError("Couldn't find a starter deck! Please inform the idiot who made this mod so they can fix it.");
                return true;
            }

            // if random mod cards is chosen, choose three random cards from this mod to act as a starter deck
            if (cardsToAdd.Count == 0)
            {
                while (cardsToAdd.Count < 3 + ConfigManager.Instance.StarterDeckSize)
                {
                    int randomIdx = UnityEngine.Random.Range(0, WstlPlugin.ObtainableLobotomyCards.Count);
                    cardsToAdd.Add(WstlPlugin.ObtainableLobotomyCards[randomIdx].name);
                }
            }

            foreach (string str in cardsToAdd)
                __instance.AddCard(CardLoader.GetCardByName(str));

            return false;
        }
    }
}
