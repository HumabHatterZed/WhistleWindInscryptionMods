using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents
{
    /// <summary>
    /// The weakest Green Ordeal.
    /// As the 'standard' Ordeal type, Green Ordeals should have the simplest blueprints.
    /// Difficulty range: [1,3] +[0,2]
    /// Cards required: 3, 3, 4
    /// Valid regions: 0
    /// </summary>
    public class OrdealGreenDawn : OrdealBattleSequencer
    {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty)
        {
            int minCards = 3;
            int oneMoreDiff = baseDifficulty + 2, twoMoreDiff = baseDifficulty + 3; // account for innate +1 modifier
            List<EncounterBlueprintData.CardBlueprint> turn1 = new(), turn2 = new(), turn3 = new();
            switch (baseDifficulty)
            {
                case 0:
                    LobotomyPlugin.Log.LogDebug("Easy");
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtA, Cards.doubtB, oneMoreDiff));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtA, Cards.doubtB, oneMoreDiff));
                    turn3.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, twoMoreDiff));
                    break;
                case 1:
                    LobotomyPlugin.Log.LogDebug("Medium");
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, oneMoreDiff));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, twoMoreDiff));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, twoMoreDiff));
                    break;
                default:
                    LobotomyPlugin.Log.LogDebug("Hard");
                    minCards++;
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, oneMoreDiff));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, oneMoreDiff));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, twoMoreDiff));
                    turn3.Add(EncounterManager.NewCardBlueprint(Cards.doubtY));
                    break;
            }

            // make the entire blueprint occur 1 turn sooner
            int difficultyModifier = encounterData.Difficulty - baseDifficulty;
            if (difficultyModifier > 1)
            {
                EncounterData.StartCondition cond = new();
                turn1.Add(null);
                turn1.Add(null);
                turn1.Add(null);
                turn1.Randomize();
                cond.cardsInOpponentSlots = turn1.Select(x => difficultyModifier > 2 ? x?.replacement : x?.card).ToArray();
                encounterData.startConditions.Add(cond);
                turn1.Clear();
                turn1.AddRange(turn2);
                turn2.Clear();
                turn2.AddRange(turn3);
                turn3.Clear();

                if (difficultyModifier > 2) {
                    turn3.Add(HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, 3));
                    minCards++;
                }
            }

            encounterData.Blueprint.AddTurns(turn1, turn2, turn3);
            return minCards;
        }
    }
}