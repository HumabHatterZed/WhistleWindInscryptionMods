using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents
{
    // accounts for totem variant
    public class OrdealOpponent : Part1Opponent, IKillPlayerSequence, IPreventInstantWin, ICustomExhaustSequence
    {
        public bool totemOpponent;
        private Color totemGlowColour;

        public override bool GiveCurrencyOnDefeat => false; // excess damage is still counted even with a cap in place, so we give currency AFTER the Ordeal ends
        public static OrdealBattleSequencer BattleSequencer => TurnManager.Instance.SpecialSequencer as OrdealBattleSequencer;

        public virtual bool PreventInstantWin(bool timeMachine, CardSlot triggeringSlot)
        {
            return false;
        }
        public virtual IEnumerator OnInstantWinPrevented(bool timeMachine, CardSlot triggeringSlot)
        {
            yield break;
        }
        public virtual IEnumerator OnInstantWinTriggered(bool timeMachine, CardSlot triggeringSlot)
        {
            yield break;
        }

        public virtual bool RespondsToCustomExhaustSequence(CardDrawPiles drawPiles)
        {
            return false;
        }

        public virtual IEnumerator DoCustomExhaustSequence(CardDrawPiles drawPiles)
        {
            yield break;
        }

        public bool RespondsToKillPlayerSequence()
        {
            return false;
        }
        public virtual IEnumerator KillPlayerSequence()
        {
            yield break;
        }

        /// <summary>
        /// Insert empty turns when the queue is full so we don't skip over any cards
        /// </summary>
        public override IEnumerator QueueNewCards(bool doTween = true, bool changeView = true)
        {
            if (NumTurnsTaken < TurnPlan.Count && Queue.Count == 4)
                TurnPlan.Insert(NumTurnsTaken, new());

            yield return base.QueueNewCards(doTween, changeView);
        }
        public override void ModifySpawnedCard(PlayableCard card)
        {
            base.ModifySpawnedCard(card);
            BattleSequencer.ModifySpawnedCard(card);
        }

        public override void ModifyQueuedCard(PlayableCard card)
        {
            base.ModifyQueuedCard(card);
            BattleSequencer.ModifyQueuedCard(card);
        }

        public override IEnumerator LifeLostSequence()
        {
            Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
            yield return new WaitForSeconds(0.25f);
            yield return DialogueHelper.PlayDialogueEvent("DefeatedOrdealOpponent");
        }

        public override IEnumerator IntroSequence(EncounterData encounter)
        {
            totemOpponent = encounter.opponentTotem != null;
            OrdealBannerManager.Instance.UpdateBanner(BattleSequencer.ordealType, BattleSequencer.ordealTier);
            OrdealCounterManager.Instance.UpdateConsole(BattleSequencer.ordealTier, BattleSequencer.MinNumCardsRequired);
            AudioController.Instance.FadeOutLoop(0.1f, 0, 1);

            totemGlowColour = BattleSequencer.ordealType switch
            {
                OrdealType.Green => GameColors.Instance.darkLimeGreen,
                OrdealType.Violet => GameColors.Instance.purple,
                OrdealType.Crimson => GameColors.Instance.glowRed,
                OrdealType.Amber => GameColors.Instance.orange,
                OrdealType.Indigo => GameColors.Instance.blue,
                _ => GameColors.Instance.gray,
            };
            base.StartCoroutine(DisplayBanner(BattleSequencer.ordealType, true));
            this.SetSceneEffectsShown(true);
            //AudioController.Instance.SetLoopAndPlay("first_warning", 1);
            //AudioController.Instance.SetLoopVolumeImmediate(0.3f, 0.8f);
            OrdealCounterManager.Instance.SetShown(true);
            yield return new WaitForSeconds(1.5f);

            if (totemOpponent)
                yield return base.AssembleTotem(encounter.opponentTotem, Vector3.zero, Vector3.zero, totemGlowColour, true);

            Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            if (!ProgressionData.LearnedMechanic(OrdealUtils.OrdealBattle))
            {
                yield return new WaitUntil(() => !OrdealBannerManager.Instance.Displaying);
                ViewManager.Instance.SwitchToView(View.Default);
                yield return new WaitForSeconds(0.5f);
                yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("OrdealFirstIntro", TextDisplayer.MessageAdvanceMode.Input);
                ProgressionData.SetMechanicLearned(OrdealUtils.OrdealBattle);
            }
            yield return HelperMethods.ChangeCurrentView(OrdealUtils.ViewCounter, startDelay: 0f);
            OrdealCounterManager.Instance.EnableConsole(true);
            yield return new WaitForSeconds(1f);
            ViewManager.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.2f);

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }

        public override IEnumerator OutroSequence(bool wasDefeated)
        {
            OrdealBannerManager.Instance.UpdateBannerOutro(BattleSequencer.ordealType, BattleSequencer.ordealTier);
            base.StartCoroutine(DisplayBanner(BattleSequencer.ordealType, false));
            yield return new WaitForSeconds(2f);
            if (totemOpponent)
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.OpponentTotem, immediate: false, lockAfter: true);
                yield return new WaitForSeconds(0.5f);
                Singleton<OpponentAnimationController>.Instance.SetLookTarget(base.totem.transform, Vector3.up * 2f + Vector3.back * 2f);
                yield return base.DisassembleTotem();
            }
            AudioController.Instance.FadeOutLoop(0.5f, 0, 1);
            this.SetSceneEffectsShown(showEffects: false);
            yield return HelperMethods.ChangeCurrentView(View.Default, 0.7f);
            OrdealCounterManager.Instance.EnableConsole(false);
            yield return new WaitForSeconds(0.25f);
            OrdealCounterManager.Instance.SetShown(false);
            yield return new WaitForSeconds(1.5f);

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
        }

        private IEnumerator DisplayBanner(OrdealType ordeal, bool intro)
        {
            LobotomyPlugin.Log.LogInfo($"DisplayBanner [{ordeal}] Intro:{intro}");
            string audioName = ordeal.ToString() + "_" + (intro ? "start" : "end");
            AudioController.Instance.PlaySound2D(audioName, MixerGroup.TableObjectsSFX);
            OrdealBannerManager.Instance.ShowBanner();
            yield return new WaitForSeconds(3f);

            /*OrdealBannerManager.Instance.UpdateBanner(OrdealType.White, 0);
            yield return new WaitForSeconds(3f);
            OrdealBannerManager.Instance.UpdateBannerOutro(OrdealType.White, 0);
            yield return new WaitForSeconds(3f);

            OrdealBannerManager.Instance.UpdateBanner(OrdealType.White, 1);
            yield return new WaitForSeconds(3f);
            OrdealBannerManager.Instance.UpdateBannerOutro(OrdealType.White, 1);
            yield return new WaitForSeconds(3f);

            OrdealBannerManager.Instance.UpdateBanner(OrdealType.White, 2);
            yield return new WaitForSeconds(3f);
            OrdealBannerManager.Instance.UpdateBannerOutro(OrdealType.White, 2);
            yield return new WaitForSeconds(3f);

            OrdealBannerManager.Instance.UpdateBanner(OrdealType.White, 3);
            yield return new WaitForSeconds(3f);
            OrdealBannerManager.Instance.UpdateBannerOutro(OrdealType.White, 3);
            yield return new WaitForSeconds(3f);*/

            OrdealBannerManager.Instance.HideBanner();
            yield return new WaitForSeconds(2f);
        }

        private void SetSceneEffectsShown(bool showEffects)
        {
            Singleton<TableVisualEffectsManager>.Instance.SetDustParticlesActive(!showEffects);
            if (!showEffects)
            {
                Singleton<TableVisualEffectsManager>.Instance.ResetTableColors();
                return;
            }

            Color cardLightColour;
            Color mainHighlightColour, mainDefaultColour;
            Color queueHighlightColour, queueDefaultColour;
            switch (BattleSequencer.ordealType)
            {
                case OrdealType.Green:
                    cardLightColour = GameColors.Instance.seafoam;
                    mainHighlightColour = mainDefaultColour = GameColors.Instance.darkLimeGreen;
                    queueHighlightColour = queueDefaultColour = GameColors.Instance.darkSeafoam;
                    break;
                case OrdealType.Violet:
                    cardLightColour = GameColors.Instance.fuschia;
                    mainHighlightColour = mainDefaultColour = GameColors.Instance.darkPurple;
                    queueHighlightColour = queueDefaultColour = GameColors.Instance.darkFuschia;
                    break;
                case OrdealType.Crimson:
                    cardLightColour = GameColors.Instance.orange;
                    mainHighlightColour = mainDefaultColour = GameColors.Instance.darkRed;
                    queueHighlightColour = queueDefaultColour = GameColors.Instance.brownOrange;
                    break;
                case OrdealType.Amber:
                    cardLightColour = GameColors.Instance.marigold;
                    mainHighlightColour = mainDefaultColour = GameColors.Instance.brownOrange;
                    queueHighlightColour = queueDefaultColour = GameColors.Instance.darkGold;
                    break;
                case OrdealType.Indigo:
                    cardLightColour = GameColors.Instance.purple;
                    mainHighlightColour = mainDefaultColour = GameColors.Instance.darkBlue;
                    queueHighlightColour = queueDefaultColour = GameColors.Instance.darkPurple;
                    break;
                default:
                    cardLightColour = GameColors.Instance.nearWhite;
                    mainHighlightColour = mainDefaultColour = GameColors.Instance.gray;
                    queueHighlightColour = queueDefaultColour = GameColors.Instance.nearBlack;
                    break;
            };

            mainDefaultColour.a = 0.5f;
            queueDefaultColour.a = 0.5f;
            Singleton<TableVisualEffectsManager>.Instance.ChangeTableColors(
                totemGlowColour,
                cardLightColour,
                totemGlowColour,
                mainDefaultColour,
                mainHighlightColour,
                totemGlowColour,
                queueDefaultColour,
                queueHighlightColour,
                totemGlowColour);
        }
    }
}
