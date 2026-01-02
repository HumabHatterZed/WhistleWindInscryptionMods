using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using System.Linq;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// The weakest Crimson Ordeal.
    /// Crimson Ordeals are themed around compounding threats, with Ordeals appearing in groups and stronger tiers splitting into weaker ones.
    /// Noon will have two Harmony of Skins appear, one after the other.
    /// Cards required: 6, 9
    /// Valid regions: 0, 1, 2
    /// </summary>
    public class OrdealCrimsonNoon : OrdealBattleSequencer {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty) {
            int num = 3;
            List<EncounterBlueprintData.CardBlueprint> turn = new() {
                EncounterManager.NewCardBlueprint(Cards.skinHarmony)
            };
            if (encounterData.Difficulty > 8) {
                EncounterData.StartCondition cond = new();
                List<CardInfo> info = new() { null, null, null, CardLoader.GetCardByName(Cards.skinHarmony) };
                cond.cardsInOpponentSlots = info.Randomize().ToArray();
                encounterData.startConditions.Add(cond);
            }
            else {
                encounterData.Blueprint.AddTurn(turn);
            }
            encounterData.Blueprint.AddTurn().AddTurn();

            if (difficulty > 11) {
                num += 3;
                encounterData.Blueprint.AddTurn(turn).AddTurn();
            }
            encounterData.Blueprint.AddTurn(turn);
            return num;
        }
    }
}