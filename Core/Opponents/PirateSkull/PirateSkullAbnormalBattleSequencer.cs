using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static WhistleWindLobotomyMod.EncounterHelper;

namespace WhistleWindLobotomyMod
{
    public class PirateSkullAbnormalBattleSequencer : PirateSkullBattleSequencer
    {
		public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
		{
			EncounterData encounterData = base.BuildCustomEncounter(nodeData);
			encounterData.Blueprint = AbnormalEncounterData.PirateSkullAbnormalBossP1;
			encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, 20);
			EncounterData.StartCondition startCondition = new EncounterData.StartCondition();
			startCondition.cardsInOpponentSlots[0] = CardLoader.GetCardByName("wstl_apostleMoleman");
			startCondition.cardsInOpponentSlots[3] = CardLoader.GetCardByName("wstl_apostleMoleman");
			encounterData.startConditions.Add(startCondition);
			encounterData.aiId = PirateSkullAbnormalAI.ID;
			return encounterData;
		}
	}
}
