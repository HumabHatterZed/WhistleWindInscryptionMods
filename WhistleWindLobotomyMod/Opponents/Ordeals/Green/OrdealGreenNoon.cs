using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents
{
    public class OrdealGreenNoon : OrdealBattleSequencer
    {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty)
        {
            ValidCards.Add(Cards.whereWeReach);
            List<EncounterBlueprintData.CardBlueprint> turn1 = new(), turn2 = new();
            encounterData.Blueprint.AddTurn(EncounterManager.NewCardBlueprint(Cards.whereWeReach));
            return 2;
        }
    }
}