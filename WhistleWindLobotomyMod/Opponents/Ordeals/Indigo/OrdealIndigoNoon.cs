using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// Indigo Ordeal is an onslaught of cards that can heal themselves when dealing damage and have additional attacks.
    /// Cards appear in a steady stream.
    /// Cards required: 5
    /// Valid regions: 0, 1, 2
    /// </summary>
    public class OrdealIndigoNoon : OrdealBattleSequencer {
        protected string GetRandomSweeper(int randomSeed, int rangeMaxExclusive = 4) {
            return SeededRandom.Range(0, rangeMaxExclusive, randomSeed) switch {
                0 => Cards.sweeperA,
                1 => Cards.sweeperB,
                2 => Cards.sweeperC,
                3 => Cards.sweeperD,
                4 => Cards.sweeperE,
                5 => Cards.sweeperF,
                _ => Cards.sweeperG
            };
        }
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            int num = 0;
            int numTurns = 3 + encounterData.Difficulty / 6;
            int seed = base.GetRandomSeed();

            for (int i = 0; i < numTurns; i++) {
                List<EncounterBlueprintData.CardBlueprint> turn = new() {
                    EncounterManager.NewCardBlueprint(GetRandomSweeper(seed++))
                };
                num++;

                if (i % 3 == 0) {
                    turn.Add(EncounterManager.NewCardBlueprint(GetRandomSweeper(seed++)));
                    num++;
                }
                else if (i % 2 == 0 && (RunState.Run.DifficultyModifier < 2 || encounterData.Blueprint.turns.Count < 6 - RunState.Run.DifficultyModifier)) {
                    // create empty turns before turns with cards
                    // if the difficulty modifier is not 1, stop adding buffer turns after X num of turns have been added
                    encounterData.Blueprint.AddTurn();
                }

                encounterData.Blueprint.AddTurn(turn);
            }
            return num;
        }
    }
}