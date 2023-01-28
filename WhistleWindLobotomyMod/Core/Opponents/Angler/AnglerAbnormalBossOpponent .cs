using DiskCardGame;
using System.Collections;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public class AnglerAbnormalBossOpponent : AnglerBossOpponent
    {
        public override IEnumerator StartNewPhaseSequence()
        {
            if (base.HasGrizzlyGlitchPhase(0))
            {
                yield return AbnormalGrizzlySequence.ApostleGlitchSequence(this);
                yield break;
            }
            base.TurnPlan.Clear();
            if (StoryEventsData.EventCompleted(StoryEvent.LeshyDefeated) && !StoryEventsData.EventCompleted(StoryEvent.LukeVODieAlready))
            {
                VoiceOverPlayer.Instance.PlayVoiceOver("Die already!", "VO_diealready", VoiceOverPlayer.VOCameraAnim.MediumRefocus, StoryEvent.LukeVODieAlready);
                yield return new WaitForSeconds(0.5f);
            }
            yield return this.PlaceBaitSequence();
            yield return this.ReplaceWithCustomBlueprint(AbnormalEncounterData.AnglerAbnormalBossP2, removeLockedCards: true);
        }
    }
}
