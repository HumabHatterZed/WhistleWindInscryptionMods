using DiskCardGame;
using System.Collections;
using UnityEngine;

namespace WhistleWind.LobotomyMod.Core.Opponents.Angler
{
    public class AnglerAbnormalBattleSequencer : AnglerBattleSequencer
    {
        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
        {
            EncounterData encounterData = base.BuildCustomEncounter(nodeData);
            encounterData.Blueprint = AbnormalEncounterData.AnglerAbnormalBossP1;
            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, nodeData.difficulty + RunState.Run.DifficultyModifier);
            return encounterData;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return new WaitForSeconds(0.2f);
            CardInfo cardByName = CardLoader.GetCardByName("wstl_dreamingCurrent");
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardByName, deathSlot);
            yield return new WaitForSeconds(0.25f);
            if (!sharkDialoguePlayed)
            {
                sharkDialoguePlayed = true;
                yield return new WaitForSeconds(0.5f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Go fish.", -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
