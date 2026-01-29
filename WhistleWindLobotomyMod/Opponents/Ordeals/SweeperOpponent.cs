using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents {
    public class SweeperOpponent : OrdealOpponent {
        public override Type ID => OrdealUtils.SweeperOpponentID;
        public override string DefeatedPlayerDialogue => "The streets have been cleaned.";
        public override int StartingLives => 3;
        private ParticleSystem left;
        private ParticleSystem right;
        private ParticleSystem middle;
        public void StopEmissions() {
            var leftEmission = left.emission;
            var rightEmission = right.emission;
            leftEmission.rateOverTime = 0;
            rightEmission.rateOverTime = 0;
        }
        public void EmitCentre() {
            base.StartCoroutine(TheRumbling());
            middle.Play(); // 1.5s emission
        }
        private IEnumerator TheRumbling() {
            TableVisualEffectsManager.Instance.ThumpTable(0.2f);
            yield return new WaitForSeconds(0.01f);
            TableVisualEffectsManager.Instance.ThumpTable(0.3f);
            yield return new WaitForSeconds(0.01f);
            TableVisualEffectsManager.Instance.ThumpTable(0.2f);
            yield return new WaitForSeconds(0.01f);
            TableVisualEffectsManager.Instance.ThumpTable(0.3f);
            yield return new WaitForSeconds(0.01f);
            TableVisualEffectsManager.Instance.ThumpTable(0.2f);
            yield return new WaitForSeconds(0.01f);
        }
        public override IEnumerator PostResetScalesSequence() {
            if (NumLives == 0) {
                Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
                yield return new WaitForSeconds(0.25f);
                yield return DialogueHelper.PlayDialogueEvent("DefeatedSweeperOpponent");
            }
        }

        public override IEnumerator IntroSequence(EncounterData encounter) {
            OrdealPatches.AllowMoveToCounterView(ViewManager.Instance.Controller, ViewManager.Instance.Controller.controlMode);

            base.SpawnScenery("SweeperTableEffects");
            AudioController.Instance.PlaySound2D("giant_head_rising", MixerGroup.TableObjectsSFX, 0.2f);
            Transform child = sceneryObject.transform.GetChild(1);
            left = child.GetChild(0).GetComponent<ParticleSystem>();
            right = child.GetChild(1).GetComponent<ParticleSystem>();
            middle = child.GetChild(2).GetComponent<ParticleSystem>();
            yield return new WaitForSeconds(2f);

            InitialiseOpponent(encounter);

            AudioController.Instance.FadeOutLoop(0.1f, 0, 1);
            yield return this.ReducePlayerLivesSequence();
            yield return new WaitForSeconds(0.25f);
            yield return DialogueHelper.PlayDialogueEvent("SweeperOrdealIntro");

            ViewManager.Instance.SwitchToView(View.Default);
            OrdealBannerManager.Instance.DisplayBanner(BattleSequencer.ordealType, true);
            this.SetSceneEffectsShown(true);
            EmitCentre();
            left.Play();
            right.Play();
            AudioController.Instance.SetLoopAndPlay("first_trumpet", 1); // sweeper music???
            AudioController.Instance.SetLoopVolumeImmediate(0.8f, 1);
            OrdealCounterManager.Instance.SetShown(true);
            yield return new WaitForSeconds(1.5f);

            if (hasTotem) {
                yield return base.AssembleTotem(encounter.opponentTotem, Vector3.zero, Vector3.zero, totemGlowColour, true);
            }

            Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            ViewManager.Instance.SwitchToView(OrdealUtils.ViewCounter);
            yield return new WaitForSeconds(0.2f);

            if (BattleSequencer.HighestPositiveScaleBalance < 4) {
                OrdealCounterManager.Instance.UpdateIconRenderer(OrdealUtils.GetScaleLockSprite(BattleSequencer.HighestPositiveScaleBalance));
                OrdealCounterManager.Instance.UpdateConsole(-1, BattleSequencer.HighestPositiveScaleBalance, "scale lock");
                OrdealCounterManager.Instance.EnableConsole(true);
                yield return new WaitForSeconds(1.5f);

                if (BattleSequencer.HighestPositiveScaleBalance < 0 && LifeManager.Instance.Balance > BattleSequencer.HighestPositiveScaleBalance) {
                    yield return LifeManager.Instance.ShowDamageSequence(-BattleSequencer.HighestPositiveScaleBalance, 1, toPlayer: true);
                    yield return new WaitForSeconds(0.5f);
                }

                OrdealCounterManager.Instance.EnableConsole(false);
                yield return new WaitForSeconds(0.5f);
                OrdealCounterManager.Instance.ResetToDisplayRemaining(BattleSequencer.ordealTier);
            }

            OrdealCounterManager.Instance.EnableConsole(true);
            yield return new WaitForSeconds(0.8f);
            ViewManager.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.2f);

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }

        public override IEnumerator OutroSequence(bool wasDefeated) {
            if (!BattleSequencer.defeated) {
                OrdealBannerManager.Instance.UpdateBannerOutro(BattleSequencer.ordealType, BattleSequencer.ordealTier);
                OrdealBannerManager.Instance.DisplayBanner(BattleSequencer.ordealType, false);
                yield return new WaitForSeconds(2f);
            }

            if (hasTotem) {
                Singleton<ViewManager>.Instance.SwitchToView(View.OpponentTotem, immediate: false, lockAfter: true);
                yield return new WaitForSeconds(0.5f);
                Singleton<OpponentAnimationController>.Instance.SetLookTarget(base.totem.transform, Vector3.up * 2f + Vector3.back * 2f);
                yield return base.DisassembleTotem();
            }
            AudioController.Instance.FadeOutLoop(0.5f, 0, 1);
            this.SetSceneEffectsShown(false);
            yield return HelperMethods.ChangeCurrentView(View.Default, 0.7f);
            OrdealCounterManager.Instance.EnableConsole(false);
            yield return new WaitForSeconds(0.25f);
            OrdealCounterManager.Instance.SetShown(false);
            yield return new WaitForSeconds(1.5f);

            yield return DefeatedFinalBossSequence();

            Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
        }
    }
}
