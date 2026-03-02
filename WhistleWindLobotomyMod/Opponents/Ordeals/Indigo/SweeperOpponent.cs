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

        public override IEnumerator OutroSequence(bool wasDefeated) {
            StopEmissions();
            yield return base.OutroSequence(wasDefeated);
        }
    }
}
