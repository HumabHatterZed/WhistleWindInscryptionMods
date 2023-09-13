using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents.Prospector
{
    public class ProspectorAbnormalBattleSequencer : ProspectorBattleSequencer
    {
        // Replace blueprint and AI with custom versions
        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
        {
            EncounterData encounterData = base.BuildCustomEncounter(nodeData);
            encounterData.Blueprint = LobotomyEncounterManager.ProspectorAbnormalBossP1;
            int num = !StoryEventsData.EventCompleted(StoryEvent.TutorialRunCompleted) ? 1 : 0;
            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, nodeData.difficulty + RunState.Run.DifficultyModifier + num, false);
            encounterData.aiId = ProspectorAbnormalAI.ID;
            return encounterData;
        }

        // Replaces Bloodhound with Bad Wolf
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            if (attacker != null && attacker.OpponentCard && attacker.Info.name == "wstl_willBeBadWolf")
            {
                return !bloodhoundMessageShown;
            }
            return false;
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            bloodhoundMessageShown = true;
            StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear("Git 'em!", 3f));
            yield return new WaitForSeconds(0.75f);
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (!muleMessageShown)
            {
                return card.Info.name == "wstl_RUDOLTA_MULE";
            }
            return false;
        }

        // Alters message to say reindeer
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            muleMessageShown = true;
            Singleton<ViewManager>.Instance.SwitchToView(View.BossCloseup);
            yield return new WaitForSeconds(0.1f);
            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Dag nab it! My reindeer!");
        }
    }
}
