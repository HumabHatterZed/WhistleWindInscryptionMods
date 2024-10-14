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
    /// Appears in R0
    /// Difficulty range: (1 - 3) +[0,2] // 4 difficulty is for boss node w/o challlenges
    /// </summary>
    public class OrdealGreenMidnight : OrdealBattleSequencer
    {
        public void ConstructGreenMidnight(EncounterData encounterData)
        {
            EncounterData.StartCondition cond = new()
            {
                cardsInOpponentSlots = new CardInfo[] { CardLoader.GetCardByName("wstl_lastHelix") }
            };
            encounterData.startConditions.Add(cond);
        }

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            EncounterData.StartCondition cond = new()
            {
                cardsInOpponentSlots = new CardInfo[] { CardLoader.GetCardByName("wstl_lastHelix") }
            };
            encounterData.startConditions.Add(cond);
            return encounterData;
        }
    }
}