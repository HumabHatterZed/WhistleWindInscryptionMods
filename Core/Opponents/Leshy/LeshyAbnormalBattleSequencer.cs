using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
	public class LeshyAbnormalBattleSequencer : LeshyBattleSequencer
    {
		private new LeshyBossOpponent Leshy => Singleton<TurnManager>.Instance.Opponent as LeshyAbnormalBossOpponent;
		public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
		{
			EncounterData encounterData = base.BuildCustomEncounter(nodeData);
			encounterData.Blueprint = AbnormalEncounterData.LeshyAbnormalBossP1;
            int num = (SaveFile.IsAscension ? (-1) : 0);
            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, nodeData.difficulty + RunState.Run.DifficultyModifier + num);
			EncounterData.StartCondition startCondition = new EncounterData.StartCondition();
			startCondition.cardsInOpponentSlots[2] = CardLoader.GetCardByName("wstl_apostleMoleman");
			encounterData.startConditions.Add(startCondition);
			return encounterData;
		}
		public override IEnumerator OpponentUpkeep()
		{
			yield return this.Leshy.AdvanceMaskState();
			if (!SaveFile.IsAscension && !this.playedStinkyMoonDialogue && Singleton<BoardManager>.Instance.GetSlots(getPlayerSlots: true).Exists((CardSlot x) => x.Card != null && x.Card.HasAbility(Ability.DebuffEnemy)) && Singleton<BoardManager>.Instance.GetSlots(getPlayerSlots: false).Exists((CardSlot x) => x.Card != null && x.Card.Info.HasTrait(Trait.Giant)))
			{
				yield return new WaitForSeconds(0.5f);
				yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("LeshyBossStinkyMoon", TextDisplayer.MessageAdvanceMode.Input);
				this.playedStinkyMoonDialogue = true;
			}
		}
	}
}
