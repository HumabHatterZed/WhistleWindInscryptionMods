using DiskCardGame;
using InscryptionAPI.Encounters;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Opponents.Apocalypse
{
    public class ApocalypseBossOpponent : Part1BossOpponent
    {
        public static readonly Opponent.Type ID = OpponentManager.Add(
            LobotomyPlugin.pluginGuid, "ApocalypseBossOpponent",
            ApocalypseBattleSequencer.ID, typeof(ApocalypseBossOpponent), new()
            {
                TextureLoader.LoadTextureFromFile("nodeApocalypseBoss1"),
                TextureLoader.LoadTextureFromFile("nodeApocalypseBoss2"),
                TextureLoader.LoadTextureFromFile("nodeApocalypseBoss3"),
                TextureLoader.LoadTextureFromFile("nodeApocalypseBoss4")
            }).Id;

        public override int StartingLives => 4;
        public override bool GiveCurrencyOnDefeat => false;
        public override string DefeatedPlayerDialogue => "Twilight falls...";
        public override Color InteractablesGlowColor => GameColors.Instance.gold;
        public ApocalypseBattleSequencer BattleSequence => TurnManager.Instance.SpecialSequencer as ApocalypseBattleSequencer;

        internal GameObject apocalypseAnimation;
        internal Animator MasterAnimator;
        public readonly List<Transform> LeftEyes = new();
        public readonly List<Transform> RightEyes = new();

        private const float BG_VOLUME = 0.3f;

        private bool bossTotems; // if totem, change sigil each phase
        private readonly List<Ability> possibleAbilities = new() // totem abilities
        {
            Ability.GuardDog,
            Ability.Sentry,
            Ability.Strafe,
            Cycler.ability,
            NimbleFoot.ability,
            Scorching.ability,
            ThickSkin.ability
        };

        public override IEnumerator OutroSequence(bool wasDefeated)
        {
            if (AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems))
            {
                yield return base.DisassembleTotem();
                Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            }
            if (!wasDefeated)
            {
                apocalypseAnimation.transform.SetParent(null);
                yield return new WaitForSeconds(0.1f);
            }
        }
        public override IEnumerator DefeatedPlayerSequence()
        {
            BattleSequence.CleanupTargetIcons();
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
            switch (BattleSequence.ActiveEggEffect)
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
                LobotomySaveManager.DefeatedApocalypseBoss = true;
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
            LobotomyPlugin.PreventOpponentDamage = false;
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
                yield return BattleSequence.SwitchToNextEggEffect(true);

            if (bossTotems)
            {
                if (finalPhase)
                {
                    possibleAbilities.Clear();
                    possibleAbilities.Add(Piercing.ability);
                    possibleAbilities.Add(Persistent.ability);
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
                yield return BattleSequence.MoveOpponentCards();
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
        }

        private IEnumerator StartGiantPhase()
        {
            CardInfo beast = CardLoader.GetCardByName("wstl_!GIANTCARD_ApocalypseBird");
            beast.baseHealth = BattleSequence.BossHealthThreshold(2);
            yield return ClearBoard();
            yield return ClearQueue();
            yield return HelperMethods.ChangeCurrentView(View.Default, 0.5f);
            PlayDefeatAnimation();
            AudioController.Instance.SetLoopVolume(BG_VOLUME, 0.5f);
            AudioController.Instance.loopSources[0].pitch *= 1.1f;
            yield return new WaitForSeconds(2f);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(beast, BoardManager.Instance.OpponentSlotsCopy[0], 0.2f);
            apocalypseAnimation.transform.SetParent(BoardManager.Instance.OpponentSlotsCopy[0].Card.transform.Find("Quad"));
            apocalypseAnimation.transform.localScale = new(0.5f, 0.5f, 0.5f);
            apocalypseAnimation.transform.localPosition = new(-1.6f, -0.4f, -0.5f);
            yield return new WaitForSeconds(0.05f);
            MasterAnimator.Play("finalPhase");

            AudioController.Instance.PlaySound3D("map_slam", MixerGroup.TableObjectsSFX, Singleton<BoardManager>.Instance.transform.position);
            AudioController.Instance.PlaySound2D("bird_roar", MixerGroup.TableObjectsSFX);
            Singleton<CameraEffects>.Instance.Shake(0.5f, 0.5f);
            yield return new WaitForSeconds(2f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossFinal");
            yield return BattleSequence.GiantPhaseLogic(true);
            yield return new WaitForSeconds(0.1f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossFinalTargets");
        }

        public override IEnumerator IntroSequence(EncounterData encounter)
        {
            // expand base.IntroSequence so we can modify it further
            RunState.CurrentMapRegion.FadeOutAmbientAudio();
            yield return ReducePlayerLivesSequence();
            yield return new WaitForSeconds(0.4f);
            // don't instantiate the boss skull; we won't be using it

            if (AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems))
            {
                bossTotems = true;
                yield return new WaitForSeconds(0.25f);
                ChallengeActivationUI.TryShowActivation(AscensionChallenge.BossTotems);

                TotemItemData totemData = ScriptableObject.CreateInstance<TotemItemData>();
                totemData.top = ScriptableObject.CreateInstance<TotemTopData>();
                totemData.top.prerequisites = new() { tribe = Tribe.Bird };
                totemData.bottom = CreateTotemBottomData();
                yield return base.AssembleTotem(totemData, new Vector3(0.5f, 0f, -0.5f), new Vector3(0f, 10f, 0f), this.InteractablesGlowColor, false);
                yield return new WaitForSeconds(0.5f);
                if (!DialogueEventsData.EventIsPlayed("ChallengeBossTotems"))
                {
                    yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("ChallengeBossTotems", TextDisplayer.MessageAdvanceMode.Input);
                }
                Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            }

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            base.SpawnScenery("ForestTableEffects");
            this.sceneryObject.transform.Find("GodRaysEffect").gameObject.SetActive(false);

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
                completeCallback: () => LeshyAnimationController.Instance.head.localPosition += new Vector3(0f, 0f, 18f)
                );

            yield return new WaitForSeconds(0.75f);
            AudioController.Instance.PlaySound2D("bird_roar", MixerGroup.TableObjectsSFX);

            yield return base.FaceZoomSequence();
            // create Apocalypse Bird object
            apocalypseAnimation = Instantiate(CustomBossUtils.apocalypsePrefab, new Vector3(0.3f, 5.5f, 4.5f), Quaternion.identity);
            apocalypseAnimation.name = "ApocalypseBoss";

            MasterAnimator = apocalypseAnimation.GetComponent<Animator>();
            Transform eyes = apocalypseAnimation.transform.Find("Wing1").Find("Eyes");
            LeftEyes.Add(eyes.Find("Eye1"));
            LeftEyes.Add(eyes.Find("Eye2"));
            LeftEyes.Add(eyes.Find("Eye3"));
            LeftEyes.Add(eyes.Find("Eye4"));
            eyes = apocalypseAnimation.transform.Find("Wing1").Find("OuterWing").Find("Eyes");
            LeftEyes.Add(eyes.Find("Eye1"));
            LeftEyes.Add(eyes.Find("Eye2"));
            LeftEyes.Add(eyes.Find("Eye3"));
            LeftEyes.Add(eyes.Find("Eye4"));
            eyes = apocalypseAnimation.transform.Find("Wing2").Find("Eyes");
            RightEyes.Add(eyes.Find("Eye1"));
            RightEyes.Add(eyes.Find("Eye2"));
            RightEyes.Add(eyes.Find("Eye3"));
            RightEyes.Add(eyes.Find("Eye4"));
            eyes = apocalypseAnimation.transform.Find("Wing2").Find("OuterWing").Find("Eyes");
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

            // account for any challenges that affect the initial scale balance
            int damageThreshold = LifeManager.Instance.DamageUntilPlayerWin > 4 ? 4 : LifeManager.Instance.DamageUntilPlayerWin - 1;
            LobotomyPlugin.PreventOpponentDamage = false;
            yield return LifeManager.Instance.ShowDamageSequence(damageThreshold, 1, toPlayer: false);
            LobotomyPlugin.PreventOpponentDamage = true;
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossBendScales2");

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossPrelude");
            MasterAnimator.SetTrigger("MoveFromEntry");
            yield return new WaitForSeconds(0.75f);
            base.StartCoroutine(StartMainLoop());

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
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
        private IEnumerator ReplaceTotemBottom()
        {
            // "disassemble" the totem
            this.totem.Anim.Play("slow_disassemble", 0, 0f);
            yield return new WaitForSeconds(0.333f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            this.totem.ShowHighlighted(highlighted: false, immediate: true);
            this.totem.SetEffectsActive(particlesActive: false, lightActive: false);
            AudioController.Instance.PlaySound2D("metal_object_up#2", MixerGroup.TableObjectsSFX, 1f, 0.25f);

            yield return new WaitForSeconds(0.25f);

            totem.TotemItemData.bottom = CreateTotemBottomData();
            totem.bottomPieceParent.GetComponentInChildren<CompositeTotemPiece>().SetData(totem.TotemItemData.bottom);

            this.totem.Anim.Play("slow_assemble", 0, 0f);
            yield return new WaitForSeconds(0.166f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(0.166f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(1.418f);
            this.totem.ShowHighlighted(highlighted: true, immediate: true);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.2f);
            this.totem.SetEffectsActive(false, lightActive: true);
            AudioController.Instance.PlaySound2D("metal_object_up#2", MixerGroup.TableObjectsSFX, 1f, 0.25f);
        }
        private TotemBottomData CreateTotemBottomData()
        {
            int index = SeededRandom.Range(0, possibleAbilities.Count, SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<GlobalTriggerHandler>.Instance.NumTriggersThisBattle);
            Ability ability = possibleAbilities[index];
            possibleAbilities.Remove(ability);
            TotemBottomData retval = ScriptableObject.CreateInstance<TotemBottomData>();
            retval.effect = TotemEffect.CardGainAbility;
            retval.effectParams = new() { ability = ability };
            return retval;
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
            // override the overrides
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