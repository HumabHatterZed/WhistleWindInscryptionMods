using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// Difficulty (5 - 9) [+2]
    /// 
    /// D | Turn 1 | Turn 2 | Turn 3 | ## | HP | Atk
    /// 5 | H      | -      | H      | 2  | X  | X
    /// 7 | H      | H      |        | 2
    /// </summary>
    public class OrdealCrimsonNoon : OrdealBattleSequencer {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty) {
            List<EncounterBlueprintData.CardBlueprint> turn1 = new()
            {
                EncounterManager.NewCardBlueprint(Cards.skinHarmony)
            };
            List<EncounterBlueprintData.CardBlueprint> turn2 = new()
            {
                EncounterManager.NewCardBlueprint(Cards.skinHarmony)
            };

            encounterData.Blueprint.AddTurn(turn1);
            if (encounterData.Difficulty < 7)
                encounterData.Blueprint.AddTurn();

            encounterData.Blueprint.AddTurn(turn2);

            return -1;
        }
    }
}