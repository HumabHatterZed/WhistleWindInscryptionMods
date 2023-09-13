using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Core.SpecialSequencers;

namespace WhistleWindLobotomyMod.Opponents.Angler
{
    public class AnglerAbnormalBossOpponent : AnglerBossOpponent
    {
        public override IEnumerator StartNewPhaseSequence()
        {
            if (HasGrizzlyGlitchPhase(0))
            {
                // do Apostles only if it's ascension and the Grizzly challenge is active
                if (SaveFile.IsAscension)
                    yield return AbnormalGrizzlySequence.ApostleGlitchSequence(this);
                else
                    yield return GrizzlyGlitchSequence();

                yield break;
            }
            TurnPlan.Clear();
            if (StoryEventsData.EventCompleted(StoryEvent.LeshyDefeated) && !StoryEventsData.EventCompleted(StoryEvent.LukeVODieAlready))
            {
                VoiceOverPlayer.Instance.PlayVoiceOver("Die already!", "VO_diealready", VoiceOverPlayer.VOCameraAnim.MediumRefocus, StoryEvent.LukeVODieAlready);
                yield return new WaitForSeconds(0.5f);
            }
            yield return PlaceBaitSequence();
            yield return this.ReplaceWithCustomBlueprint(LobotomyEncounterManager.AnglerAbnormalBossP2, removeLockedCards: true);
        }
    }
}
