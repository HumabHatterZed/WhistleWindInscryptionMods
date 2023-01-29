﻿using DiskCardGame;
using System.Collections;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public class ProspectorAbnormalBattleSequencer : ProspectorBattleSequencer
    {
        // Replace blueprint and AI with custom versions
        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
        {
            EncounterData encounterData = base.BuildCustomEncounter(nodeData);
            encounterData.Blueprint = AbnormalEncounterData.ProspectorAbnormalBossP1;
            int num = ((!StoryEventsData.EventCompleted(StoryEvent.TutorialRunCompleted)) ? 1 : 0);
            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, nodeData.difficulty + RunState.Run.DifficultyModifier + num);
            encounterData.aiId = ProspectorAbnormalAI.ID;
            return encounterData;
        }

        // Replaces Bloodhound with Bad Wolf
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            if (attacker != null && attacker.OpponentCard && attacker.Info.name == "wstl_willBeBadWolf")
            {
                return !this.bloodhoundMessageShown;
            }
            return false;
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            this.bloodhoundMessageShown = true;
            base.StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear("Git 'em!", 3f));
            yield return new WaitForSeconds(0.75f);
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (!this.muleMessageShown)
            {
                return card.Info.name == "wstl_RUDOLTA_MULE";
            }
            return false;
        }

        // Alters message to say reindeer
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            this.muleMessageShown = true;
            Singleton<ViewManager>.Instance.SwitchToView(View.BossCloseup);
            yield return new WaitForSeconds(0.1f);
            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Dag nab it! My reindeer!");
        }
    }
}