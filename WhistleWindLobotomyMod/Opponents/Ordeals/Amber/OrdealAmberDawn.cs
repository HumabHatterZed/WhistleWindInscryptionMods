using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// Appears in R0
    /// Difficulty range: (1 - 3) +[0,2] // 4 difficulty is for boss node w/o challlenges
    /// 
    /// D | Turn 1 | Turn 2 | Turn 3 | Turn 4 | ## | HP | Atk
    /// 1 | P      | P P    | P      | P P    | 6  | 6  | 6
    /// 2 | P P    | P P    | P P    | P P    | 8  | 8  | 8
    /// 3 | P P    | P P P  | P P    | P P P  | 10 | 10 | 10
    /// 4 | P P P  | P P P  | P P P  | P P P  | 12 | 12 | 12
    /// 5 | P P P  | P P P P| P P P  | P P P P| 14 | 14 | 14
    /// </summary>
    public class OrdealAmberDawn : OrdealBattleSequencer {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty) {
            List<EncounterBlueprintData.CardBlueprint> turn1 = new()
            {
                EncounterManager.NewCardBlueprint(Cards.perfectFood)
            };
            List<EncounterBlueprintData.CardBlueprint> turn2 = new()
            {
                EncounterManager.NewCardBlueprint(Cards.perfectFood),
                EncounterManager.NewCardBlueprint(Cards.perfectFood)
            };
            List<EncounterBlueprintData.CardBlueprint> turn3 = new()
            {
                EncounterManager.NewCardBlueprint(Cards.perfectFood)
            };
            List<EncounterBlueprintData.CardBlueprint> turn4 = new()
            {
                EncounterManager.NewCardBlueprint(Cards.perfectFood),
                EncounterManager.NewCardBlueprint(Cards.perfectFood)
            };

            for (int i = 2; i < encounterData.Difficulty + 1; i++) {
                if (i % 2 == 0) {
                    turn1.Add(EncounterManager.NewCardBlueprint(Cards.perfectFood));
                    turn3.Add(EncounterManager.NewCardBlueprint(Cards.perfectFood));
                }
                else {
                    turn2.Add(EncounterManager.NewCardBlueprint(Cards.perfectFood));
                    turn4.Add(EncounterManager.NewCardBlueprint(Cards.perfectFood));
                }

                if (turn1.Count >= 4 && turn2.Count >= 4) break;
            }

            encounterData.Blueprint.AddTurns(turn1, turn2, turn3, turn4);
            return -1;
        }
    }
}