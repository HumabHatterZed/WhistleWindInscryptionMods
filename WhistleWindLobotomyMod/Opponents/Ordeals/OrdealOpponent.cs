using DiskCardGame;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Tango;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using EncounterBuilder = DiskCardGame.EncounterBuilder;

namespace WhistleWindLobotomyMod.Opponents
{
    // accounts for totem variant
    public class OrdealOpponent : Part1Opponent, IKillPlayerSequence, IExhaustSequence, IPreventInstantWin
    {
        public bool totemOpponent;
        private Color totemGlowColour;
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

        public bool RespondsToExhaustSequence(CardDrawPiles drawPiles, PlayableCard giantOpponentCard)
        {
            return false;
        }
        public IEnumerator ExhaustSequence(CardDrawPiles drawPiles, PlayableCard giantOpponentCard)
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

        public override IEnumerator IntroSequence(EncounterData encounter)
        {
            totemOpponent = encounter.opponentTotem != null;
            totemGlowColour = BattleSequencer.ordealType switch
            {
                OrdealType.Green => GameColors.Instance.darkLimeGreen,
                OrdealType.Violet => GameColors.Instance.purple,
                OrdealType.Crimson => GameColors.Instance.glowRed,
                OrdealType.Amber => GameColors.Instance.orange,
                OrdealType.Indigo => GameColors.Instance.blue,
                _ => GameColors.Instance.gray
            };

            AudioController.Instance.SetLoopVolume(0.5f * (Singleton<GameFlowManager>.Instance as Part1GameFlowManager).GameTableLoopVolume, 0.5f);
            if (totemOpponent)
                yield return base.AssembleTotem(encounter.opponentTotem, Vector3.zero, Vector3.zero, totemGlowColour, true);

            AudioController.Instance.SetLoopAndPlay("red_noise", 1);
            AudioController.Instance.SetLoopVolumeImmediate(0.3f, 1);
            this.SetSceneEffectsShown(true);
            yield return new WaitForSeconds(1f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.25f);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
        }
        public override IEnumerator LifeLostSequence()
        {
            Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
            yield return new WaitForSeconds(0.25f);
            yield return DialogueHelper.PlayDialogueEvent("DefeatedOrdealOpponent");
        }
        public override IEnumerator OutroSequence(bool wasDefeated)
        {
            yield return new WaitForSeconds(0.5f);
            if (totemOpponent)
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.OpponentTotem, immediate: false, lockAfter: true);
                yield return new WaitForSeconds(0.5f);
                Singleton<OpponentAnimationController>.Instance.SetLookTarget(base.totem.transform, Vector3.up * 2f + Vector3.back * 2f);
                yield return base.DisassembleTotem();
            }
            AudioController.Instance.StopLoop(1);
            AudioController.Instance.SetLoopVolume((Singleton<GameFlowManager>.Instance as Part1GameFlowManager).GameTableLoopVolume, 0.25f);
            this.SetSceneEffectsShown(showEffects: false);
            yield return new WaitForSeconds(0.7f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.15f);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
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
