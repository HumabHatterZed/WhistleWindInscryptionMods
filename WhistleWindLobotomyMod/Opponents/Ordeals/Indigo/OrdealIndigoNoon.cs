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
                List<EncounterBlueprintData.CardBlueprint> turn = new();

                switch (SeededRandom.Range(0, 3, seed++)) {
                    case 0:
                        turn.Add(EncounterManager.NewCardBlueprint(Cards.sweeper));
                        break;
                    case 1:
                        turn.Add(EncounterManager.NewCardBlueprint(Cards.sweeper2));
                        break;
                    default:
                        turn.Add(EncounterManager.NewCardBlueprint(Cards.sweeper3));
                        break;
                }
                num++;

                if (i % 3 == 0) {
                    switch (SeededRandom.Range(0, 3, seed++)) {
                        case 0:
                            turn.Add(EncounterManager.NewCardBlueprint(Cards.sweeper));
                            break;
                        case 1:
                            turn.Add(EncounterManager.NewCardBlueprint(Cards.sweeper2));
                            break;
                        default:
                            turn.Add(EncounterManager.NewCardBlueprint(Cards.sweeper3));
                            break;
                    }
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