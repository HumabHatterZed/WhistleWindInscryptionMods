using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(AscensionSaveData))]
    internal static class AscensionSaveDataPatch
    {
        [HarmonyPrefix, HarmonyPatch(nameof(AscensionSaveData.NewRun))]
        public static void ModStarterDecks(AscensionSaveData __instance, ref List<CardInfo> starterDeck)
        {
            // if all cards are disabled and this starter deck has mod cards in it, replace it with a deck of mod death cards
            if (LobotomyPlugin.AllCardsDisabled && starterDeck.Exists(x => LobotomyPlugin.LobotomyCards.Contains(x)))
            {
                int tickCount = Environment.TickCount;
                starterDeck = new()
                {
                    ModCardLoader.GetRandomModDeathCard(tickCount++),
                    ModCardLoader.GetRandomModDeathCard(tickCount++),
                    ModCardLoader.GetRandomModDeathCard(tickCount++)
                };
                return;
            }
            // if the starter deck has a placeholder card in it
            if (starterDeck.Exists(x => x.name == "wstl_RANDOM_PLACEHOLDER"))
            {
                starterDeck = new();
                bool allowRares = OnlyAlephCards();
                while (starterDeck.Count < 3)
                {
                    int randomIdx = UnityEngine.Random.Range(0, LobotomyPlugin.ObtainableLobotomyCards.Count);
                    CardInfo cardToAdd = LobotomyPlugin.ObtainableLobotomyCards[randomIdx];

                    // starting deck cannot have rare (if non-Aleph cards can be pulled) or sefirot cards
                    while ((!allowRares && cardToAdd.metaCategories.Contains(CardMetaCategory.Rare)) || cardToAdd.metaCategories.Contains(LobotomyCardHelper.SephirahCard))
                    {
                        randomIdx = UnityEngine.Random.Range(0, LobotomyPlugin.ObtainableLobotomyCards.Count);
                        cardToAdd = LobotomyPlugin.ObtainableLobotomyCards[randomIdx];
                    }
                    starterDeck.Add(cardToAdd);
                }
            }
        }
        private static bool OnlyAlephCards()
        {
            if (LobotomyPlugin.DisabledRiskLevels.HasFlag(LobotomyCardHelper.RiskLevel.Aleph))
                return false;

            if (LobotomyPlugin.DisabledRiskLevels.HasFlags(
                LobotomyCardHelper.RiskLevel.Zayin, LobotomyCardHelper.RiskLevel.Teth,
                LobotomyCardHelper.RiskLevel.He, LobotomyCardHelper.RiskLevel.Waw))
                return true;

            return false;
        }
    }
}
