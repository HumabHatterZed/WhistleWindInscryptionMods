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
        public override bool GiveCurrencyOnDefeat => false;
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
            base.StartCoroutine(OrdealUtils.ShakeBoard());
            middle.Play(); // 1.5s emission
        }
        public override IEnumerator PostResetScalesSequence() {
            if (NumLives == 0) {
                Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
                yield return new WaitForSeconds(0.25f);
                yield return DialogueHelper.PlayDialogueEvent("DefeatedSweeperOpponent");
            }
        }

        public override IEnumerator PreIntroBannerSequence(EncounterData encounter) {
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
        }
        public override void SetSceneEffectsShown(bool shown) {
            base.SetSceneEffectsShown(shown);
            EmitCentre();
            left.Play();
            right.Play();
        }
        public override void SetMusicLoop() {
            AudioController.Instance.SetLoopAndPlay("sweeper_theme", 1);
        }
        //public override IEnumerator IntroSequence(EncounterData encounter) {
        //    //OrdealPatches.AllowMoveToCounterView(ViewManager.Instance.Controller, ViewManager.Instance.Controller.controlMode);


        //    //OrdealBannerManager.Instance.DisplayBanner(BattleSequencer.ordealType, true);
        //    //this.SetSceneEffectsShown(true);

        //    //AudioController.Instance.SetLoopAndPlay("sweeper_theme", 1); // sweeper music???
        //    //AudioController.Instance.SetLoopVolumeImmediate(0.8f, 1);
        //    //OrdealDisplayConsole.Instance.SetShown(true);
        //    //yield return new WaitForSeconds(1.5f);

        //    //if (hasTotem) {
        //    //    yield return base.AssembleTotem(encounter.opponentTotem, Vector3.zero, Vector3.zero, totemGlowColour, true);
        //    //}

        //    //Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
        //    //ViewManager.Instance.SwitchToView(OrdealUtils.ViewCounter);
        //    //yield return new WaitForSeconds(0.2f);

        //    //if (BattleSequencer.HighestPositiveScaleBalance < 4) {
        //    //    OrdealDisplayConsole.Instance.UpdateIconRenderer(OrdealUtils.GetScaleLockSprite(BattleSequencer.HighestPositiveScaleBalance));
        //    //    OrdealDisplayConsole.Instance.UpdateConsole(-1, BattleSequencer.HighestPositiveScaleBalance, "scale lock");
        //    //    OrdealDisplayConsole.Instance.EnableConsole(true);
        //    //    yield return new WaitForSeconds(1.5f);

        //    //    if (BattleSequencer.HighestPositiveScaleBalance < 0 && LifeManager.Instance.Balance > BattleSequencer.HighestPositiveScaleBalance) {
        //    //        yield return LifeManager.Instance.ShowDamageSequence(-BattleSequencer.HighestPositiveScaleBalance, 1, toPlayer: true);
        //    //        yield return new WaitForSeconds(0.5f);
        //    //    }

        //    //    OrdealDisplayConsole.Instance.EnableConsole(false);
        //    //    yield return new WaitForSeconds(0.5f);
        //    //    OrdealDisplayConsole.Instance.ResetToDisplayRemaining(BattleSequencer.ordealTier);
        //    //}

        //    OrdealDisplayConsole.Instance.EnableConsole(true);
        //    yield return new WaitForSeconds(0.8f);
        //    ViewManager.Instance.SwitchToView(View.Default);
        //    yield return new WaitForSeconds(0.2f);

        //    Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        //}

        public override IEnumerator OutroSequence(bool wasDefeated) {
            StopEmissions();
            yield return base.OutroSequence(wasDefeated);
            //if (!BattleSequencer.defeated) {
            //    OrdealBannerManager.Instance.UpdateBannerOutro(BattleSequencer.ordealType, BattleSequencer.ordealTier);
            //    OrdealBannerManager.Instance.DisplayBanner(BattleSequencer.ordealType, false);
            //    yield return new WaitForSeconds(2f);
            //}
            
            //if (hasTotem) {
            //    Singleton<ViewManager>.Instance.SwitchToView(View.OpponentTotem, immediate: false, lockAfter: true);
            //    yield return new WaitForSeconds(0.5f);
            //    Singleton<OpponentAnimationController>.Instance.SetLookTarget(base.totem.transform, Vector3.up * 2f + Vector3.back * 2f);
            //    yield return base.DisassembleTotem();
            //}
            //AudioController.Instance.FadeOutLoop(0.5f, 0, 1);
            //this.SetSceneEffectsShown(false);
            //yield return HelperMethods.ChangeCurrentView(View.Default, 0.7f);
            //OrdealDisplayConsole.Instance.EnableConsole(false);
            //yield return new WaitForSeconds(0.25f);
            //OrdealDisplayConsole.Instance.SetShown(false);
            //yield return new WaitForSeconds(1.5f);

            //yield return DefeatedFinalBossSequence();

            //Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            //Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            //Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
        }
    }
}
