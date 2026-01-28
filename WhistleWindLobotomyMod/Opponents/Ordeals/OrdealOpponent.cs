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
    public class OrdealOpponent : LobotomyOpponent {
        public override Type ID => OrdealUtils.OpponentID;
        public override string DefeatedPlayerDialogue => "Not good enough.";
        public override int StartingLives => 1;
        public OrdealBattleSequencer BattleSequencer => TurnManager.Instance.SpecialSequencer as OrdealBattleSequencer;

        public bool hasTotem;
        private Color totemGlowColour;

        // Override Sweeper totem light so they light up correctly
        public override Color InteractablesGlowColor => BattleSequencer.ordealType == OrdealType.Indigo ? GameColors.Instance.glowRed : OrdealUtils.GetOrdealColor(BattleSequencer.ordealType);

        public bool IsBoss() => BattleSequencer.ordealTier == 3 || BattleSequencer.ordealType == OrdealType.White;

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

        public override List<List<CardInfo>> ModifyTurnPlan(List<List<CardInfo>> turnPlan) {
            if (LobotomyConfigManager.ChallengeIsActive(QlippothMeltdown.Id)) {
                string key = BattleSequencer.ordealType switch {
                    OrdealType.Green => "Mechanical",
                    OrdealType.Violet => "Divine",
                    OrdealType.Crimson => "Fae",
                    OrdealType.Amber => "Insect",
                    _ => "Anthropoid"
                };
                return QlippothCards.AddEmpoweredCardsToTurnPlan(turnPlan, key);
            }
            return base.ModifyTurnPlan(turnPlan);
        }

        public override IEnumerator PostResetScalesSequence() {
            if (NumLives == 0) {
                Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
                yield return new WaitForSeconds(0.25f);
                yield return DialogueHelper.PlayDialogueEvent("DefeatedOrdealOpponent");
            }
        }

        public override IEnumerator IntroSequence(EncounterData encounter) {
            OrdealPatches.AllowMoveToCounterView(ViewManager.Instance.Controller, ViewManager.Instance.Controller.controlMode);
            // change NumLives here since we can't do it in the sequencer
            if (RunState.CurrentRegionTier == 3) {
                this.NumLives = 3;
                if (BattleSequencer.ordealType == OrdealType.White) {
                    this.NumLives = 4;
                }
                base.SpawnScenery("CityTableEffects");
                AudioController.Instance.PlaySound2D("giant_head_rising", MixerGroup.TableObjectsSFX, 0.2f);
                yield return new WaitForSeconds(2f);
            }

            yield return base.IntroSequence(encounter);
            AudioController.Instance.FadeOutLoop(0.1f, 0, 1);
            if (IsBoss()) {
                yield return this.ReducePlayerLivesSequence();
                yield return new WaitForSeconds(0.25f);
            }

            OrdealBannerManager.Instance.DisplayBanner(BattleSequencer.ordealType, true);
            this.SetSceneEffectsShown(true);
            //AudioController.Instance.SetLoopAndPlay("first_warning", 1);
            AudioController.Instance.SetLoopVolumeImmediate(0.3f, 1);
            OrdealCounterManager.Instance.SetShown(true);
            yield return new WaitForSeconds(1.5f);

            if (hasTotem) {
                yield return base.AssembleTotem(encounter.opponentTotem, Vector3.zero, Vector3.zero, totemGlowColour, true);
            }

            Singleton<OpponentAnimationController>.Instance.ClearLookTarget();

            bool firstOrdeal = !LobotomySaveManager.LearnedOrdeals;
            if (firstOrdeal) {
                yield return new WaitUntil(() => !OrdealBannerManager.Instance.Displaying);
                ViewManager.Instance.SwitchToView(View.Default);
                yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("OrdealFirstIntro", TextDisplayer.MessageAdvanceMode.Input);
                LobotomySaveManager.LearnedOrdeals = true;
            }

            ViewManager.Instance.SwitchToView(OrdealUtils.ViewCounter);
            yield return new WaitForSeconds(0.2f);

            if (firstOrdeal || BattleSequencer.HighestPositiveScaleBalance < 4) {
                OrdealCounterManager.Instance.UpdateIconRenderer(OrdealUtils.GetScaleLockSprite(BattleSequencer.HighestPositiveScaleBalance));
                OrdealCounterManager.Instance.UpdateConsole(-1, BattleSequencer.HighestPositiveScaleBalance, "scale lock");
                OrdealCounterManager.Instance.EnableConsole(true);
                yield return new WaitForSeconds(0.8f);
                if (firstOrdeal) {
                    yield return new WaitForSeconds(0.7f);
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

            // if this is a boss ordeal, restore life and setup a rare card sequence
            if (IsBoss()) {
                if (RunState.CurrentRegionTier == 3) {
                    yield return DefeatedFinalBossSequence();
                }
                else {
                    yield return DefeatedBossSequence();
                }
            }

            Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
        }

        public IEnumerator DefeatedFinalBossSequence() {
            Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
            yield return new WaitForSeconds(0.5f);
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetColor(GameColors.Instance.nearBlack);
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetIntensity(1f, float.MaxValue);
            AudioController.Instance.StopAllLoops();
            Singleton<InteractionCursor>.Instance.SetHidden(hidden: true);
            yield return new WaitForSeconds(3f);
            if (SaveFile.IsAscension) {
                this.EndAscensionRun();
                SceneLoader.Load("Ascension_Configure");
            }
            else {
                Singleton<TurnManager>.Instance.PostBattleSpecialNode = new VictoryFeastNodeData();
                (Singleton<GameFlowManager>.Instance.SpecialSequencer as FinaleGameFlowSequencer).TrySetTutorialFlag();
                AudioListener.volume = 0f;
            }
        }
        private IEnumerator DefeatedBossSequence() {
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
        public override bool RespondsToCustomExhaustSequence(CardDrawPiles drawPiles) => true;
        public override IEnumerator DoCustomExhaustSequence(CardDrawPiles drawPiles) {
            if (drawPiles.turnsSinceExhausted == 0) {
                yield return DialogueHelper.PlayDialogueEvent("OrdealExhausted");
            }

            Singleton<ViewManager>.Instance.SwitchToView(View.Default, immediate: false, lockAfter: true);
            yield return new WaitForSeconds(0.1f);

            if (drawPiles.turnsSinceExhausted > 7) {
                yield return Singleton<LifeManager>.Instance.ShowDamageSequence(1, 1, toPlayer: true);
                BattleSequencer.HighestPositiveScaleBalance--; // really show the player i hate them
            }

            List<PlayableCard> opponentCards = BoardManager.Instance.GetOpponentCards().Concat(Queue).ToList();
            if (opponentCards.Count > 0) {
                for (int i = 0; i < 1 + drawPiles.turnsSinceExhausted; i++) {
                    PlayableCard card = opponentCards.GetRandom();
                    if (card.HasTrait(Trait.Terrain)) {
                        card.AddTemporaryMod(new(Withering.ability) { fromCardMerge = true });
                    }
                    else {
                        card.AddTemporaryMod(new(1, 0));
                    }
                    card.Anim.LightNegationEffect();
                }
            }
            yield return new WaitForSeconds(0.2f);
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
                    mainHighlightColour = mainDefaultColour = GameColors.Instance.limeGreen;
                    queueHighlightColour = queueDefaultColour = GameColors.Instance.darkSeafoam;
                    break;
                case OrdealType.Violet:
                    cardLightColour = OrdealUtils.GetOrdealColor(OrdealType.Violet); // custom colour, use to sync usages
                    mainHighlightColour = mainDefaultColour = GameColors.Instance.purple;
                    queueHighlightColour = queueDefaultColour = GameColors.Instance.darkPurple;
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
                    cardLightColour = GameColors.Instance.brightBlue;
                    mainHighlightColour = mainDefaultColour = GameColors.Instance.brightBlue;
                    queueHighlightColour = queueDefaultColour = GameColors.Instance.darkBlue;
                    break;
                default:
                    cardLightColour = GameColors.Instance.gray;
                    mainHighlightColour = mainDefaultColour = GameColors.Instance.lightGray;
                    queueHighlightColour = queueDefaultColour = Color.gray;
                    break;
            }
    ;
            Color ordealCol = OrdealUtils.GetOrdealColor(BattleSequencer.ordealType);
            mainDefaultColour.a = 0.5f;
            queueDefaultColour.a = 0.5f;
            Singleton<TableVisualEffectsManager>.Instance.ChangeTableColors(
                ordealCol,
                cardLightColour,
                totemGlowColour,
                mainDefaultColour,
                mainHighlightColour,
                ordealCol,
                queueDefaultColour,
                queueHighlightColour,
                ordealCol);
        }
        public override void InitialiseOpponent(EncounterData encounter) {
            base.InitialiseOpponent(encounter);
            hasTotem = encounter.opponentTotem != null;
            
            totemGlowColour = InteractablesGlowColor;
            OrdealBannerManager.Instance.UpdateBanner(BattleSequencer.ordealType, BattleSequencer.ordealTier);
            OrdealCounterManager.Instance.UpdateConsole(BattleSequencer.ordealTier, BattleSequencer.MinNumCardsRequired);
        }
    }
}
