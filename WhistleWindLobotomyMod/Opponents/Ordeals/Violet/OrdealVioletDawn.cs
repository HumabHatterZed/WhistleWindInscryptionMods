using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// Appears in R0
    /// Difficulty range: (1 - 3) +[0,2] // 4 difficulty is for boss node w/o challlenges
    /// 
    /// D | Turn 1 | Turn 2 | Turn 3 | ## | HP | Atk
    /// 1 | F4     | F3     | F3     | 3  | 12 | 0
    /// 3 | F3     | F3     | F2     | 3  | 12 | 0
    /// 5 | F3     | F2 F2  | F2     | 4  | 16 | 0
    /// </summary>
    public class OrdealVioletDawn : OrdealBattleSequencer {
        public override void ModifyQueuedCard(PlayableCard card) {
            if (Opponent.Difficulty < RunState.Run.regionTier * 6 + 2) {
                if (Opponent.NumTurnsTaken == 0)
                    card.AddTemporaryMod(new() { abilities = new() { StartingDecay.ability, StartingDecay.ability } });
                else
                    card.AddTemporaryMod(new() { abilities = new() { StartingDecay.ability } });
            }
            else if (Opponent.NumTurnsTaken < 2)
                card.AddTemporaryMod(new() { abilities = new() { StartingDecay.ability } });
        }

        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            int minCards = 3;
            List<EncounterBlueprintData.CardBlueprint> turn1 = new() {
                EncounterManager.NewCardBlueprint(Cards.fruitUnderstanding)
            };
            List<EncounterBlueprintData.CardBlueprint> turn2 = new() {
                EncounterManager.NewCardBlueprint(Cards.fruitUnderstanding)
            };
            List<EncounterBlueprintData.CardBlueprint> turn3 = new() {
                EncounterManager.NewCardBlueprint(Cards.fruitUnderstanding)
            };

            if (encounterData.Difficulty > baseDifficulty + 1) {
                minCards++;
                turn2.Add(EncounterManager.NewCardBlueprint(Cards.fruitUnderstanding));
            }

            encounterData.Blueprint.AddTurns(turn1, turn2, turn3);
            if (baseDifficulty < 3)
                encounterData.Blueprint.AddTurn();

            return minCards;
        }
    }
}