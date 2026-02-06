using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

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
            int num = 0;
            int numTurns = 1 + difficulty / 3;
            if (difficulty > 3) {
                EncounterData.StartCondition cond = new();
                List<CardInfo> infos = new() { CardLoader.GetCardByName(Cards.skinCheers), null, null, null };
                if (difficulty > 7) {
                    infos[1] = CardLoader.GetCardByName(Cards.skinCheers);
                    num++;
                }
                infos.Randomize();
                cond.cardsInOpponentSlots = infos.ToArray();
                encounterData.startConditions.Add(cond);
                num ++;
            }

            for (int i = 0; i < numTurns; i++) {
                List<EncounterBlueprintData.CardBlueprint> turn = new() {
                    EncounterManager.NewCardBlueprint(Cards.skinCheers)
                };
                List<EncounterBlueprintData.CardBlueprint> turn2 = new() {
                    EncounterManager.NewCardBlueprint(Cards.skinCheers)
                };
                encounterData.Blueprint.AddTurn(turn).AddTurn(turn2).AddTurn();
                num += 2;
            }

            return num;
        }
    }
}