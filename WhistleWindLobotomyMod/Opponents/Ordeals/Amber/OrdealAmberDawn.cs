using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// The weakest Amber Ordeal.
    /// Crimson Ordeals are themed around constantly spawning enemies.
    /// Dawn will have cards appear one after the other with no downtime.
    /// Cards required: 4, 6, 9,...
    /// Valid regions: 0, 1
    /// </summary>
    public class OrdealAmberDawn : OrdealBattleSequencer {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty) {
            int num = 0;
            int numTurns = 3;
            if (encounterData.Difficulty > 3) {
                numTurns += 1 + (encounterData.Difficulty - 3) / 2;
            }

            for (int i = 0; i < numTurns; i++) {
                List<EncounterBlueprintData.CardBlueprint> turn = new();
                if (i % 3 == 0) {
                    turn.Add(EncounterManager.NewCardBlueprint(Cards.perfectFood));
                    num++;
                }
                if (i > 2 && i % 2 == 0) {
                    turn.Add(EncounterManager.NewCardBlueprint(Cards.perfectFood));
                    num++;
                }
                turn.Add(EncounterManager.NewCardBlueprint(Cards.perfectFood));
                num++;

                encounterData.Blueprint.AddTurn(turn);
            }
            return num;
        }
    }
}