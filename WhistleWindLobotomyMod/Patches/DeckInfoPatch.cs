using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.Core.Helpers.LobotomyCardManager;

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

        [HarmonyPrefix, HarmonyPatch(nameof(DeckInfo.InitializeAsPlayerDeck))]
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
                    LobotomyPlugin.RuinaCardsDisabled ? "wstl_mirrorOfAdjustment" : "wstl_pinocchio",
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
                    "wstl_plagueDoctor"
                },
                9 => new()
                {
                    "wstl_bloodBath",
                    "wstl_burrowingHeaven",
                    "wstl_snowQueen"
                },
                10 => new()
                {
                    LobotomyPlugin.RuinaCardsDisabled ? "wstl_laetitia" : "wstl_theRoadHome",
                    "wstl_warmHeartedWoodsman",
                    "wstl_wisdomScarecrow",
                    LobotomyPlugin.RuinaCardsDisabled ? "wstl_snowWhitesApple" : "wstl_ozma"
                },
                11 => new()
                {
                    "wstl_magicalGirlSpade",
                    "wstl_magicalGirlHeart",
                    "wstl_magicalGirlDiamond",
                    LobotomyPlugin.RuinaCardsDisabled ? "wstl_voidDream" : "wstl_magicalGirlClover"
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
                while (cardsToAdd.Count < 3 + LobotomyConfigManager.Instance.StarterDeckSize)
                {
                    int randomIdx = UnityEngine.Random.Range(0, ObtainableLobotomyCards.Count);
                    cardsToAdd.Add(ObtainableLobotomyCards[randomIdx].name);
                }
            }

            foreach (string str in cardsToAdd)
                __instance.AddCard(CardLoader.GetCardByName(str));

            return false;
        }
    }
}
