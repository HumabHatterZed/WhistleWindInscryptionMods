using DiskCardGame;
using EasyFeedback.APIs;
using Infiniscryption.P03KayceeRun.Encounters;
using InscryptionAPI.Encounters;
using System;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    public class OrdealCrimsonDusk : OrdealBattleSequencer
    {
        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_skinClimax")
            };
            List<EncounterBlueprintData.CardBlueprint> turn2 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_skinClimax")
            };

            encounterData.Blueprint.AddTurn().AddTurn(turn1).AddTurn().AddTurn();
            if (encounterData.Difficulty < 7)
                encounterData.Blueprint.AddTurn();

            encounterData.Blueprint.AddTurn(turn2);

            return encounterData;
        }
    }
}