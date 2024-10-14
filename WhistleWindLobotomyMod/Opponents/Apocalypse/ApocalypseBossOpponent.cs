using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Nodes;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Opponents.Apocalypse
{
    public class ApocalypseBossOpponent : LobotomyBossOpponent
    {
        public override Type ID => CustomOpponentUtils.ApocalypseBossID;
        public override int StartingLives => 4;
        public override string DefeatedPlayerDialogue => "Twilight falls...";
        public override Color InteractablesGlowColor => GameColors.Instance.gold;
        public ApocalypseBattleSequencer BattleSequencer => TurnManager.Instance.SpecialSequencer as ApocalypseBattleSequencer;

        public readonly List<Transform> LeftEyes = new();
        public readonly List<Transform> RightEyes = new();

        private const float BG_VOLUME = 0.3f;

        public override bool RespondsToCustomExhaustSequence(CardDrawPiles drawPiles) => NumLives == 1 && BattleSequencer.BossCard != null;
        public override IEnumerator DoCustomExhaustSequence(CardDrawPiles drawPiles)
        {
            if (drawPiles.turnsSinceExhausted == 0)
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossCardsExhausted", TextDisplayer.MessageAdvanceMode.Input);

            Singleton<ViewManager>.Instance.SwitchToView(View.Board, immediate: false, lockAfter: true);
            yield return new WaitForSeconds(0.25f);
            BattleSequencer.BossCard.AddTemporaryMod(new CardModificationInfo(1, 0));
            BattleSequencer.BossCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(1f);
        }

        public override bool RespondsToKillPlayerSequence() => true;
        public override IEnumerator KillPlayerSequence()
        {
            ApocalypseBossOpponent opponent = TurnManager.Instance.Opponent as ApocalypseBossOpponent;
            opponent.MasterAnimator.SetTrigger("KillPlayer");
            opponent.MasterAnimator.SetLayerWeight(1, 0f);
            opponent.MasterAnimator.SetLayerWeight(2, 0f);
            opponent.MasterAnimator.SetLayerWeight(3, 0f);
            opponent.MasterAnimator.SetLayerWeight(4, 0f);
            yield break;
        }

        public override IEnumerator DefeatedPlayerSequence()
        {
            BattleSequencer.CleanupTargetIcons();
            AudioController.Instance.FadeOutLoop(0.5f, 0);
            yield return ResetToIdle();
            yield return base.DefeatedPlayerSequence();
        }
        public override IEnumerator LifeLostSequence()
        {
            Singleton<InteractionCursor>.Instance.InteractionDisabled = true;

            yield return ResetToIdle();
            Singleton<ViewManager>.Instance.SwitchToView(View.Default, immediate: false, lockAfter: true);
            yield return new WaitForSeconds(1f);

            AudioController.Instance.PlaySound2D("bird_dead", MixerGroup.TableObjectsSFX);
            switch (BattleSequencer.ActiveEggEffect)
            {
                case ActiveEggEffect.BigEyes:
                    yield return DefeatBigEyesSequence();
                    break;
                case ActiveEggEffect.SmallBeak:
                    yield return DefeatSmallBeakSequence();
                    break;
                case ActiveEggEffect.LongArms:
                    yield return DefeatLongArmsSequence();
                    break;
            }

            if (base.NumLives == 0)
            {
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
                if (SaveFile.IsAscension)
                {
                    EndAscensionRun();
                    SceneLoader.Load("Ascension_Configure");
                }
                else
                {
                    Singleton<TurnManager>.Instance.PostBattleSpecialNode = new VictoryFeastNodeData();
                    (Singleton<GameFlowManager>.Instance.SpecialSequencer as FinaleGameFlowSequencer).TrySetTutorialFlag();
                    AudioListener.volume = 0f;
                }
            }
            yield return new WaitForSeconds(0.5f);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
        }
        private IEnumerator DefeatSequence()
        {
            AudioController.Instance.PlaySound2D("bird_roar", MixerGroup.TableObjectsSFX);
            MasterAnimator.Play("finalDeath");
            Singleton<CameraEffects>.Instance.Shake(0.5f, 1.5f);
            yield return new WaitForSeconds(0.5f);
            yield return BoardManager.Instance.OpponentSlotsCopy[0].Card.Die(false, playSound: false);
            yield return new WaitForSeconds(1f);

            BattleSequencer.HighestPositiveScaleBalance = 5;
            int damage = Singleton<LifeManager>.Instance.DamageUntilPlayerWin - 1;
            yield return LifeManager.Instance.ShowDamageSequence(damage, damage, false);
            yield return new WaitForSeconds(1f);
            yield return LifeManager.Instance.ShowDamageSequence(1, 1, false);
        }
        public override IEnumerator StartNewPhaseSequence()
        {
            bool finalPhase = NumLives == 1;

            TurnPlan.Clear();
            Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue);

            // switch to the next phase if it's not the last phase; otherwise remove Airborne as a possible totem ability (assuming it's still a choice)
            if (!finalPhase)
                yield return BattleSequencer.SwitchToNextEggEffect(true);

            if (AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems))
            {
                if (finalPhase)
                {
                    bossTotemAbilities.Clear();
                    bossTotemAbilities = new() { Persistent.ability, Piercing.ability, Scorching.ability };
                }
                Singleton<ViewManager>.Instance.SwitchToView(View.OpponentTotem);
                yield return new WaitForSeconds(0.25f);
                yield return ReplaceTotemBottom();

                // immediately increase the music to regular volume for maximum coolness(tm)
                AudioController.Instance.loopSources[0].time = 11.5f;
                AudioController.Instance.SetLoopVolumeImmediate(BG_VOLUME);
                yield return new WaitForSeconds(0.5f);
                Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue);
            }
            else
            {
                AudioController.Instance.SetLoopVolume(BG_VOLUME, 1f);
            }

            if (!finalPhase)
            {
                yield return BattleSequencer.MoveOpponentCards();
                yield return QueueNewCards();
            }
            else
                yield return StartGiantPhase();
        }
        public IEnumerator DefeatBigEyesSequence()
        {
            MasterAnimator.SetBool("DefeatEyes", true);
            Singleton<CameraEffects>.Instance.Shake(0.5f, 1f);
            yield return new WaitForSeconds(1.5f);
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBrokenEggBig", TextDisplayer.MessageAdvanceMode.Input);
        }
        public IEnumerator DefeatSmallBeakSequence()
        {
            MasterAnimator.SetBool("DefeatBeak", true);
            Singleton<CameraEffects>.Instance.Shake(0.5f, 1f);
            yield return new WaitForSeconds(1.5f);
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBrokenEggSmall", TextDisplayer.MessageAdvanceMode.Input);
        }
        public IEnumerator DefeatLongArmsSequence()
        {
            MasterAnimator.SetBool("DefeatArms", true);
            Singleton<CameraEffects>.Instance.Shake(0.5f, 1f);
            yield return new WaitForSeconds(1.5f);
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBrokenEggLong", TextDisplayer.MessageAdvanceMode.Input);
            foreach (PlayableCard card in BoardManager.Instance.CardsOnBoard.Concat(PlayerHand.Instance.CardsInHand))
                yield return card.RemoveStatusEffect<Sin>();
        }

        private IEnumerator StartGiantPhase()
        {
            CardInfo beast = CardLoader.GetCardByName("wstl_!GIANTCARD_ApocalypseBird");
            beast.Mods.Add(new(BattleSequencer.ReactiveDifficulty > 13 ? 1 : 0, BattleSequencer.BossHealthThreshold(2) - beast.baseHealth)
            { singletonId = "ReactiveStrength", nonCopyable = true });

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
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossFinal");
            yield return BattleSequencer.GiantPhaseLogic(true);
            yield return new WaitForSeconds(0.1f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossFinalTargets");
        }

        public override IEnumerator IntroSequence(EncounterData encounter)
        {
            bossTotemAbilities = new() {
                    Ability.GuardDog,
                    Ability.Sentry,
                    Ability.Strafe,
                    NimbleFoot.ability,
                    Scorching.ability,
                    ThickSkin.ability
                };

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

            bossObjectAnimation = Instantiate(CustomOpponentUtils.apocalypsePrefab, new Vector3(0.3f, 5.5f, 4.5f), Quaternion.identity);
            bossObjectAnimation.name = "ApocalypseBoss";

            MasterAnimator = bossObjectAnimation.GetComponent<Animator>();
            Transform eyes = bossObjectAnimation.transform.Find("Wing1").Find("Eyes");
            LeftEyes.Add(eyes.Find("Eye1"));
            LeftEyes.Add(eyes.Find("Eye2"));
            LeftEyes.Add(eyes.Find("Eye3"));
            LeftEyes.Add(eyes.Find("Eye4"));
            eyes = bossObjectAnimation.transform.Find("Wing1").Find("OuterWing").Find("Eyes");
            LeftEyes.Add(eyes.Find("Eye1"));
            LeftEyes.Add(eyes.Find("Eye2"));
            LeftEyes.Add(eyes.Find("Eye3"));
            LeftEyes.Add(eyes.Find("Eye4"));
            eyes = bossObjectAnimation.transform.Find("Wing2").Find("Eyes");
            RightEyes.Add(eyes.Find("Eye1"));
            RightEyes.Add(eyes.Find("Eye2"));
            RightEyes.Add(eyes.Find("Eye3"));
            RightEyes.Add(eyes.Find("Eye4"));
            eyes = bossObjectAnimation.transform.Find("Wing2").Find("OuterWing").Find("Eyes");
            RightEyes.Add(eyes.Find("Eye1"));
            RightEyes.Add(eyes.Find("Eye2"));
            RightEyes.Add(eyes.Find("Eye3"));
            RightEyes.Add(eyes.Find("Eye4"));

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

        public override bool PreventInstantWin(bool timeMachine, CardSlot triggeringSlot)
        {
            if (timeMachine)
                return !BattleSequencer.DisabledEggEffects.Contains(ActiveEggEffect.LongArms);
            
            return true;
        }
        public override IEnumerator OnInstantWinPrevented(bool timeMachine, CardSlot triggeringSlot)
        {
            if (timeMachine)
                yield return DialogueHelper.ShowUntilInput("The Long Bird's arms conceal time.");
        }
        public override IEnumerator OnInstantWinTriggered(bool timeMachine, CardSlot triggeringSlot)
        {
            if (timeMachine)
            {
                if (NumLives > 1)
                {
                    BattleSequencer.turnsToNextPhase = 3;
                    BattleSequencer.justSwitchedEffect = true;
                    BattleSequencer.BossCard.Anim.StrongNegationEffect();
                    BattleSequencer.CleanupTargetIcons();
                    BattleSequencer.specialTargetSlots.Clear();
                    foreach (GameObject obj in BattleSequencer.mouthIcons.Values)
                        BattleSequencer.CleanUpTargetIcon(obj);

                    BattleSequencer.mouthIcons.Clear();
                    BattleSequencer.UpdateCounter();
                    
                    foreach (PlayableCard item in Queue)
                    {
                        GlitchOutAssetEffect.GlitchModel(item.StatsLayer.transform);
                        yield return new WaitForSeconds(0.1f);
                    }
                    Queue.Clear();
                    foreach (CardSlot slot in BoardManager.Instance.OpponentSlotsCopy)
                    {
                        if (slot.Card != null && slot.Card != BattleSequencer.BossCard)
                        {
                            PlayableCard card = slot.Card;
                            slot.Card.UnassignFromSlot();
                            GlitchOutAssetEffect.GlitchModel(card.StatsLayer.transform);
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    foreach (CardSlot slot2 in BoardManager.Instance.PlayerSlotsCopy)
                    {
                        if (slot2.Card != null)
                        {
                            yield return slot2.Card.RemoveStatusEffects(false);
                        }
                    }
                    foreach (PlayableCard card in PlayerHand.Instance.CardsInHand)
                    {
                        yield return card.RemoveStatusEffects(false);
                    }

                    yield return new WaitForSeconds(0.5f);
                    yield return DialogueHelper.ShowUntilInput("Your beasts are rejuvenated and the [c:bR]monster[c:] finds itself alone.");
                }
                else
                {
                    yield return BattleSequencer.GiantPhaseLogic(true);
                }

                yield return new WaitForSeconds(0.5f);
                yield return DialogueHelper.ShowUntilInput("The [c:bR]monster[c:] switches its targets.");
            }
            else if (triggeringSlot.Card.HasAbility(TrueSaviour.ability))
            {
                yield return BattleSequencer.BossCard.TakeDamage(20, null);
            }
        }

        private IEnumerator StartIntroLoop()
        {
            yield return new WaitForSeconds(0.5f);
            AudioController.Instance.SetLoopAndPlay("second_trumpet_intro", looping: false);
            AudioController.Instance.SetLoopVolumeImmediate(BG_VOLUME);
            yield return new WaitUntil(() => AudioController.Instance.loopSources[0].time + 0.05f >= AudioController.Instance.loopSources[0].clip.length);
            AudioController.Instance.SetLoopAndPlay("second_trumpet_intro_loop");
            AudioController.Instance.SetLoopVolumeImmediate(BG_VOLUME);
        }
        private IEnumerator StartMainLoop()
        {
            yield return new WaitUntil(() => AudioController.Instance.loopSources[0].time + 0.05f >= AudioController.Instance.loopSources[0].clip.length);
            AudioController.Instance.SetLoopAndPlay("second_trumpet_main", looping: false);
            AudioController.Instance.SetLoopVolumeImmediate(BG_VOLUME);
            yield return new WaitUntil(() => AudioController.Instance.loopSources[0].time + 0.05f >= AudioController.Instance.loopSources[0].clip.length);
            AudioController.Instance.SetLoopAndPlay("second_trumpet_main_loop");
            AudioController.Instance.SetLoopVolumeImmediate(BG_VOLUME);
        }

        internal IEnumerator ResetToIdle()
        {
            if (MasterAnimator.GetBool("Flare") || MasterAnimator.GetBool("Mouth"))
                yield return new WaitForSeconds(0.5f);

            MasterAnimator.SetBool("Flare", false);
            MasterAnimator.SetBool("Mouth", false);
        }

        private void PlayDefeatAnimation()
        {
            MasterAnimator.Play("finalPhaseStart");
            MasterAnimator.Play("idle2", 1);
            MasterAnimator.SetLayerWeight(2, 0f);
            MasterAnimator.SetLayerWeight(3, 0f);
            MasterAnimator.SetLayerWeight(4, 0f);
        }
        public override void SetSceneEffectsShown(bool showEffects)
        {
            if (showEffects)
            {
                ApocalypseBossUtils.ChangeTableColours();
                (Singleton<ExplorableAreaManager>.Instance as CabinManager).SetWestWallHidden(hidden: true);
            }
            else
            {
                Singleton<TableVisualEffectsManager>.Instance.ResetTableColors();
                (Singleton<ExplorableAreaManager>.Instance as CabinManager).SetWestWallHidden(hidden: false);
            }
        }
    }
}