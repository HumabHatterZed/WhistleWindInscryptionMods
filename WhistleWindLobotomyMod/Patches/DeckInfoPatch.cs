using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(DeckInfo))]
    internal static class DeckInfoPatch
    {
        // Adds Nothing There to the deck when chosen in a card choice (Trader, Boss Box, etc.)
        [HarmonyPrefix, HarmonyPatch(nameof(DeckInfo.AddCard))]
        private static void AddNothingThere(ref CardInfo card)
        {
            if (card.Mods.Exists((x) => x.singletonId == "wstl_nothingThere"))
                card = CardLoader.GetCardByName("wstl_nothingThere");
        }

        [HarmonyPrefix, HarmonyPatch(nameof(DeckInfo.InitializeAsPlayerDeck))]
        private static bool ModStarterDecks(ref DeckInfo __instance)
        {
            if (ConfigManager.Instance.StarterDeck <= 0 || ConfigManager.Instance.StarterDeck > 9)
                return true;

            int deckIdx = ConfigManager.Instance.StarterDeck;
            if (deckIdx == 9)
                deckIdx = UnityEngine.Random.Range(1, 8);

            List<string> cardsToAdd = deckIdx switch
            {
                1 => new() { "wstl_oneSin", "wstl_fairyFestival", "wstl_oldLady" },
                2 => new() { "wstl_scorchedGirl", "wstl_laetitia", "wstl_childOfTheGalaxy" },
                3 => new() { "wstl_weCanChangeAnything", "wstl_allAroundHelper", "wstl_singingMachine" },
                4 => new() { "Squirrel" },
                5 => new() { LobotomyPlugin.RuinaCardsDisabled ? "wstl_laetitia" : "wstl_theRoadHome", "wstl_warmHeartedWoodsman", "wstl_wisdomScarecrow" },
                6 => new() { "wstl_magicalGirlHeart", "wstl_magicalGirlDiamond", LobotomyPlugin.RuinaCardsDisabled ? "wstl_magicalGirlSpade" : "wstl_magicalGirlClover" },
                7 => new() { "wstl_punishingBird", "wstl_bigBird", "wstl_judgementBird" },
                _ => new()
            };

            // if random mod cards is chosen, choose three random cards from this mod to act as a starter deck
            if (cardsToAdd.Count == 0)
            {
                while (cardsToAdd.Count < 3)
                {
                    int randomIdx = UnityEngine.Random.Range(0, LobotomyPlugin.ObtainableLobotomyCards.Count);
                    cardsToAdd.Add(LobotomyPlugin.ObtainableLobotomyCards[randomIdx].name);
                }
            }

            foreach (string str in cardsToAdd)
                __instance.AddCard(CardLoader.GetCardByName(str));

            return false;
        }
    }
}
