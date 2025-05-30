using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents
{
    /// <summary>
    /// The weakest Green Ordeal.
    /// As the 'standard' Ordeal type, Green Ordeals should have the simplest blueprints.
    /// Difficulty range: [0,2] +[0,2]
    /// Cards required: 3, 3, 4
    /// Valid regions: 0
    /// </summary>
    public class OrdealGreenDawn : OrdealBattleSequencer
    {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty)
        {
            int minCards = 3;
            int oneMoreDiff = difficulty + 1, twoMoreDiff = difficulty + 2;
            List<EncounterBlueprintData.CardBlueprint> turn1 = new(), turn2 = new(), turn3 = new();
            encounterData.Blueprint.turns = new() { turn1, turn2, turn3 };
            switch (difficulty)
            {
                case 0:
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtA, Cards.doubtB, oneMoreDiff));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtA, Cards.doubtB, oneMoreDiff));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, twoMoreDiff));
                    break;
                case 1:
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, oneMoreDiff));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, twoMoreDiff));
                    turn3.Add(HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, twoMoreDiff));
                    break;
                default:
                    minCards++;
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, oneMoreDiff));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, oneMoreDiff));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, twoMoreDiff));
                    turn3.Add(EncounterManager.NewCardBlueprint(Cards.doubtO));
                    break;
            }

            if (encounterData.Difficulty - difficulty > 1)
            {
                minCards++;
                turn3.Add(HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, 3));
            }

            return minCards;
        }
    }
}