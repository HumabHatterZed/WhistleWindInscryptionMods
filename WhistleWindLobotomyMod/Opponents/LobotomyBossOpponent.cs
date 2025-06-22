using DiskCardGame;
using System.Collections;
using UnityEngine;

namespace WhistleWindLobotomyMod.Opponents {
    public abstract class LobotomyBossOpponent : LobotomyOpponent {
        public Animator MasterAnimator;
        public GameObject bossObjectAnimation;

        public override IEnumerator IntroSequence(EncounterData encounter) {
            yield return base.IntroSequence(encounter);
            RunState.CurrentMapRegion.FadeOutAmbientAudio();
            yield return ReducePlayerLivesSequence();
            yield return new WaitForSeconds(0.4f);

            if (AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems)) {
                yield return new WaitForSeconds(0.25f);
                ChallengeActivationUI.TryShowActivation(AscensionChallenge.BossTotems);
                yield return SetUpTotem();
                if (!DialogueEventsData.EventIsPlayed("ChallengeBossTotems")) {
                    yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("ChallengeBossTotems", TextDisplayer.MessageAdvanceMode.Input);
                }

                Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
                yield return new WaitForSeconds(0.25f);
            }
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
        }

        public override IEnumerator OutroSequence(bool wasDefeated) {
            if (AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems)) {
                yield return base.DisassembleTotem();
                Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            }

            if (!wasDefeated) {
                bossObjectAnimation.transform.SetParent(null);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
