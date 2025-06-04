using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(AscensionSaveData))]
    internal static class AscensionSaveDataPatch
    {
        [HarmonyPrefix, HarmonyPatch(nameof(AscensionSaveData.NewRun))]
        private static void AscensionModStarterDecks(ref List<CardInfo> starterDeck)
        {
            if (AscensionSaveData.Data.ChallengeIsActive(NoTime.Id)) {
                LobotomyPlugin.Log.LogInfo("Disable Backward Clock");
                LobotomySaveManager.UsedBackwardClock = true;
            }

            int randomSeed = SaveFile.IsAscension ? AscensionSaveData.Data.currentRunSeed : (SaveManager.SaveFile.pastRuns.Count * 1000);

            // if all cards are disabled and this starter deck has mod cards in it, replace them mod death cards
            if (LobotomyPlugin.AllCardsDisabled && starterDeck.Exists(AllLobotomyCards.Contains))
            {
                for (int i = 0; i < starterDeck.Count; i++)
                {
                    if (AllLobotomyCards.Contains(starterDeck[i]))
                        starterDeck[i] = LobotomyCardLoader.GetRandomModDeathCard(randomSeed++);
                }
            }
            else if (starterDeck.Exists(x => x.name == Cards.randomPlaceholder)) // if the starter deck has a placeholder card in it
            {
                List<CardInfo> newStarterDeck = new();
                bool addRare = SeededRandom.Value(randomSeed++) <= 0.05f;
                while (newStarterDeck.Count < starterDeck.Count)
                {
                    if (LobotomyPlugin.AllCardsDisabled) {
                        newStarterDeck.Add(ObtainableLobotomyCards[0]);
                        continue;
                    }

                    List<CardInfo> validCards;

                    if (addRare) {
                        addRare = false;
                        validCards = ObtainableLobotomyCards.FindAll(x => x.HasCardMetaCategory(CardMetaCategory.Rare));
                    }
                    else {
                        validCards = ObtainableLobotomyCards.FindAll(x => x.LacksCardMetaCategory(CardMetaCategory.Rare));
                    }

                    validCards.RemoveAll(x => x.HasTrait(Sephirah));
                    validCards.RemoveAll(x => x.onePerDeck && newStarterDeck.Contains(x));

                    int randomIdx = SeededRandom.Range(0, validCards.Count, randomSeed++);
                    CardInfo cardToAdd = validCards[randomIdx];

                    newStarterDeck.Add(cardToAdd);
                }
                starterDeck = newStarterDeck;
            }

            // only add 1 copy
            // this is to prevent duplication when restarting a run using the retry button
            if (AscensionSaveData.Data.ChallengeIsActive(StartingApocalypse.Id) && !starterDeck.Exists(x => x.name == Cards.apocalypseBird))
                starterDeck.Add(CardLoader.GetCardByName(Cards.apocalypseBird));

            if (AscensionSaveData.Data.ChallengeIsActive(StartingJester.Id) && !starterDeck.Exists(x => x.name == Cards.jesterOfNihil))
                starterDeck.Add(CardLoader.GetCardByName(Cards.jesterOfNihil));

            if (AscensionSaveData.Data.ChallengeIsActive(StartingLiar.Id) && !starterDeck.Exists(x => x.name == Cards.lyingAdult))
                starterDeck.Add(CardLoader.GetCardByName(Cards.lyingAdult));
        }
    }
}
