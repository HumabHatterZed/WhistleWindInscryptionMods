using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod
{
    [HarmonyPatch(typeof(AscensionSaveData))]
    internal static class AscensionSaveDataPatch
    {
        [HarmonyPrefix, HarmonyPatch(nameof(AscensionSaveData.NewRun))]
        public static void AscensionModStarterDecks(ref List<CardInfo> starterDeck)
        {
            int tickCount = Environment.TickCount;

            // if the starter deck has a placeholder card in it
            if (starterDeck.Exists(x => x.name == "wstl_RANDOM_PLACEHOLDER"))
            {
                List<CardInfo> starterDeck2 = new();
                bool isRare;
                while (starterDeck2.Count < starterDeck.Count)
                {
                    isRare = SeededRandom.Bool(tickCount++);
                    int randomIdx = UnityEngine.Random.Range(0, WstlPlugin.ObtainableLobotomyCards.Count);
                    CardInfo cardToAdd = isRare ? ModCardLoader.GetRandomRareModCard(tickCount++) : WstlPlugin.ObtainableLobotomyCards[randomIdx];

                    // starting deck cannot have rare (if non-Aleph cards can be pulled) or sefirot cards
                    while (!isRare && cardToAdd.metaCategories.Contains(CardMetaCategory.Rare))
                    {
                        randomIdx = UnityEngine.Random.Range(0, WstlPlugin.ObtainableLobotomyCards.Count);
                        cardToAdd = WstlPlugin.ObtainableLobotomyCards[randomIdx];
                    }
                    starterDeck2.Add(cardToAdd);
                }
            }
        }
    }
}
