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

        public override IEnumerator PostResetScalesSequence() {
            if (NumLives == 0) {
                Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
                yield return new WaitForSeconds(0.25f);
                yield return DialogueHelper.PlayDialogueEvent("DefeatedSweeperOpponent");
            }
        }

        public override IEnumerator IntroSequence(EncounterData encounter) {
            OrdealPatches.AllowMoveToCounterView(ViewManager.Instance.Controller, ViewManager.Instance.Controller.controlMode);

            Debug.Log("effects");
            base.SpawnScenery("SweeperTableEffects");
            AudioController.Instance.PlaySound2D("giant_head_rising", MixerGroup.TableObjectsSFX, 0.2f);
            yield return new WaitForSeconds(2f);
            Debug.Log("posteffects");
            InitialiseOpponent(encounter);

            AudioController.Instance.FadeOutLoop(0.1f, 0, 1);
            yield return this.ReducePlayerLivesSequence();
            yield return new WaitForSeconds(0.25f);
            yield return DialogueHelper.PlayDialogueEvent("SweeperOrdealIntro");

            OrdealBannerManager.Instance.DisplayBanner(BattleSequencer.ordealType, true);
            this.SetSceneEffectsShown(true);
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
                yield return new WaitForSeconds(0.8f);
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

            // if this is a boss ordeal, restore life and setup a rare card sequence
            yield return DefeatedFinalBossSequence();

            Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
        }

        //public override bool RespondsToCustomExhaustSequence(CardDrawPiles drawPiles) => true;
        //public override IEnumerator DoCustomExhaustSequence(CardDrawPiles drawPiles) {
        //    if (drawPiles.turnsSinceExhausted == 0) {
        //        yield return DialogueHelper.PlayDialogueEvent("OrdealExhausted");
        //    }

        //    Singleton<ViewManager>.Instance.SwitchToView(View.Default, immediate: false, lockAfter: true);
        //    yield return new WaitForSeconds(0.1f);

        //    if (drawPiles.turnsSinceExhausted > 7) {
        //        yield return Singleton<LifeManager>.Instance.ShowDamageSequence(1, 1, toPlayer: true);
        //        BattleSequencer.HighestPositiveScaleBalance--; // really show the player i hate them
        //    }

        //    List<PlayableCard> opponentCards = BoardManager.Instance.GetOpponentCards().Concat(Queue).ToList();
        //    if (opponentCards.Count > 0) {
        //        for (int i = 0; i < 1 + drawPiles.turnsSinceExhausted; i++) {
        //            PlayableCard card = opponentCards.GetRandom();
        //            if (card.HasTrait(Trait.Terrain)) {
        //                card.AddTemporaryMod(new(Withering.ability) { fromCardMerge = true });
        //            }
        //            else {
        //                card.AddTemporaryMod(new(1, 0));
        //            }
        //            card.Anim.LightNegationEffect();
        //        }
        //    }
        //    yield return new WaitForSeconds(0.2f);
        //}
    }
}
