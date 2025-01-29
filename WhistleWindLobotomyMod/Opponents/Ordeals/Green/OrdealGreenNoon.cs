using DiskCardGame;
using Infiniscryption.P03KayceeRun.Encounters;
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
                EncounterManager.NewCardBlueprint("wstl_doubtProcess")
                };
            turn2 = new() {
                EncounterManager.NewCardBlueprint("wstl_doubtProcess")
                };
            turn3 = new() {
                EncounterManager.NewCardBlueprint("wstl_doubtProcess")
                };

            if (encounterData.Difficulty >= 6)
                turn3.Add(EncounterManager.NewCardBlueprint("wstl_doubtProcess"));

            if (encounterData.Difficulty >= 8)
                turn2.Add(EncounterManager.NewCardBlueprint("wstl_doubtProcess"));

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