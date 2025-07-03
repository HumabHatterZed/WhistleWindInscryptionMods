using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// The one and only Indigo Ordeal.
    /// Indigo Ordeal is an onslaught of cards that can heal themselves when dealing damage and have additional attacks.
    /// Cards appear in a steady stream.
    /// Cards required: 5
    /// Valid regions: 0, 1, 2
    /// </summary>
    public class OrdealIndigoNoon : OrdealBattleSequencer {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            int num = 0;
            int numTurns = 5 + encounterData.Difficulty / 7;
            int seed = base.GetRandomSeed();

            for (int i = 0; i < numTurns; i++) {
                float randVal = SeededRandom.Value(seed++);
                List<EncounterBlueprintData.CardBlueprint> turn = new();
                num++;

                if (randVal <= 0.33f) {
                    turn.Add(EncounterManager.NewCardBlueprint(Cards.sweeper));
                }
                else if (randVal <= 0.67f) {
                    turn.Add(EncounterManager.NewCardBlueprint(Cards.sweeper2));
                }
                else {
                    turn.Add(EncounterManager.NewCardBlueprint(Cards.sweeper3));
                }

                if (i % 2 == 0) {
                    encounterData.Blueprint.AddTurn();
                }
                else if (i % 3 == 0) {
                    num++;
                    randVal = SeededRandom.Value(seed++);
                    if (randVal <= 0.33f) {
                        turn.Add(EncounterManager.NewCardBlueprint(Cards.sweeper));
                    }
                    else if (randVal <= 0.67f) {
                        turn.Add(EncounterManager.NewCardBlueprint(Cards.sweeper2));
                    }
                    else {
                        turn.Add(EncounterManager.NewCardBlueprint(Cards.sweeper3));
                    }
                }

                encounterData.Blueprint.AddTurn(turn);
            }
            return num;
        }
    }
}