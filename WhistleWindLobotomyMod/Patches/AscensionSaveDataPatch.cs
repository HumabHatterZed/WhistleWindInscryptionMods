using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(AscensionSaveData))]
    internal static class AscensionSaveDataPatch
    {
        [HarmonyPrefix, HarmonyPatch(nameof(AscensionSaveData.NewRun))]
        public static void AscensionModStarterDecks(ref List<CardInfo> starterDeck)
        {
            int tickCount = Environment.TickCount;

            // if all cards are disabled and this starter deck has mod cards in it, replace them mod death cards
            if (LobotomyPlugin.AllCardsDisabled && starterDeck.Exists(x => LobotomyPlugin.AllLobotomyCards.Contains(x)))
            {
                for (int i = 0; i < starterDeck.Count; i++)
                {
                    if (LobotomyPlugin.AllLobotomyCards.Contains(starterDeck[i]))
                        starterDeck[i] = LobotomyCardLoader.GetRandomModDeathCard(tickCount++);
                }

                return;
            }
            // if the starter deck has a placeholder card in it
            if (starterDeck.Exists(x => x.name == "wstl_RANDOM_PLACEHOLDER"))
            {
                List<CardInfo> starterDeck2 = new();
                bool isRare;
                while (starterDeck2.Count < starterDeck.Count)
                {
                    isRare = SeededRandom.Bool(tickCount++);
                    int randomIdx = UnityEngine.Random.Range(0, LobotomyPlugin.ObtainableLobotomyCards.Count);
                    CardInfo cardToAdd = isRare ? LobotomyCardLoader.GetRandomRareModCard(tickCount++) : LobotomyPlugin.ObtainableLobotomyCards[randomIdx];

                    // starting deck cannot have rare (if non-Aleph cards can be pulled) or sefirot cards
                    while (!isRare && cardToAdd.metaCategories.Contains(CardMetaCategory.Rare))
                    {
                        randomIdx = UnityEngine.Random.Range(0, LobotomyPlugin.ObtainableLobotomyCards.Count);
                        cardToAdd = LobotomyPlugin.ObtainableLobotomyCards[randomIdx];
                    }
                    starterDeck2.Add(cardToAdd);
                }
            }
        }
    }
}
