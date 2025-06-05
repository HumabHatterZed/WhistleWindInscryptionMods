using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal class PlayerDeckPatches {
        /// <summary>
        /// Modifies the starting deck. Use to add cheat cards or generate the Random deck.
        /// </summary>
        [HarmonyPrefix, HarmonyPatch(typeof(AscensionSaveData), nameof(AscensionSaveData.NewRun))]
        private static void AscensionModStarterDecks(ref List<CardInfo> starterDeck) {
            if (AscensionSaveData.Data.ChallengeIsActive(NoTime.Id)) {
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
            if (AscensionSaveData.Data.ChallengeIsActive(StartingApocalypse.Id) && !starterDeck.Exists(x => x.name == Cards.apocalypseBird))
                starterDeck.Add(CardLoader.GetCardByName(Cards.apocalypseBird));

            if (AscensionSaveData.Data.ChallengeIsActive(StartingJester.Id) && !starterDeck.Exists(x => x.name == Cards.jesterOfNihil))
                starterDeck.Add(CardLoader.GetCardByName(Cards.jesterOfNihil));

            if (AscensionSaveData.Data.ChallengeIsActive(StartingLiar.Id) && !starterDeck.Exists(x => x.name == Cards.lyingAdult))
                starterDeck.Add(CardLoader.GetCardByName(Cards.lyingAdult));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RunIntroSequencer), nameof(RunIntroSequencer.TryModifyStarterCards))]
        private static void ModifyStarterDeck() {
            if (!AscensionSaveData.Data.ChallengeIsActive(SoulboundCards.Id))
                return;

            foreach (CardInfo card in RunState.Run.playerDeck.Cards) {
                if (card.DefaultAbilities.Count < 5) {
                    RunState.Run.playerDeck.ModifyCard(card, new(DeathPenalty.ability));
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

                if (AscensionSaveData.Data.ChallengeIsActive(SoulboundCards.Id) && card.DefaultAbilities.Count < 4) {
                    card.Mods.Add(new(DeathPenalty.ability));
                }
            }

            /// <summary>
            /// Patch that lets people playing Part 1 use the starter decks via the config.
            /// </summary>
            /// <remarks>
            /// Doubt this is used by anyone but may as well keep it.
            /// </remarks>
            [HarmonyPrefix, HarmonyPatch(nameof(DeckInfo.InitializeAsPlayerDeck))]
            private static bool Part1StarterDecks(ref DeckInfo __instance) {
                if (LobotomyConfigManager.StarterDeck < 1 || LobotomyConfigManager.StarterDeck > 12)
                    return true;

                int deckIdx = LobotomyConfigManager.StarterDeck;
                if (deckIdx == 1)
                    deckIdx = UnityEngine.Random.Range(3, 1 + StarterDecks.NUM_DECKS);

                List<string> cardsToAdd = deckIdx switch {
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
                if (cardsToAdd.Count == 0) {
                    List<CardInfo> validCards = new(ObtainableLobotomyCards);
                    while (cardsToAdd.Count < 3 + LobotomyConfigManager.StarterDeckSize) {
                        CardInfo cardToAdd = ObtainableLobotomyCards[SeededRandom.Range(0, validCards.Count, SaveManager.SaveFile.GetCurrentRandomSeed())];

                        if (cardToAdd.onePerDeck)
                            validCards.Remove(cardToAdd);

                        cardsToAdd.Add(cardToAdd.name);
                    }
                }

                foreach (string str in cardsToAdd)
                    __instance.AddCard(CardLoader.GetCardByName(str));

                if (LobotomyConfigManager.ChallengeIsActive(StartingApocalypse.Id) && !__instance.Cards.Exists(x => x.name == Cards.apocalypseBird))
                    __instance.AddCard(CardLoader.GetCardByName(Cards.apocalypseBird));

                if (LobotomyConfigManager.ChallengeIsActive(StartingJester.Id) && !__instance.Cards.Exists(x => x.name == Cards.jesterOfNihil))
                    __instance.AddCard(CardLoader.GetCardByName(Cards.jesterOfNihil));

                if (LobotomyConfigManager.ChallengeIsActive(StartingLiar.Id) && !__instance.Cards.Exists(x => x.name == Cards.lyingAdult))
                    __instance.AddCard(CardLoader.GetCardByName(Cards.lyingAdult));

                return false;
            }
        }
    }
}
