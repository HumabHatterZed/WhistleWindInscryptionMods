using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// The weakest Green Ordeal.
    /// Green Ordeals should have simple blueprints, with cards appearing quickly.
    /// Dawn will have cards appear in quick succession of each other.
    /// Cards required: 3, 4, 4
    /// Valid regions: 0
    /// </summary>
    public class OrdealGreenDawn : OrdealBattleSequencer {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            int minCards = 4;

            List<CardInfo> startingCard = new() { null, null, null };
            List<EncounterBlueprintData.CardBlueprint> turn1 = new(), turn2 = new();

            switch (baseDifficulty) {
                case 0:
                    startingCard.Add(CardLoader.GetCardByName(encounterData.Difficulty > 2 ? Cards.doubtB : Cards.doubtA));
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtA, Cards.doubtB, 2));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, 3));
                    minCards = 3;
                    break;
                case 1:
                    startingCard.Add(CardLoader.GetCardByName(encounterData.Difficulty > 3 ? Cards.doubtB : Cards.doubtA));
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, 4));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, 3));
                    turn2.Add(HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, 4));
                    break;
                default:
                    startingCard.Add(CardLoader.GetCardByName(Cards.doubtB));
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, 8));
                    turn1.Add(HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, 11));
                    turn2.Add(EncounterManager.NewCardBlueprint(Cards.doubtO));
                    break;
            }
            encounterData.Blueprint.AddTurns(turn1, turn2);

            EncounterData.StartCondition cond = new();
            cond.cardsInOpponentSlots = startingCard.Randomize().ToArray();
            encounterData.startConditions.Add(cond);

            if (encounterData.Difficulty > 9) {
                encounterData.Blueprint.AddTurn(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, 3));
                minCards++;
            }

            if (baseDifficulty > 4) {
                float strongCeiling = (baseDifficulty - 6) * 0.05f;
                for (int i = 0; i < (baseDifficulty - 4) / 2; i++) {
                    if (UnityEngine.Random.value <= strongCeiling) {
                        encounterData.Blueprint.AddTurn(HelperMethods.NewDifficultyCard(Cards.doubtY, Cards.doubtO, 12));
                    }
                    else {
                        encounterData.Blueprint.AddTurn(HelperMethods.NewDifficultyCard(Cards.doubtB, Cards.doubtY, 9));
                    }
                    minCards++;
                }
            }

            LobotomyPlugin.Log.LogInfo($"[Green Dawn] Base: {baseDifficulty} Diff: {encounterData.Difficulty}");
            return minCards;
        }
    }
}