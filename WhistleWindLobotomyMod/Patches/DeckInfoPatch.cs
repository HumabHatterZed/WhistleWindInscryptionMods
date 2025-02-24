using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(DeckInfo))]
    internal class DeckInfoPatch
    {
        // Adds Nothing There to the deck when chosen in a card choice (Trader, Boss Box, etc.)
        [HarmonyPrefix, HarmonyPatch(nameof(DeckInfo.AddCard))]
        private static void AddNothingThereToPlayerDeck(ref CardInfo card)
        {
            CardModificationInfo mod = card.Mods.Find(x => HelperMethods.CompareSingleton(x.singletonId, "NothingThere"));
            if (mod != null)
            {
                string disguise = card.name;
                card = CardLoader.GetCardByName(Cards.nothingThere);
                card.Mods = new() { new() { singletonId = "NothingThere:" + disguise } };
            }
        }

        // Act 1 starter decks
        [HarmonyPatch(nameof(DeckInfo.InitializeAsPlayerDeck))]
        [HarmonyPostfix]
        private static void VanillaDeckAddEvents(ref DeckInfo __instance)
        {
            if (LobotomyConfigManager.StartApocalypseBird && !__instance.Cards.Exists(x => x.name == Cards.apocalypseBird))
                __instance.AddCard(CardLoader.GetCardByName(Cards.apocalypseBird));

            if (LobotomyConfigManager.StartJesterOfNihil && !__instance.Cards.Exists(x => x.name == Cards.jesterOfNihil))
                __instance.AddCard(CardLoader.GetCardByName(Cards.jesterOfNihil));

            if (LobotomyConfigManager.StartLyingAdult && !__instance.Cards.Exists(x => x.name == Cards.lyingAdult))
                __instance.AddCard(CardLoader.GetCardByName(Cards.lyingAdult));
        }

        [HarmonyPatch(nameof(DeckInfo.InitializeAsPlayerDeck))]
        [HarmonyPrefix]
        private static bool Part1StarterDecks(ref DeckInfo __instance)
        {
            if (LobotomyConfigManager.StarterDeck <= 0 || LobotomyConfigManager.StarterDeck > 12)
                return true;

            int deckIdx = LobotomyConfigManager.StarterDeck;
            if (deckIdx == 1)
                deckIdx = UnityEngine.Random.Range(3, 1 + StarterDecks.NUM_DECKS);

            List<string> cardsToAdd = deckIdx switch
            {
                3 => StarterDecks.firstDay,
                4 => StarterDecks.lonelyFriends,
                5 => StarterDecks.bloodMechs,
                6 => StarterDecks.peoplePleasers,
                7 => StarterDecks.freakShow,
                8 => StarterDecks.apocrypha,
                9 => StarterDecks.keter,
                10 => StarterDecks.deathLovers,
                11 => StarterDecks.roadToOz,
                12 => StarterDecks.magicGirls,
                13 => StarterDecks.twilight,
                _ => null
            };

            // return vanilla deck
            if (cardsToAdd == null)
                return true;

            // if random mod cards is chosen, choose three random cards from this mod to act as a starter deck
            if (cardsToAdd.Count == 0)
            {
                List<CardInfo> validCards = new(ObtainableLobotomyCards);
                while (cardsToAdd.Count < 3 + LobotomyConfigManager.StarterDeckSize)
                {
                    CardInfo cardToAdd = ObtainableLobotomyCards[SeededRandom.Range(0, validCards.Count, SaveManager.SaveFile.GetCurrentRandomSeed())];

                    if (cardToAdd.onePerDeck)
                        validCards.Remove(cardToAdd);

                    cardsToAdd.Add(cardToAdd.name);
                }
            }

            foreach (string str in cardsToAdd)
                __instance.AddCard(CardLoader.GetCardByName(str));

            if (LobotomyConfigManager.StartApocalypseBird)
                __instance.AddCard(CardLoader.GetCardByName(Cards.apocalypseBird));

            if (LobotomyConfigManager.StartJesterOfNihil)
                __instance.AddCard(CardLoader.GetCardByName(Cards.jesterOfNihil));

            if (LobotomyConfigManager.StartLyingAdult)
                __instance.AddCard(CardLoader.GetCardByName(Cards.lyingAdult));

            return false;
        }
    }
}
