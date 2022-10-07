using InscryptionAPI;
using InscryptionAPI.Encounters;
using DiskCardGame;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace WhistleWindLobotomyMod.Core.Helpers
{
    public static class EncounterHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static EncounterBlueprintData.CardBlueprint CreateCardBlueprint(
            string cardName, int replacementChance = 25)
        {
            return new()
            {
                card = CardLoader.GetCardByName(cardName),
                randomReplaceChance = replacementChance
            };
        }
        
        public static EncounterBlueprintData BuildBlueprint(
            string name, List<Tribe> tribes, int min, int max,
            List<CardInfo> randomCards, List<Ability> redundantAbilities,
            List<List<EncounterBlueprintData.CardBlueprint>> turns)
        {
            EncounterBlueprintData encounterData = ScriptableObject.CreateInstance<EncounterBlueprintData>();
            encounterData.name = name;
            encounterData.dominantTribes = tribes;
            encounterData.SetDifficulty(min, max);
            encounterData.randomReplacementCards = randomCards;
            encounterData.redundantAbilities = redundantAbilities;
            encounterData.regionSpecific = true;
            encounterData.turns = turns;
            return encounterData;
        }
    }
}
