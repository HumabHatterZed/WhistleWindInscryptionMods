using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Opponents
{
    public class OrdealCrimsonDusk : OrdealBattleSequencer
    {
        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1 = new()
            {
                EncounterManager.NewCardBlueprint(Cards.skinClimax)
            };
            List<EncounterBlueprintData.CardBlueprint> turn2 = new()
            {
                EncounterManager.NewCardBlueprint(Cards.skinClimax)
            };

            encounterData.Blueprint.AddTurn().AddTurn(turn1).AddTurn().AddTurn();
            if (encounterData.Difficulty < 7)
                encounterData.Blueprint.AddTurn();

            encounterData.Blueprint.AddTurn(turn2);

            return encounterData;
        }
    }
}