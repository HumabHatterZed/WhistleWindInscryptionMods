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
    /// <summary>
    /// Difficulty (5 - 9) [+2]
    /// 
    /// D | Turn 1 | Turn 2 | Turn 3 | ## | HP | Atk
    /// 5 | H      | -      | H      | 2  | X  | X
    /// 7 | H      | H      |        | 2
    /// </summary>
    public class OrdealCrimsonNoon : OrdealBattleSequencer
    {
        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            List<EncounterBlueprintData.CardBlueprint> turn1 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_skinHarmony")
            };
            List<EncounterBlueprintData.CardBlueprint> turn2 = new()
            {
                EncounterManager.NewCardBlueprint("wstl_skinHarmony")
            };

            encounterData.Blueprint.AddTurn(turn1);
            if (encounterData.Difficulty < 7)
                encounterData.Blueprint.AddTurn();

            encounterData.Blueprint.AddTurn(turn2);

            return encounterData;
        }
    }
}