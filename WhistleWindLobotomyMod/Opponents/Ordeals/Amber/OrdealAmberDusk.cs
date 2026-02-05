using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// The weakest Amber Ordeal.
    /// Crimson Ordeals are themed around constantly spawning enemies.
    /// Dusk cards will summon Dawn cards, which will be added to the total required.
    /// Cards required: 2, 3, 4... (not counting Dawn cards)
    /// Valid regions: 1, 2
    /// </summary>
    public class OrdealAmberDusk : OrdealBattleSequencer {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty) {
            int num = 2;
            if (RunState.Run.regionTier > 1) {
                num++;
            }
            if (encounterData.Difficulty > 14) {
                num++;
            }
            for (int i = 0; i < num; i++) {
                List<EncounterBlueprintData.CardBlueprint> turn = new() {
                    EncounterManager.NewCardBlueprint(Cards.foodChain)
                };

                encounterData.Blueprint.AddTurn(turn).AddTurn();
            }
            return num;
        }
        public override void TryAddOrdealRandomBuff(PlayableCard card) {
            base.TryAddOrdealRandomBuff(card);
            if (card.Info.name == Cards.perfectFood) {
                amountKilledThisTurn--;
            }
        }
    }
}