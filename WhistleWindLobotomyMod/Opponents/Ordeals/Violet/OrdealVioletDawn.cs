using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using System.Linq;
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
        private int maxFruit = 0;
        private int fruitToSpawn = 0;

        public override void TryAddOrdealRandomBuff(PlayableCard card) {
            if (card.Info.name == Cards.fruitUnderstanding) {
                CardModificationInfo mod = new();
                int decayStacks = 5;

                // first fruit has higher timer
                if (maxFruit == fruitToSpawn) {
                    decayStacks++;
                }
                if (Opponent.Difficulty > 7) {
                    decayStacks--;
                    if (Opponent.Difficulty > 13) {
                        decayStacks--;
                    }
                }

                // last fruit has reduced timer
                if (fruitToSpawn == 1) {
                    decayStacks--;
                }
                for (int i = 0; i < decayStacks; i++) {
                    mod.abilities.Add(StartingDecay.ability);
                }
                card.AddTemporaryMod(mod);
                card.OnStatsChanged();
                fruitToSpawn--;
            }
        }
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            int minCards = 2;
            HighestPositiveScaleBalance = Mathf.Min(4, 5 - RunState.Run.DifficultyModifier - 2 * RunState.CurrentRegionTier);
            List<EncounterBlueprintData.CardBlueprint> turn = new() {
                EncounterManager.NewCardBlueprint(Cards.fruitUnderstanding),
                EncounterManager.NewCardBlueprint(Cards.fruitUnderstanding)
            };

            if (encounterData.Difficulty > 8) {
                CardInfo[] startOnBoard = new CardInfo[] {
                        CardLoader.GetCardByName(Cards.fruitUnderstanding),
                        CardLoader.GetCardByName(Cards.fruitUnderstanding),
                        null,
                        null
                };
                startOnBoard = startOnBoard.Randomize().ToArray();
                EncounterData.StartCondition cond = new() {
                    cardsInOpponentSlots = startOnBoard
                };
                encounterData.startConditions.Add(cond);
                minCards++;
            }
            else {
                if (encounterData.Difficulty > 3) {
                    turn.Add(EncounterManager.NewCardBlueprint(Cards.fruitUnderstanding));
                    minCards++;
                }
                if (encounterData.Difficulty > 5) {
                    turn.Add(EncounterManager.NewCardBlueprint(Cards.fruitUnderstanding));
                    minCards++;
                }
            }

            fruitToSpawn = maxFruit = minCards;
            encounterData.Blueprint.AddTurn(turn);
            return minCards;
        }
    }
}