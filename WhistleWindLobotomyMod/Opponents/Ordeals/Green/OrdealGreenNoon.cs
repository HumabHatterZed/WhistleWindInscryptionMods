using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// The second-weakest Green Ordeal.
    /// Green Ordeals should have simple blueprints, with cards appearing quickly.
    /// Noon Ordeals will appear every other turn, with higher difficulties making them appear sooner.
    /// Cards required: 3, 3, 4
    /// Valid regions: 1
    /// </summary>
    public class OrdealGreenNoon : OrdealBattleSequencer {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            int minCards = 3;
            int oneAboveBase = baseDifficulty + 2;
            List<CardInfo> startingCard = new() { null, null, null, null };

            switch (baseDifficulty) {
                case 6:
                    encounterData.Blueprint
                        .AddTurn(EncounterManager.NewCardBlueprint(Cards.doubtProcessDown))
                        .AddTurn()
                        .AddTurn(EncounterManager.NewCardBlueprint(Cards.doubtProcess))
                        .AddTurn()
                        .AddTurn(EncounterManager.NewCardBlueprint(Cards.doubtProcess));
                    break;
                case 7:
                    encounterData.Blueprint
                        .AddTurn(EncounterManager.NewCardBlueprint(Cards.doubtProcessDown))
                        .AddTurn()
                        .AddTurn(EncounterManager.NewCardBlueprint(Cards.doubtProcess))
                        .AddTurn(EncounterManager.NewCardBlueprint(Cards.doubtProcess));
                    break;
                default:
                    encounterData.Blueprint
                        .AddTurn(EncounterManager.NewCardBlueprint(Cards.doubtProcessDown))
                        .AddTurn(EncounterManager.NewCardBlueprint(Cards.doubtProcessDown))
                        .AddTurn(EncounterManager.NewCardBlueprint(Cards.doubtProcess))
                        .AddTurn(EncounterManager.NewCardBlueprint(Cards.doubtProcess));
                    minCards = 4;
                    break;
            }

            if (encounterData.Difficulty > 8) {
                startingCard[0] = CardLoader.GetCardByName(Cards.doubtProcessDown2);
                minCards++;
            }

            EncounterData.StartCondition cond = new();
            startingCard.Randomize();
            cond.cardsInOpponentSlots = startingCard.ToArray();
            encounterData.startConditions.Add(cond);

            LobotomyPlugin.Log.LogInfo($"[Green Noon] Base: {baseDifficulty} Diff: {encounterData.Difficulty}");
            return minCards;
        }
    }
}