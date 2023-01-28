using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Core.SpecialSequencers;

namespace WhistleWind.LobotomyMod.Core.Opponents.Angler
{
    public class AnglerAbnormalBossOpponent : AnglerBossOpponent
    {
        public override IEnumerator StartNewPhaseSequence()
        {
            if (HasGrizzlyGlitchPhase(0))
            {
                yield return AbnormalGrizzlySequence.ApostleGlitchSequence(this);
                yield break;
            }
            TurnPlan.Clear();
            if (StoryEventsData.EventCompleted(StoryEvent.LeshyDefeated) && !StoryEventsData.EventCompleted(StoryEvent.LukeVODieAlready))
            {
                VoiceOverPlayer.Instance.PlayVoiceOver("Die already!", "VO_diealready", VoiceOverPlayer.VOCameraAnim.MediumRefocus, StoryEvent.LukeVODieAlready);
                yield return new WaitForSeconds(0.5f);
            }
            yield return PlaceBaitSequence();
            yield return this.ReplaceWithCustomBlueprint(AbnormalEncounterData.AnglerAbnormalBossP2, removeLockedCards: true);
        }
    }
}
