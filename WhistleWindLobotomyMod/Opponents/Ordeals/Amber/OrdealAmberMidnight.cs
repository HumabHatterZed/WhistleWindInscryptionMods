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
    public class OrdealAmberMidnight : OrdealBattleSequencer {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty) {
            return 0;
        }
    }
}