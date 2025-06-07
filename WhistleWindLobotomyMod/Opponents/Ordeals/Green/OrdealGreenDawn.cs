using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents
{
    /// <summary>
    /// The weakest Green Ordeal.
    /// Green Ordeals should have simple blueprints, with cards appearing quickly.
    /// Difficulty range: [0,3] +[1,3]
    /// Cards required: 3, 4, 5
    /// Valid regions: 0
    /// </summary>
    public class OrdealGreenDawn : OrdealBattleSequencer
    {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty)
        {
            int minCards = 4;
            //int difficultyModifier = encounterData.Difficulty - baseDifficulty - 1; // account for innate +1 modifier
            int oneAboveBase = baseDifficulty + 2, twoAboveBase = baseDifficulty + 3; // account for innate modifier
            List<CardInfo> startingCard = new() { null, null, null };
            List<EncounterBlueprintData.CardBlueprint> turn1 = new(), turn2 = new();

            switch (baseDifficulty) {
                case 0:
                    startingCard.Add(CardLoader.GetCardByName(encounterData.Difficulty > oneAboveBase ? Cards.doubtB : Cards.doubtA));
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtA, Cards.doubtB, oneAboveBase));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, twoAboveBase));
                    minCards = 3;
                    break;
                case 1:
                    startingCard.Add(CardLoader.GetCardByName(encounterData.Difficulty > oneAboveBase ? Cards.doubtB : Cards.doubtA));
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, twoAboveBase));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtA, Cards.doubtB, oneAboveBase));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, twoAboveBase));
                    break;
                default:
                    startingCard.Add(CardLoader.GetCardByName(Cards.doubtB));
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, twoAboveBase));
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, twoAboveBase));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, oneAboveBase));
                    break;
            }

            EncounterData.StartCondition cond = new();
            startingCard.Randomize();
            cond.cardsInOpponentSlots = startingCard.ToArray();
            encounterData.startConditions.Add(cond);

            encounterData.Blueprint.AddTurns(turn1, turn2);
            if (encounterData.Difficulty > oneAboveBase) {
                encounterData.Blueprint.AddTurn(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, 3));
                minCards++;
            }

            return minCards;
        }
    }
}