using DiskCardGame;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents.PirateSkull
{
    public class PirateSkullAbnormalBattleSequencer : PirateSkullBattleSequencer
    {
        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
        {
            EncounterData encounterData = base.BuildCustomEncounter(nodeData);
            encounterData.Blueprint = LobotomyEncounterManager.PirateSkullAbnormalBossP1;
            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, 20, false);
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
