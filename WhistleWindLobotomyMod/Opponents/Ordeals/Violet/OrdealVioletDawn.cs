using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// The weakest Violet Ordeal.
    /// Violet Ordeals are religious themed.
    /// Dawn will have cards appear all at once, with several turns to kill all cards before they detonate.
    /// Cards required: 2, 3, 4
    /// Valid regions: 0, 1
    /// </summary>
    public class OrdealVioletDawn : OrdealBattleSequencer {
        private int fruitToSpawn = 0;
        public override void ModifyQueuedCard(PlayableCard card) {
            base.ModifyQueuedCard(card);
            if (card.Info.name != Cards.fruitUnderstanding) {
                return;
            }

            CardModificationInfo mod = new();
            int decayStacks = fruitToSpawn == 4 ? 3 : 2;
            if (Opponent.Difficulty > 5) {
                decayStacks--;
            }
            if (fruitToSpawn == 1) {
                decayStacks--;
            }
            for (int i = 0; i < decayStacks; i++) {
                mod.abilities.Add(StartingDecay.ability);
            }
            card.AddTemporaryMod(mod);
            fruitToSpawn--;
        }

        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            int minCards = 2;
            List<EncounterBlueprintData.CardBlueprint> turn = new() {
                EncounterManager.NewCardBlueprint(Cards.fruitUnderstanding),
                EncounterManager.NewCardBlueprint(Cards.fruitUnderstanding)
            };

            if (encounterData.Difficulty > 3) {
                turn.Add(EncounterManager.NewCardBlueprint(Cards.fruitUnderstanding));
                minCards++;
            }
            if (encounterData.Difficulty > 8) {
                turn.Add(EncounterManager.NewCardBlueprint(Cards.fruitUnderstanding));
                minCards++;
            }

            encounterData.Blueprint.AddTurn(turn);
            fruitToSpawn = minCards;
            return minCards;
        }
    }
}