using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod.Patches {
    [HarmonyPatch]
    internal class PlayerDeckPatches {
        /// <summary>
        /// Modifies the starting deck. Use to add cheat cards or generate the Random deck.
        /// </summary>
        [HarmonyPrefix, HarmonyPatch(typeof(AscensionSaveData), nameof(AscensionSaveData.NewRun))]
        private static void AscensionModStarterDecks(ref List<CardInfo> starterDeck) {
            if (LobotomyConfigManager.ChallengeIsActive(NoTime.ID)) {
                LobotomySaveManager.UsedBackwardClock = true;
            }

            int randomSeed = SaveFile.IsAscension ? AscensionSaveData.Data.currentRunSeed : (SaveManager.SaveFile.pastRuns.Count * 1000);

            // if all cards are disabled and this starter deck has mod cards in it, replace them mod death cards
            if (LobotomyPlugin.AllCardsDisabled && starterDeck.Exists(AllLobotomyCards.Contains)) {
                for (int i = 0; i < starterDeck.Count; i++) {
                    if (AllLobotomyCards.Contains(starterDeck[i]))
                        starterDeck[i] = LobotomyCardLoader.GetRandomModDeathCard(randomSeed++);
                }
            }
            else if (starterDeck.Exists(x => x.name == Cards.randomPlaceholder)) // if the starter deck has a placeholder card in it
            {
                List<CardInfo> newStarterDeck = new();
                bool addRare = SeededRandom.Value(randomSeed++) <= 0.05f;
                while (newStarterDeck.Count < starterDeck.Count) {
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

            // check the card doesn't exist in the deck (possible when retrying a run)
            if (LobotomyConfigManager.ChallengeIsActive(StartingApocalypse.ID) && !starterDeck.Exists(x => x.name == Cards.apocalypseBird))
                starterDeck.Add(CardLoader.GetCardByName(Cards.apocalypseBird));

            if (LobotomyConfigManager.ChallengeIsActive(StartingJester.ID) && !starterDeck.Exists(x => x.name == Cards.jesterOfNihil))
                starterDeck.Add(CardLoader.GetCardByName(Cards.jesterOfNihil));

            if (LobotomyConfigManager.ChallengeIsActive(StartingLiar.ID) && !starterDeck.Exists(x => x.name == Cards.lyingAdult))
                starterDeck.Add(CardLoader.GetCardByName(Cards.lyingAdult));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RunIntroSequencer), nameof(RunIntroSequencer.TryModifyStarterCards))]
        private static void ModifyStarterDeck() {
            if (LobotomyConfigManager.ChallengeIsActive(SoulboundCards.ID)) {
                foreach (CardInfo card in SaveManager.SaveFile.CurrentDeck.Cards) {
                    if (card.LacksTrait(Trait.Pelt)) {
                        SaveManager.SaveFile.CurrentDeck.ModifyCard(card, new(DeathPenalty.ID) { fromCardMerge = card.DefaultAbilities.Count > 4 });
                    }
                }
            }
        }

        /// <summary>
        /// Patches related to the DeckInfo class.
        /// </summary>
        [HarmonyPatch(typeof(DeckInfo))]
        private class DeckInfoPatch {

            [HarmonyPrefix, HarmonyPatch(nameof(DeckInfo.AddCard))]
            private static void AddNothingThereToPlayerDeck(ref CardInfo card) {
                CardModificationInfo mod = card.Mods.Find(x => HelperMethods.CompareSingleton(x.singletonId, "NothingThere"));
                if (mod != null) {
                    string disguise = card.name;
                    card = CardLoader.GetCardByName(Cards.nothingThere);
                    card.Mods = new() { new() { singletonId = "NothingThere:" + disguise } };
                }

                if (SaveFile.IsAscension && LobotomyConfigManager.ChallengeIsActive(SoulboundCards.ID) && card.DefaultAbilities.Count < 4) {
                    card.Mods.Add(new(DeathPenalty.ID));
                }
            }
        }
    }
}
