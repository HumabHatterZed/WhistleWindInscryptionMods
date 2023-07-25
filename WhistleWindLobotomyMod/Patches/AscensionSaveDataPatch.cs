using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Challenges;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(AscensionSaveData))]
    internal static class AscensionSaveDataPatch
    {
        [HarmonyPrefix, HarmonyPatch(nameof(AscensionSaveData.NewRun))]
        private static void AscensionModStarterDecks(ref List<CardInfo> starterDeck)
        {
            int randomSeed = SaveFile.IsAscension ? AscensionSaveData.Data.currentRunSeed : (SaveManager.SaveFile.pastRuns.Count * 1000);

            // if all cards are disabled and this starter deck has mod cards in it, replace them mod death cards
            if (LobotomyPlugin.AllCardsDisabled)
            {
                if (starterDeck.Exists(x => AllLobotomyCards.Contains(x)))
                {
                    for (int i = 0; i < starterDeck.Count; i++)
                    {
                        if (AllLobotomyCards.Contains(starterDeck[i]))
                            starterDeck[i] = LobotomyCardLoader.GetRandomModDeathCard(randomSeed++);
                    }
                }
            }
            else if (starterDeck.Exists(x => x.name == "wstl_RANDOM_PLACEHOLDER")) // if the starter deck has a placeholder card in it
            {
                List<CardInfo> newStarterDeck = new();
                bool addRare = SeededRandom.Value(randomSeed++) <= 0.05f;
                while (newStarterDeck.Count < starterDeck.Count)
                {
                    List<CardInfo> validCards = ObtainableLobotomyCards.FindAll(x => x.LacksCardMetaCategory(CardMetaCategory.Rare));

                    if (addRare) validCards = ObtainableLobotomyCards.FindAll(x => x.HasCardMetaCategory(CardMetaCategory.Rare));

                    validCards.RemoveAll(x => x.HasTrait(TraitSephirah));
                    validCards.RemoveAll(x => x.onePerDeck && newStarterDeck.Contains(x));

                    int randomIdx = SeededRandom.Range(0, validCards.Count, randomSeed++);
                    CardInfo cardToAdd = validCards[randomIdx];

                    // starting deck cannot have rare (if non-Aleph cards can be pulled) or sefirot cards
                    while (!addRare && cardToAdd.HasCardMetaCategory(CardMetaCategory.Rare))
                    {
                        randomIdx = SeededRandom.Range(0, ObtainableLobotomyCards.Count, randomSeed++);
                        cardToAdd = ObtainableLobotomyCards[randomIdx];
                    }

                    if (addRare) addRare = false;

                    newStarterDeck.Add(cardToAdd);
                }
                starterDeck = newStarterDeck;
            }

            if (AscensionSaveData.Data.ChallengeIsActive(ApocalypseBirdStart.Id))
                starterDeck.Add(CardLoader.GetCardByName("wstl_apocalypseBird"));

            if (AscensionSaveData.Data.ChallengeIsActive(JesterOfNihilStart.Id))
                starterDeck.Add(CardLoader.GetCardByName("wstl_jesterOfNihil"));

            if (AscensionSaveData.Data.ChallengeIsActive(LyingAdultStart.Id))
                starterDeck.Add(CardLoader.GetCardByName("wstl_lyingAdult"));
        }
    }
}
