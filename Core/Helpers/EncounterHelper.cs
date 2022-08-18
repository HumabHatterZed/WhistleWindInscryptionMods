using InscryptionAPI;
using InscryptionAPI.Encounters;
using DiskCardGame;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace WhistleWindLobotomyMod
{
    public static class EncounterHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static EncounterBlueprintData.CardBlueprint CreateCardBlueprint(
            string cardName, int minDifficulty = 0, int maxDifficulty = 0, string replacement = "wstl_trainingDummy", int replacementChance = 0)
        {
            return new()
            {
                card = CardLoader.GetCardByName(cardName),
                minDifficulty = minDifficulty,
                maxDifficulty = maxDifficulty,
                replacement = CardLoader.GetCardByName(replacement),
                randomReplaceChance = replacementChance
            };
        }
    }
}
