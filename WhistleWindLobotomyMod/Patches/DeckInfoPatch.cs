using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(DeckInfo))]
    internal static class DeckInfoPatch
    {
        // Adds Nothing There to the deck when chosen in a card choice (Trader, Boss Box, etc.)
        [HarmonyPrefix, HarmonyPatch(nameof(DeckInfo.AddCard))]
        private static void AddNothingThereToPlayerDeck(ref CardInfo card)
        {
            if (card.Mods.Exists((x) => x.singletonId == "wstl_nothingThere"))
                card = CardLoader.GetCardByName("wstl_nothingThere");
        }

        // Act 1 starter decks
        [HarmonyPatch(nameof(DeckInfo.InitializeAsPlayerDeck))]
        [HarmonyPostfix]
        private static void VanillaDeckAddEvents(ref DeckInfo __instance)
        {
            if (LobotomyConfigManager.Instance.StartApocalypseBird)
                __instance.AddCard(CardLoader.GetCardByName("wstl_apocalypseBird"));

            if (LobotomyConfigManager.Instance.StartJesterOfNihil)
                __instance.AddCard(CardLoader.GetCardByName("wstl_jesterOfNihil"));

            if (LobotomyConfigManager.Instance.StartLyingAdult)
                __instance.AddCard(CardLoader.GetCardByName("wstl_lyingAdult"));
        }

        [HarmonyPatch(nameof(DeckInfo.InitializeAsPlayerDeck))]
        [HarmonyPrefix]
        private static bool Part1StarterDecks(ref DeckInfo __instance)
        {
            if (LobotomyConfigManager.Instance.StarterDeck <= 0 || LobotomyConfigManager.Instance.StarterDeck > 12)
                return true;

            int deckIdx = LobotomyConfigManager.Instance.StarterDeck;
            if (deckIdx == 1)
                deckIdx = UnityEngine.Random.Range(3, 13);

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
                    "wstl_todaysShyLook",
                    LobotomyConfigManager.Instance.NoRuina ? "wstl_mirrorOfAdjustment" : "wstl_pinocchio",
                    "wstl_behaviourAdjustment"
                },
                7 => new()
                {
                    "wstl_beautyAndBeast",
                    "wstl_voidDream",
                    "wstl_queenBee"
                },
                8 => new()
                {
                    "wstl_fragmentOfUniverse",
                    "wstl_skinProphecy",
                    LobotomyConfigManager.Instance.NoRuina ? "wstl_mhz176" : "wstl_priceOfSilence"
                },
                9 => new()
                {
                    "wstl_bloodBath",
                    "wstl_burrowingHeaven",
                    "wstl_snowQueen"
                },
                10 => new()
                {
                    LobotomyConfigManager.Instance.NoRuina ? "wstl_laetitia" : "wstl_theRoadHome",
                    "wstl_warmHeartedWoodsman",
                    "wstl_wisdomScarecrow",
                    LobotomyConfigManager.Instance.NoRuina ? "wstl_snowWhitesApple" : "wstl_ozma"
                },
                11 => new()
                {
                    "wstl_magicalGirlSpade",
                    "wstl_magicalGirlHeart",
                    "wstl_magicalGirlDiamond",
                    LobotomyConfigManager.Instance.NoRuina ? "wstl_voidDream" : "wstl_magicalGirlClover"
                },
                12 => new()
                {
                    "wstl_punishingBird",
                    "wstl_bigBird",
                    "wstl_judgementBird"
                },
                _ => null
            };

            // return vanilla deck
            if (cardsToAdd == null)
                return true;

            // if random mod cards is chosen, choose three random cards from this mod to act as a starter deck
            if (cardsToAdd.Count == 0)
            {
                List<CardInfo> validCards = new(ObtainableLobotomyCards);
                while (cardsToAdd.Count < 3 + LobotomyConfigManager.Instance.StarterDeckSize)
                {
                    CardInfo cardToAdd = ObtainableLobotomyCards[SeededRandom.Range(0, validCards.Count, SaveManager.SaveFile.GetCurrentRandomSeed())];

                    if (cardToAdd.onePerDeck)
                        validCards.Remove(cardToAdd);

                    cardsToAdd.Add(cardToAdd.name);
                }
            }

            foreach (string str in cardsToAdd)
                __instance.AddCard(CardLoader.GetCardByName(str));

            if (LobotomyConfigManager.Instance.StartApocalypseBird)
                __instance.AddCard(CardLoader.GetCardByName("wstl_apocalypseBird"));

            if (LobotomyConfigManager.Instance.StartJesterOfNihil)
                __instance.AddCard(CardLoader.GetCardByName("wstl_jesterOfNihil"));

            if (LobotomyConfigManager.Instance.StartLyingAdult)
                __instance.AddCard(CardLoader.GetCardByName("wstl_lyingAdult"));

            return false;
        }
    }
}
