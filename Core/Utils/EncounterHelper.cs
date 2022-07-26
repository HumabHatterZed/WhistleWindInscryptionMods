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
            string cardName, int minDifficulty, int maxDifficulty, string replacement = "wstl_trainingDummy", int replacementChance = 0)
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

        // Taken from GrimoraMod BlueprintUtils.BuildRandomBlueprint()
        public static EncounterBlueprintData CreateRandomBlueprint()
        {
            var blueprint = ScriptableObject.CreateInstance<EncounterBlueprintData>();
            blueprint.name = $"wstl_randomBlueprint_{UnityRandom.Range(1,99999)}";
            int numOfTurns = UnityRandom.Range(3, 15);
            blueprint.turns = new List<List<EncounterBlueprintData.CardBlueprint>>();
            for (int i = 0;i < numOfTurns; i++)
            {
                int numOfCards = UnityRandom.Range(0, 4);
                List<EncounterBlueprintData.CardBlueprint> cardBlueprints = new();
                for (int j = 0; j < numOfCards; j++)
                {
                    CardInfo randomCard = WstlPlugin.AllPlayableWstlModCards[UnityRandom.Range(0, WstlPlugin.AllPlayableWstlModCards.Count)];

                    cardBlueprints.Add(CreateCardBlueprint(randomCard.name, 0, randomCard.PowerLevel));
                }
            }
            return blueprint;
        }
    }
}
