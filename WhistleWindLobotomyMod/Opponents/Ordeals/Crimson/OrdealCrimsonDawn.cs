using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using System.Linq;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// The weakest Crimson Ordeal.
    /// Crimson Ordeals are themed around compounding threats, with Ordeals appearing in groups and stronger tiers splitting into weaker ones.
    /// Dawn will have cards appear in pairs.
    /// Cards required: 2, 4, 6,...
    /// Valid regions: 0, 1
    /// </summary>
    public class OrdealCrimsonDawn : OrdealBattleSequencer {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty) {
            int num = 2;
            int extraTurns = encounterData.Difficulty / 3;
            if (encounterData.Difficulty > 7) {
                EncounterData.StartCondition cond = new();
                List<CardInfo> infos = new() { CardLoader.GetCardByName(Cards.skinCheers), null, null, null };
                cond.cardsInOpponentSlots = infos.Randomize().ToArray();
                encounterData.startConditions.Add(cond);
                num++;
            }
            List<EncounterBlueprintData.CardBlueprint> turn1 = new() {
                EncounterManager.NewCardBlueprint(Cards.skinCheers)
            };

            encounterData.Blueprint.AddTurn(turn1);
            if (encounterData.Difficulty < 5) {
                encounterData.Blueprint.AddTurn();

                if (encounterData.Difficulty < 3) {
                    encounterData.Blueprint.AddTurn();
                }
            }

            for (int i = 0; i < extraTurns; i++) {
                List<EncounterBlueprintData.CardBlueprint> turn = new() {
                    EncounterManager.NewCardBlueprint(Cards.skinCheers)
                };

                encounterData.Blueprint.AddTurn(turn);
                if (encounterData.Difficulty < 6) {
                    encounterData.Blueprint.AddTurn();
                }

                num++;
            }

            return num;
        }
    }
}