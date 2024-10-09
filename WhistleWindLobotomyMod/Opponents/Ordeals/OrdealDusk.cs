using DiskCardGame;
using InscryptionAPI.Encounters;
using InscryptionAPI.Nodes;
using InscryptionAPI.TalkingCards;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod
{
    public class OrdealDusk : OrdealBattleSequencer
    {
        public static readonly string ID = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealDusk", typeof(OrdealDusk)).Id;

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            switch (ordealType)
            {
                case OrdealType.Green:
                    ConstructGreenDusk(encounterData);
                    break;
                case OrdealType.Crimson:
                    ConstructCrimsonDusk(encounterData);
                    break;
                case OrdealType.Amber:
                    ConstructAmberDusk(encounterData);
                    break;
                default:
                    ConstructWhiteDusk(encounterData);
                    break;
            }
            return encounterData;
        }
        private void ConstructGreenDusk(EncounterData encounterData)
        {

        }
        private void ConstructCrimsonDusk(EncounterData encounterData)
        {

        }
        private void ConstructAmberDusk(EncounterData encounterData)
        {

        }
        private void ConstructWhiteDusk(EncounterData encounterData)
        {
            List<string> possibleFixers = new()
            {
                "wstl_fixerRed", "wstl_fixerWhite", "wstl_fixerWhite", "wstl_fixerPale"
            };
            possibleFixers.Randomize();

            List<EncounterBlueprintData.CardBlueprint> turn1 = new()
            {
                EncounterManager.NewCardBlueprint(possibleFixers[0])
            };
            List<EncounterBlueprintData.CardBlueprint> turn2 = new()
            {
                EncounterManager.NewCardBlueprint(possibleFixers[1])
            };
            List<EncounterBlueprintData.CardBlueprint> turn3 = new()
            {
                EncounterManager.NewCardBlueprint(possibleFixers[2])
            };
            List<EncounterBlueprintData.CardBlueprint> turn4 = new()
            {
                EncounterManager.NewCardBlueprint(possibleFixers[3])
            };
            encounterData.Blueprint.AddTurns(turn1, turn2, turn3, turn4);
        }
    }
}