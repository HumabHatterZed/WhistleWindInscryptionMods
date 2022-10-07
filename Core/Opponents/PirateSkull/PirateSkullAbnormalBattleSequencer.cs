using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Core.Opponents;

namespace WhistleWindLobotomyMod
{
    public class PirateSkullAbnormalBattleSequencer : PirateSkullBattleSequencer
    {
		public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
		{
			EncounterData encounterData = base.BuildCustomEncounter(nodeData);
			encounterData.Blueprint = AbnormalEncounterData.PirateSkullAbnormalBossP1;
			encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, 20);
			encounterData.startConditions.Clear();
			EncounterData.StartCondition startCondition = new();
			startCondition.cardsInOpponentSlots[0] = CardLoader.GetCardByName("wstl_apostleMoleman");
			startCondition.cardsInOpponentSlots[3] = CardLoader.GetCardByName("wstl_apostleMoleman");
			encounterData.startConditions.Add(startCondition);
			encounterData.aiId = PirateSkullAbnormalAI.ID;
			return encounterData;
		}
	}
}
