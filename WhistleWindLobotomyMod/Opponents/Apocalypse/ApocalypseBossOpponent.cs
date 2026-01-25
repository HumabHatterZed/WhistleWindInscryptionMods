using DiskCardGame;
using Pixelplacement;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents.Apocalypse {
    public class ApocalypseBossOpponent : LobotomyBossOpponent {
        public override Type ID => LobOpponentUtils.ApocalypseBossID;
        public override int StartingLives => 4;
        public override string DefeatedPlayerDialogue => "Twilight falls...";
        public override Color InteractablesGlowColor => GameColors.Instance.gold;
        public ApocalypseBattleSequencer BattleSequencer => TurnManager.Instance.SpecialSequencer as ApocalypseBattleSequencer;

        public readonly Transform[] LeftEyes = new Transform[8];
        public readonly Transform[] RightEyes = new Transform[8];
        public override IEnumerator DefeatedPlayerSequence() {
            LobotomyPlugin.Log.LogMessage($"[ApocalypseBoss] Final reactive difficulty: {BattleSequencer.ReactiveDifficulty}");
            BattleSequencer.CleanupTargetIcons();
            AudioController.Instance.FadeOutLoop(0.5f, 0);
            yield return ResetToIdle();
            yield return base.DefeatedPlayerSequence();
        }
        public override IEnumerator LifeLostSequence() {
            Singleton<InteractionCursor>.Instance.InteractionDisabled = true;

            yield return ResetToIdle();
            Singleton<ViewManager>.Instance.SwitchToView(View.Default, immediate: false, lockAfter: true);
            yield return new WaitForSeconds(1f);

            AudioController.Instance.PlaySound2D("bird_dead", MixerGroup.TableObjectsSFX);
            switch (BattleSequencer.ActiveEggEffect) {
                case ActiveEggEffect.BigEyes:
                    yield return BreakEggSequence("DefeatEyes", "ApocalypseBossBrokenEggBig");
                    break;
                case ActiveEggEffect.SmallBeak:
                    yield return BreakEggSequence("DefeatBeak", "ApocalypseBossBrokenEggSmall");
                    break;
                case ActiveEggEffect.LongArms:
                    yield return BreakEggSequence("DefeatArms", "ApocalypseBossBrokenEggLong");
                    foreach (PlayableCard card in BoardManager.Instance.CardsOnBoard.Concat(PlayerHand.Instance.CardsInHand)) {
                        yield return card.RemoveStatusEffect<Sin>();
                    }
                    break;
            }

            if (base.NumLives == 0) {
                LobotomyPlugin.Log.LogMessage($"[ApocalypseBoss] Final reactive difficulty: {BattleSequencer.ReactiveDifficulty}");
                AscensionStatsData.TryIncrementStat(AscensionStat.Type.BossesDefeated);
                yield return new WaitForSeconds(0.25f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Default, immediate: false, lockAfter: true);
                AchievementAPI.Unlock(LobotomySaveManager.DefeatedApocalypseBoss, AchievementAPI.ThroughTheTwilight);
                yield return new WaitForSeconds(0.5f);
                Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
                yield return DefeatSequence();
                yield return new WaitForSeconds(1.5f);
                Object.DontDestroyOnLoad(AudioController.Instance.PlaySound2D("candle_loseLife", MixerGroup.TableObjectsSFX).gameObject);
                Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetColor(GameColors.Instance.nearBlack);
                Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetIntensity(1f, float.MaxValue);
                AudioController.Instance.StopAllLoops();
                Singleton<InteractionCursor>.Instance.SetHidden(true);
                yield return new WaitForSeconds(2f);
                if (SaveFile.IsAscension) {
                    EndAscensionRun();
                    SceneLoader.Load("Ascension_Configure");
                }
                else {
                    Singleton<TurnManager>.Instance.PostBattleSpecialNode = new VictoryFeastNodeData();
                    (Singleton<GameFlowManager>.Instance.SpecialSequencer as FinaleGameFlowSequencer).TrySetTutorialFlag();
                    AudioListener.volume = 0f;
                }
            }
            yield return new WaitForSeconds(0.5f);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
        }
        public override IEnumerator StartNewPhaseSequence() {
            bool finalPhase = NumLives == 1;
            ReplaceAndAppendTurnPlan(new());
            Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue);

            if (!finalPhase) {
                yield return BattleSequencer.ResetAndChangeEggEffect(true);
                BattleSequencer.AddNextTurnToPlan();
            }
            else {
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossFinalPhase1", 0f);
            }

            if (AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems)) {
                if (finalPhase) {
                    TotemAbilitiesWhitelist.Clear();
                    TotemAbilitiesWhitelist.Add(Persistent.ability);
                    TotemAbilitiesWhitelist.Add(Piercing.ability);
                    TotemAbilitiesWhitelist.Add(Scorching.ability);
                }

                Singleton<ViewManager>.Instance.SwitchToView(View.OpponentTotem);
                yield return new WaitForSeconds(0.25f);
                yield return ReplaceTotemBottom();

                // immediately set the music to the climax for maximum Coolness(tm)
                AudioController.Instance.loopSources[0].time = 11.5f;
                AudioController.Instance.SetLoopVolumeImmediate(BG_VOLUME);
                yield return new WaitForSeconds(0.5f);
                Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue);
            }
            else {
                AudioController.Instance.SetLoopVolume(BG_VOLUME, 1f);
            }

            if (!finalPhase) {
                yield return BattleSequencer.MoveOpponentCards();
                LobotomyPlugin.Log.LogDebug($"[ApocalypseOpp] StartNewPhase: QueueNewCards");
                yield return QueueNewCards();
            }
            else
                yield return StartGiantPhase();
        }
        private IEnumerator StartGiantPhase() {
            LobotomyPlugin.Log.LogMessage($"[ApocalypseBoss] Giant reactive difficulty: {BattleSequencer.ReactiveDifficulty}");
            CardInfo beast = CardLoader.GetCardByName(Cards.giantApocalypse);
            if (BattleSequencer.ReactiveDifficulty > 18) {
                beast.Mods.Add(new(1, 0) { singletonId = "ReactiveStrength" });
            }

            yield return ClearBoard();
            yield return ClearQueue();
            yield return HelperMethods.ChangeCurrentView(View.Default, 0.5f);
            PlayDefeatAnimation();
            AudioController.Instance.SetLoopVolume(BG_VOLUME, 0.5f);
            AudioController.Instance.loopSources[0].pitch *= 1.1f;
            yield return new WaitForSeconds(2f);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(beast, BoardManager.Instance.OpponentSlotsCopy[0], 0.2f);
            bossObjectAnimation.transform.SetParent(BoardManager.Instance.OpponentSlotsCopy[0].Card.transform.Find("Quad"));
            bossObjectAnimation.transform.localScale = new(0.5f, 0.5f, 0.5f);
            bossObjectAnimation.transform.localPosition = new(-1.6f, -0.4f, -0.5f);
            yield return new WaitForSeconds(0.05f);
            MasterAnimator.Play("finalPhase");

            AudioController.Instance.PlaySound3D("map_slam", MixerGroup.TableObjectsSFX, Singleton<BoardManager>.Instance.transform.position);
            AudioController.Instance.PlaySound2D("bird_roar", MixerGroup.TableObjectsSFX);
            Singleton<CameraEffects>.Instance.Shake(0.5f, 0.5f);
            yield return new WaitForSeconds(2f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossFinalPhase2");
            yield return BattleSequencer.GiantPhaseLogic(true);
            yield return new WaitForSeconds(0.1f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossFinalTargets");
        }
        private IEnumerator DefeatSequence() {
            AudioController.Instance.PlaySound2D("bird_roar", MixerGroup.TableObjectsSFX);
            MasterAnimator.Play("finalDeath");
            Singleton<CameraEffects>.Instance.Shake(0.5f, 1.5f);
            yield return new WaitForSeconds(0.5f);
            yield return BoardManager.Instance.OpponentSlotsCopy[0].Card.Die(false, playSound: false);
            yield return new WaitForSeconds(1f);

            BattleSequencer.HighestPositiveScaleBalance = 5;
            int damage = Singleton<LifeManager>.Instance.DamageUntilPlayerWin - 1;
            if (damage > 0) {
                yield return LifeManager.Instance.ShowDamageSequence(damage, damage, false);
                yield return new WaitForSeconds(1f);
            }
            yield return LifeManager.Instance.ShowDamageSequence(1, 1, false);
        }
        private IEnumerator BreakEggSequence(string defeatEffect, string dialogueId) {
            MasterAnimator.SetBool(defeatEffect, true);
            Singleton<CameraEffects>.Instance.Shake(0.5f, 1f);
            yield return new WaitForSeconds(1.5f);
            yield return DialogueHelper.PlayDialogueEvent(dialogueId);
        }
        private void PlayDefeatAnimation() {
            MasterAnimator.Play("finalPhaseStart");
            MasterAnimator.Play("idle2", 1);
            MasterAnimator.SetLayerWeight(2, 0f);
            MasterAnimator.SetLayerWeight(3, 0f);
            MasterAnimator.SetLayerWeight(4, 0f);
        }
        internal IEnumerator ResetToIdle() {
            if (MasterAnimator.GetBool("Flare") || MasterAnimator.GetBool("Mouth"))
                yield return new WaitForSeconds(0.5f);

            MasterAnimator.SetBool("Flare", false);
            MasterAnimator.SetBool("Mouth", false);
        }

        public override IEnumerator IntroSequence(EncounterData encounter) {
            yield return base.IntroSequence(encounter);
            base.SpawnScenery("ForestTableEffects");
            this.sceneryObject.transform.Find("GodRaysEffect").gameObject.SetActive(false);
            (CabinManager.Instance as CabinManager)?.SetNorthWallHidden(true);

            yield return new WaitForSeconds(0.5f);
            AudioController.Instance.PlaySound2D("prospector_trees_enter", MixerGroup.TableObjectsSFX, 0.2f);
            base.StartCoroutine(StartIntroLoop());
            yield return new WaitForSeconds(1.5f);
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossPreIntro", TextDisplayer.MessageAdvanceMode.Input);

            // get rid of Leshy
            Tween.LocalPosition(
                LeshyAnimationController.Instance.head,
                LeshyAnimationController.Instance.GetBaseHeadPosition(false) + new Vector3(0f, 0f, 2f),
                0.2f, 0f, Tween.EaseInOut,
                completeCallback: () => LeshyAnimationController.Instance.head.localPosition += new Vector3(0f, -20f, 18f)
                );

            yield return new WaitForSeconds(0.75f);
            AudioController.Instance.PlaySound2D("bird_roar", MixerGroup.TableObjectsSFX);
            yield return base.FaceZoomSequence();

            bossObjectAnimation = Instantiate(LobOpponentUtils.ApocalypseBossPrefab, new Vector3(0.3f, 5.5f, 4.5f), Quaternion.identity);
            bossObjectAnimation.name = "ApocalypseBoss";

            MasterAnimator = bossObjectAnimation.GetComponent<Animator>();
            Transform eyes = bossObjectAnimation.transform.Find("Wing1").Find("Eyes");
            LeftEyes[0] = eyes.Find("Eye1");
            LeftEyes[1] = eyes.Find("Eye2");
            LeftEyes[2] = eyes.Find("Eye3");
            LeftEyes[3] = eyes.Find("Eye4");
            eyes = bossObjectAnimation.transform.Find("Wing1").Find("OuterWing").Find("Eyes");
            LeftEyes[4] = eyes.Find("Eye1");
            LeftEyes[5] = eyes.Find("Eye2");
            LeftEyes[6] = eyes.Find("Eye3");
            LeftEyes[7] = eyes.Find("Eye4");
            eyes = bossObjectAnimation.transform.Find("Wing2").Find("Eyes");
            RightEyes[0] = eyes.Find("Eye1");
            RightEyes[1] = eyes.Find("Eye2");
            RightEyes[2] = eyes.Find("Eye3");
            RightEyes[3] = eyes.Find("Eye4");
            eyes = bossObjectAnimation.transform.Find("Wing2").Find("OuterWing").Find("Eyes");
            RightEyes[4] = eyes.Find("Eye1");
            RightEyes[5] = eyes.Find("Eye2");
            RightEyes[6] = eyes.Find("Eye3");
            RightEyes[7] = eyes.Find("Eye4");

            this.SetSceneEffectsShown(true);
            Singleton<CameraEffects>.Instance.Shake(0.5f, 0.25f);
            yield return new WaitForSeconds(0.1f);
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossIntro", TextDisplayer.MessageAdvanceMode.Input);
            yield return new WaitForSeconds(1f);

            // set scales
            Singleton<ViewManager>.Instance.SwitchToView(View.Scales);
            yield return new WaitForSeconds(0.2f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossBendScales1");

            yield return LifeManager.Instance.ShowDamageSequence(BattleSequencer.HighestPositiveScaleBalance, 1, toPlayer: false);
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossBendScales2");

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossPrelude");
            MasterAnimator.SetTrigger("MoveFromEntry");
            yield return new WaitForSeconds(0.75f);
            base.StartCoroutine(StartMainLoop());

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }

        public override bool PreventInstantWin(CardSlot triggeringSlot, IPreventInstantWin.InstantWinType instantWinType) {
            if (instantWinType == IPreventInstantWin.InstantWinType.TimeMachine)
                return !BattleSequencer.DisabledEggEffects.Contains(ActiveEggEffect.LongArms);

            return true;
        }
        public override IEnumerator OnInstantWinPrevented(CardSlot triggeringSlot, IPreventInstantWin.InstantWinType instantWinType) {
            if (instantWinType == IPreventInstantWin.InstantWinType.TimeMachine)
                yield return DialogueHelper.ShowUntilInput("The Long Bird's arms conceal time.");
        }
        public override IEnumerator OnInstantWinTriggered(CardSlot triggeringSlot, IPreventInstantWin.InstantWinType instantWinType) {
            if (NumLives > 1) {
                BattleSequencer.turnsToNextPhase = 3;
                BattleSequencer.justSwitchedEffect = true;
                BattleSequencer.BossCard.Anim.StrongNegationEffect();
                BattleSequencer.CleanupTargetIcons();
                BattleSequencer.specialTargetSlots.Clear();
                foreach (GameObject obj in BattleSequencer.mouthIcons.Values)
                    BattleSequencer.CleanUpTargetIcon(obj);

                BattleSequencer.mouthIcons.Clear();
                BattleSequencer.UpdateCounter();

                foreach (PlayableCard item in Queue) {
                    GlitchOutAssetEffect.GlitchModel(item.StatsLayer.transform);
                    yield return new WaitForSeconds(0.1f);
                }
                Queue.Clear();
                foreach (CardSlot slot in BoardManager.Instance.OpponentSlotsCopy) {
                    if (slot.Card != null && slot.Card != BattleSequencer.BossCard) {
                        PlayableCard card = slot.Card;
                        slot.Card.UnassignFromSlot();
                        GlitchOutAssetEffect.GlitchModel(card.StatsLayer.transform);
                        yield return new WaitForSeconds(0.1f);
                    }
                }

                yield return new WaitForSeconds(0.5f);
                yield return DialogueHelper.ShowUntilInput("Your beasts are rejuvenated and the [c:bR]monster[c:] finds itself alone.");
            }
            else {
                yield return BattleSequencer.BossCard.TakeDamage(10, null);
                yield return BattleSequencer.GiantPhaseLogic(true);
                yield return new WaitForSeconds(0.5f);
                yield return DialogueHelper.ShowUntilInput("The [c:bR]monster[c:] changes its targets.");
            }
        }

        public override bool RespondsToCustomExhaustSequence(CardDrawPiles drawPiles) => BattleSequencer.BossCard != null;
        public override IEnumerator DoCustomExhaustSequence(CardDrawPiles drawPiles) {
            if (drawPiles.turnsSinceExhausted == 0) {
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossCardsExhausted");
            }

            CardModificationInfo mod = new(drawPiles.turnsSinceExhausted, drawPiles.turnsSinceExhausted) { singletonId = RefreshDecks.REMOVE_ON_REFRESH_ID };
            if (drawPiles.turnsSinceExhausted >= 4) {
                mod.abilities.Add(Ability.Flying);
            }

            Singleton<ViewManager>.Instance.SwitchToView(View.Board, immediate: false, lockAfter: true);
            yield return new WaitForSeconds(0.25f);
            BattleSequencer.BossCard.AddTemporaryMod(mod);
            BattleSequencer.BossCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            if (drawPiles.turnsSinceExhausted >= 8) {
                yield return Singleton<LifeManager>.Instance.ShowDamageSequence(1, 1, toPlayer: true);
            }
        }

        public override bool RespondsToKillPlayerSequence() => true;
        public override IEnumerator KillPlayerSequence() {
            MasterAnimator.SetTrigger("KillPlayer");
            MasterAnimator.SetLayerWeight(1, 0f);
            MasterAnimator.SetLayerWeight(2, 0f);
            MasterAnimator.SetLayerWeight(3, 0f);
            MasterAnimator.SetLayerWeight(4, 0f);
            yield break;
        }

        public override void InitialiseOpponent(EncounterData encounter) {
            base.InitialiseOpponent(encounter);
            TotemAbilitiesWhitelist.Add(Ability.GuardDog);
            TotemAbilitiesWhitelist.Add(Ability.Sentry);
            TotemAbilitiesWhitelist.Add(Ability.Strafe);
            TotemAbilitiesWhitelist.Add(NimbleFoot.ability);
            TotemAbilitiesWhitelist.Add(Scorching.ability);
            TotemAbilitiesWhitelist.Add(ThickSkin.ability);
        }
        public override void SetSceneEffectsShown(bool showEffects) {
            if (showEffects) {
                ApocalypseBossUtils.ChangeTableColours();
                (Singleton<ExplorableAreaManager>.Instance as CabinManager).SetWestWallHidden(hidden: true);
            }
            else {
                Singleton<TableVisualEffectsManager>.Instance.ResetTableColors();
                (Singleton<ExplorableAreaManager>.Instance as CabinManager).SetWestWallHidden(hidden: false);
            }
        }

        private IEnumerator StartIntroLoop() {
            yield return new WaitForSeconds(0.5f);
            AudioController.Instance.SetLoopAndPlay("second_trumpet_intro", looping: false);
            AudioController.Instance.SetLoopVolumeImmediate(BG_VOLUME);
            yield return new WaitUntil(() => AudioController.Instance.loopSources[0].time + 0.05f >= AudioController.Instance.loopSources[0].clip.length);
            AudioController.Instance.SetLoopAndPlay("second_trumpet_intro_loop");
            AudioController.Instance.SetLoopVolumeImmediate(BG_VOLUME);
        }
        private IEnumerator StartMainLoop() {
            yield return new WaitUntil(() => AudioController.Instance.loopSources[0].time + 0.05f >= AudioController.Instance.loopSources[0].clip.length);
            AudioController.Instance.SetLoopAndPlay("second_trumpet_main", looping: false);
            AudioController.Instance.SetLoopVolumeImmediate(BG_VOLUME);
            yield return new WaitUntil(() => AudioController.Instance.loopSources[0].time + 0.05f >= AudioController.Instance.loopSources[0].clip.length);
            AudioController.Instance.SetLoopAndPlay("second_trumpet_main_loop");
            AudioController.Instance.SetLoopVolumeImmediate(BG_VOLUME);
        }
    }
}