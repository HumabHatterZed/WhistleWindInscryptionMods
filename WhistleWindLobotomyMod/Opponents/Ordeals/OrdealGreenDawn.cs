using DiskCardGame;
using EasyFeedback.APIs;
using Infiniscryption.P03KayceeRun.Encounters;
using InscryptionAPI.Encounters;
using System;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    /// <summary>
    /// Appears in R0
    /// Difficulty range: (1 - 3) +[0,2] // 4 difficulty is for boss node w/o challlenges
    /// </summary>
    public class OrdealGreenDawn : OrdealBattleSequencer
    {
        public void ConstructGreenDawn(EncounterData encounterData, int minDifficulty)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1, turn2, turn3;
            if (encounterData.Difficulty <= minDifficulty)
            {
                turn1 = new() {
                    HelperMethods.NewDifficultyCard("wstl_doubtA", "wstl_doubtB", 1),
                    EncounterManager.NewCardBlueprint("wstl_doubtA")
                };
                turn2 = new() {
                    HelperMethods.NewDifficultyCard("wstl_doubtB", "wstl_doubtY", 1),
                    HelperMethods.NewDifficultyCard("wstl_doubtA", "wstl_doubtB", 1)
                };
                turn3 = new() {
                    HelperMethods.NewDifficultyCard("wstl_doubtY", "wstl_doubtO", 1),
                    EncounterManager.NewCardBlueprint("wstl_doubtB")
                };
            }
            else if (encounterData.Difficulty <= minDifficulty + 2)
            {
                turn1 = new() {
                    HelperMethods.NewDifficultyCard("wstl_doubtB", "wstl_doubtY", 3),
                    EncounterManager.NewCardBlueprint("wstl_doubtB")
                };
                turn2 = new() {
                    HelperMethods.NewDifficultyCard("wstl_doubtY", "wstl_doubtO", 3),
                    HelperMethods.NewDifficultyCard("wstl_doubtB", "wstl_doubtY", 3)
                };
                turn3 = new() {
                    EncounterManager.NewCardBlueprint("wstl_doubtO"),
                    EncounterManager.NewCardBlueprint("wstl_doubtY"),
                    HelperMethods.NewDifficultyCard("wstl_doubtA", "wstl_doubtB", 3)
                };
            }
            else
            {
                turn1 = new() {
                    EncounterManager.NewCardBlueprint("wstl_doubtY"),
                    EncounterManager.NewCardBlueprint("wstl_doubtB")
                };
                turn2 = new() {
                    EncounterManager.NewCardBlueprint("wstl_doubtO"),
                    EncounterManager.NewCardBlueprint("wstl_doubtY"),
                    EncounterManager.NewCardBlueprint("wstl_doubtB")
                };
                turn3 = new() {
                    EncounterManager.NewCardBlueprint("wstl_doubtO"),
                    EncounterManager.NewCardBlueprint("wstl_doubtY"),
                    EncounterManager.NewCardBlueprint("wstl_doubtY"),
                    EncounterManager.NewCardBlueprint("wstl_doubtB")
                };
            }

            encounterData.Blueprint.AddTurns(turn1, turn2, turn3);
        }

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            ConstructGreenDawn(encounterData, 2);
            return encounterData;
        }
    }
}