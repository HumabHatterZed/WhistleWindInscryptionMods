using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents
{
    /// <summary>
    /// Appears in R0
    /// Difficulty range: (1 - 3) +[0,2] // 4 difficulty is for boss node w/o challlenges
    /// </summary>
    public class OrdealGreenDawn : OrdealBattleSequencer
    {
        public void ConstructGreenDawn(EncounterData encounterData, int minDifficulty)
        {
            LobotomyPlugin.Log.LogInfo("Construct Green Dawn");
            List<EncounterBlueprintData.CardBlueprint> turn1, turn2, turn3;
            if (encounterData.Difficulty <= minDifficulty)
            {
                turn1 = new() {
                    HelperMethods.NewDifficultyCard(Cards.doubtA, Cards.doubtB, 1),
                    EncounterManager.NewCardBlueprint(Cards.doubtA)
                };
                turn2 = new() {
                    HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, 1),
                    HelperMethods.NewDifficultyCard(Cards.doubtA, Cards.doubtB, 1)
                };
                turn3 = new() {
                    HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, 1),
                    EncounterManager.NewCardBlueprint(Cards.doubtB)
                };
            }
            else if (encounterData.Difficulty <= minDifficulty + 2)
            {
                turn1 = new() {
                    HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, 3),
                    EncounterManager.NewCardBlueprint(Cards.doubtB)
                };
                turn2 = new() {
                    HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, 3),
                    HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, 3)
                };
                turn3 = new() {
                    EncounterManager.NewCardBlueprint(Cards.doubtO),
                    EncounterManager.NewCardBlueprint(Cards.doubtY),
                    HelperMethods.NewDifficultyCard(Cards.doubtA, Cards.doubtB, 3)
                };
            }
            else
            {
                turn1 = new() {
                    EncounterManager.NewCardBlueprint(Cards.doubtY),
                    EncounterManager.NewCardBlueprint(Cards.doubtB)
                };
                turn2 = new() {
                    EncounterManager.NewCardBlueprint(Cards.doubtO),
                    EncounterManager.NewCardBlueprint(Cards.doubtY),
                    EncounterManager.NewCardBlueprint(Cards.doubtB)
                };
                turn3 = new() {
                    EncounterManager.NewCardBlueprint(Cards.doubtO),
                    EncounterManager.NewCardBlueprint(Cards.doubtY),
                    EncounterManager.NewCardBlueprint(Cards.doubtY),
                    EncounterManager.NewCardBlueprint(Cards.doubtB)
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