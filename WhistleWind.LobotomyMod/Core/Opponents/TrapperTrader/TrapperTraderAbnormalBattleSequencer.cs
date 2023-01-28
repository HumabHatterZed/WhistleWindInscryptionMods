﻿using DiskCardGame;

namespace WhistleWind.LobotomyMod.Core.Opponents.TrapperTrader
{
    public class TrapperTraderAbnormalBattleSequencer : TrapperTraderBattleSequencer
    {
        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
        {
            EncounterData encounterData = base.BuildCustomEncounter(nodeData);
            encounterData.Blueprint = AbnormalEncounterData.TrapperTraderAbnormalBossP1;
            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, nodeData.difficulty + RunState.Run.DifficultyModifier);
            EncounterData.StartCondition startCondition = new();
            startCondition.cardsInOpponentSlots[0] = CardLoader.GetCardByName("TrapFrog");
            startCondition.cardsInOpponentSlots[2] = CardLoader.GetCardByName("TrapFrog");
            startCondition.cardsInOpponentSlots[3] = CardLoader.GetCardByName("Trap");
            encounterData.startConditions.Add(startCondition);
            return encounterData;
        }
    }
}
