using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// The weakest Crimson Ordeal.
    /// Crimson Ordeals are themed around compounding threats, with Ordeals appearing in groups and stronger tiers splitting into weaker ones.
    /// Dusk features 2 Climaxes who appear one after the other, with the delay between shortening with higher difficulties.
    /// Cards required: 8
    /// Valid regions: 1, 2
    /// </summary>
    public class OrdealCrimsonDusk : OrdealBattleSequencer {
        public override void ModifyQueuedCard(PlayableCard card) {
            if (card.Info.name != Cards.skinClimax) {
                base.ModifyQueuedCard(card);
            }
            else if (Opponent.Difficulty > 12) {
                card.Info.Mods.Add(new(Opponent.Difficulty > 14 ? 1 : 0, Mathf.Max(0, Opponent.Difficulty - 12)));
                card.OnStatsChanged();
            }
        }
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty) {
            List<EncounterBlueprintData.CardBlueprint> turn = new() {
                EncounterManager.NewCardBlueprint(Cards.skinClimax)
            };
            if (encounterData.Difficulty < 8) {
                encounterData.Blueprint.AddTurn();
            }
            encounterData.Blueprint.AddTurn(turn);
            
            if (RunState.CurrentRegionTier < 2) {
                encounterData.Blueprint.AddTurn();
            }
            if (difficulty < 13) {
                encounterData.Blueprint.AddTurn();
            }
            encounterData.Blueprint.AddTurn(turn);
            return 8;
        }
    }
}