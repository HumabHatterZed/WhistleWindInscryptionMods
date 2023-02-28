using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

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
            if (LobotomyPlugin.AllCardsDisabled && starterDeck.Exists(x => AllLobotomyCards.Contains(x)))
            {
                for (int i = 0; i < starterDeck.Count; i++)
                {
                    if (AllLobotomyCards.Contains(starterDeck[i]))
                        starterDeck[i] = LobotomyCardLoader.GetRandomModDeathCard(tickCount++);
                }

                return;
            }
            // if the starter deck has a placeholder card in it
            if (starterDeck.Exists(x => x.name == "wstl_RANDOM_PLACEHOLDER"))
            {
                List<CardInfo> starterDeck2 = new();
                bool addRare = SeededRandom.Value(tickCount++) <= 0.05f;
                while (starterDeck2.Count < starterDeck.Count)
                {
                    List<CardInfo> validCards = ObtainableLobotomyCards.FindAll(x => x.LacksCardMetaCategory(CardMetaCategory.Rare));

                    if (addRare) validCards = ObtainableLobotomyCards.FindAll(x => x.HasCardMetaCategory(CardMetaCategory.Rare));

                    validCards.RemoveAll(x => x.HasTrait(TraitSephirah));

                    int randomIdx = UnityEngine.Random.Range(0, validCards.Count);
                    CardInfo cardToAdd = validCards[randomIdx];

                    // starting deck cannot have rare (if non-Aleph cards can be pulled) or sefirot cards
                    while (!addRare && cardToAdd.metaCategories.Contains(CardMetaCategory.Rare))
                    {
                        randomIdx = UnityEngine.Random.Range(0, ObtainableLobotomyCards.Count);
                        cardToAdd = ObtainableLobotomyCards[randomIdx];
                    }

                    if (addRare) addRare = false;

                    starterDeck2.Add(cardToAdd);
                }
                starterDeck = starterDeck2;
            }
        }
    }
}
