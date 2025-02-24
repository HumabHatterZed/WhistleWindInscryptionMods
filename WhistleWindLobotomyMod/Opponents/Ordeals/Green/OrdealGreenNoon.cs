using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Opponents
{
    public class OrdealGreenNoon : OrdealBattleSequencer
    {
        public void ConstructGreenNoon(EncounterData encounterData)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1, turn2, turn3;
            turn1 = new() {
                EncounterManager.NewCardBlueprint(Cards.doubtProcess)
                };
            turn2 = new() {
                EncounterManager.NewCardBlueprint(Cards.doubtProcess)
                };
            turn3 = new() {
                EncounterManager.NewCardBlueprint(Cards.doubtProcess)
                };

            if (encounterData.Difficulty >= 6)
                turn3.Add(EncounterManager.NewCardBlueprint(Cards.doubtProcess));

            if (encounterData.Difficulty >= 8)
                turn2.Add(EncounterManager.NewCardBlueprint(Cards.doubtProcess));

            encounterData.Blueprint
                .AddTurn()
                .AddTurn(turn1)
                .AddTurn()
                .AddTurn(turn2)
                .AddTurn()
                .AddTurn(turn3);
        }

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            ConstructGreenNoon(encounterData);
            return encounterData;
        }
    }
}