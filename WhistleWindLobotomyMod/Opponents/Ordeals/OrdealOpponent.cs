using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents
{
    public class OrdealOpponent : LobotomyOpponent
    {
        public override Type ID => OrdealUtils.OpponentID;
        public override string DefeatedPlayerDialogue => "Not good enough.";
        public OrdealBattleSequencer BattleSequencer => TurnManager.Instance.SpecialSequencer as OrdealBattleSequencer;

        public bool hasTotem;
        private Color totemGlowColour;

        /// <remarks>
        /// Insert empty turns when the queue is full so we don't skip over any cards.
        /// </remarks>
        public override IEnumerator QueueNewCards(bool doTween = true, bool changeView = true) {
            if (NumTurnsTaken < TurnPlan.Count && Queue.Count == BoardManager.Instance.OpponentSlotsCopy.Count)
                TurnPlan.Insert(NumTurnsTaken, new());

            yield return base.QueueNewCards(doTween, changeView);
        }

        /// <summary>
        /// Add additional call to the sequence's 
        /// </summary>
        /// <param name="card"></param>
        public override void ModifySpawnedCard(PlayableCard card) {
            base.ModifySpawnedCard(card);
            BattleSequencer.ModifySpawnedCard(card);
        }

        public override void ModifyQueuedCard(PlayableCard card) {
            base.ModifyQueuedCard(card);
            BattleSequencer.ModifyQueuedCard(card);
        }

        public override IEnumerator PostResetScalesSequence() {
            if (NumLives == 0) {
                Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
                yield return new WaitForSeconds(0.25f);
                yield return DialogueHelper.PlayDialogueEvent("DefeatedOrdealOpponent");
            }
        }

        public override IEnumerator IntroSequence(EncounterData encounter)
        {
            yield return base.IntroSequence(encounter);
            AudioController.Instance.FadeOutLoop(0.1f, 0, 1);

            OrdealBannerManager.Instance.DisplayBanner(BattleSequencer.ordealType, true);
            this.SetSceneEffectsShown(true);
            //AudioController.Instance.SetLoopAndPlay("first_warning", 1);
            AudioController.Instance.SetLoopVolumeImmediate(0.3f, 1);
            OrdealCounterManager.Instance.SetShown(true);
            yield return new WaitForSeconds(1.5f);

            if (hasTotem) {
                encounter.opponentTotem = EncounterBuilder.BuildOpponentTotem(DominantTribe, encounter.Difficulty, TotemAbilitiesBlacklist);
                yield return base.AssembleTotem(encounter.opponentTotem, Vector3.zero, Vector3.zero, totemGlowColour, true);
            }

            Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            if (!ProgressionData.LearnedMechanic(OrdealUtils.OrdealBattle)) {
                yield return new WaitUntil(() => !OrdealBannerManager.Instance.Displaying);
                ViewManager.Instance.SwitchToView(View.Default);
                yield return new WaitForSeconds(0.4f);
                yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("OrdealFirstIntro", TextDisplayer.MessageAdvanceMode.Input);
                ProgressionData.SetMechanicLearned(OrdealUtils.OrdealBattle);
            }

            ViewManager.Instance.SwitchToView(OrdealUtils.ViewCounter);
            yield return new WaitForSeconds(0.2f);
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

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
        }

        public override void SetSceneEffectsShown(bool shown) {
            Singleton<TableVisualEffectsManager>.Instance.SetDustParticlesActive(!shown);
            if (!shown) {
                Singleton<TableVisualEffectsManager>.Instance.ResetTableColors();
                return;
            }

            Color cardLightColour;
            Color mainHighlightColour, mainDefaultColour;
            Color queueHighlightColour, queueDefaultColour;
            switch (BattleSequencer.ordealType) {
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

        public override void InitialiseOpponent(EncounterData encounter) {
            base.InitialiseOpponent(encounter);
            if (BattleSequencer.BlacklistedAbilities != null) {
                TotemAbilitiesBlacklist.AddRange(BattleSequencer.BlacklistedAbilities);
            }
            hasTotem = encounter.opponentTotem != null;
            OrdealBannerManager.Instance.UpdateBanner(BattleSequencer.ordealType, BattleSequencer.ordealTier);
            OrdealCounterManager.Instance.UpdateConsole(BattleSequencer.ordealTier, BattleSequencer.MinNumCardsRequired);
            totemGlowColour = BattleSequencer.ordealType switch {
                OrdealType.Green => GameColors.Instance.darkLimeGreen,
                OrdealType.Violet => GameColors.Instance.purple,
                OrdealType.Crimson => GameColors.Instance.glowRed,
                OrdealType.Amber => GameColors.Instance.orange,
                OrdealType.Indigo => GameColors.Instance.blue,
                _ => GameColors.Instance.gray,
            };
        }
    }
}
