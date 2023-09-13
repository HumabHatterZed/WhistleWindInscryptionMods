using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents.Leshy
{
    public class LeshyAbnormalBattleSequencer : LeshyBattleSequencer
    {
        private new LeshyBossOpponent Leshy => Singleton<TurnManager>.Instance.Opponent as LeshyAbnormalBossOpponent;
        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
        {
            EncounterData encounterData = base.BuildCustomEncounter(nodeData);
            encounterData.Blueprint = LobotomyEncounterManager.LeshyAbnormalBossP1;
            int num = SaveFile.IsAscension ? -1 : 0;
            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, nodeData.difficulty + RunState.Run.DifficultyModifier + num, false);
            encounterData.startConditions.Clear();
            EncounterData.StartCondition startCondition = new();
            startCondition.cardsInOpponentSlots[2] = CardLoader.GetCardByName("wstl_apostleMoleman");
            encounterData.startConditions.Add(startCondition);
            return encounterData;
        }
        public override IEnumerator OpponentUpkeep()
        {
            yield return Leshy.AdvanceMaskState();
            if (!SaveFile.IsAscension && !playedStinkyMoonDialogue && Singleton<BoardManager>.Instance.GetSlots(getPlayerSlots: true).Exists((x) => x.Card != null && x.Card.HasAbility(Ability.DebuffEnemy)) && Singleton<BoardManager>.Instance.GetSlots(getPlayerSlots: false).Exists((x) => x.Card != null && x.Card.Info.HasTrait(Trait.Giant)))
            {
                yield return new WaitForSeconds(0.5f);
                yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("LeshyBossStinkyMoon", TextDisplayer.MessageAdvanceMode.Input);
                playedStinkyMoonDialogue = true;
            }
        }
    }
}
