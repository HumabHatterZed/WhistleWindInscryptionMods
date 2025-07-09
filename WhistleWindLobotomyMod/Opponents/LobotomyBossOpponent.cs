using DiskCardGame;
using System.Collections;
using UnityEngine;

namespace WhistleWindLobotomyMod.Opponents {
    public abstract class LobotomyBossOpponent : LobotomyOpponent {
        public Animator MasterAnimator { get; protected set; }
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

        public override IEnumerator DefeatedOpponentSequence() {
            AscensionStatsData.TryIncrementStat(AscensionStat.Type.BossesDefeated);
            yield return new WaitForSeconds(0.25f);
            this.DestroyScenery();
            this.SetSceneEffectsShown(shown: false);
            AudioController.Instance.StopAllLoops();
            yield return new WaitForSeconds(0.75f);
            this.CleanUpBossBehaviours();
            CustomCoroutine.WaitThenExecute(1f, LeshyAnimationController.Instance.HideArms);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default, immediate: false, lockAfter: true);
            yield return new WaitForSeconds(0.8f);
            if (RunState.Run.maxPlayerLives > 1) {
                yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("ReplenishLives", TextDisplayer.MessageAdvanceMode.Input);
                yield return new WaitForSeconds(0.25f);
                if (Random.value > 0.8f) {
                    Singleton<VideoCameraRig>.Instance.PlayCameraAnim("refocus_quick");
                }
                yield return Singleton<CandleHolder>.Instance.ReplenishFlamesSequence();
                RunState.Run.playerLives = RunState.Run.maxPlayerLives;
            }
            Singleton<TurnManager>.Instance.PostBattleSpecialNode = new ChooseRareCardNodeData();
        }
    }
}
