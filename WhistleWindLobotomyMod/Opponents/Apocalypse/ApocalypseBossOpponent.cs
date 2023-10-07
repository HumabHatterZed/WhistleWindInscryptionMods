using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Sound;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

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
        public override Color InteractablesGlowColor => GameColors.Instance.glowRed;
        public ApocalypseBattleSequencer BattleSequence => TurnManager.Instance.SpecialSequencer as ApocalypseBattleSequencer;

        private GameObject apocalypseAnimation;

        private Animator HeadAnimator;
        private Animator NeckAnimator;
        private Animator MouthAnimator;
        private Animator Arm1Animator;
        private Animator Arm2Animator;

        private readonly Animator[] ApocalypseWings = new Animator[4];
        private readonly Animator[] ApocalypseEyes = new Animator[4];

        private const string START_IDLE = "StartIdle";
        private const string START_FLARE = "StartFlare";

        // TO DO
        // figure out animations

        // if totem, change sigil each phase
        private bool bossTotems;
        private readonly List<Ability> possibleAbilities = new()
        {
            Ability.Flying,
            Ability.GainAttackOnKill,
            Ability.Sentry,
            Ability.Sharp,
            Ability.Strafe,
            NimbleFoot.ability,
            Persistent.ability,
            Scorching.ability,
            ThickSkin.ability
        };

        public override IEnumerator LifeLostSequence()
        {
            Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
            yield return new WaitForSeconds(0.25f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default, immediate: false, lockAfter: true);
            AudioController.Instance.SetLoopVolume(0f, 0.5f);
            yield return new WaitForSeconds(1f);

            switch (BattleSequence.CurrentBossPhase)
            {
                case ApocalpyseBossPhase.BigEyes:
                    yield return DefeatBigEyesSequence();
                    break;
                case ApocalpyseBossPhase.SmallBeak:
                    yield return DefeatSmallBeakSequence();
                    break;
                case ApocalpyseBossPhase.LongArms:
                    yield return DefeatLongArmsSequence();
                    break;
            }

            yield return new WaitForSeconds(0.5f);

            if (base.NumLives == 0)
            {
                AscensionStatsData.TryIncrementStat(AscensionStat.Type.BossesDefeated);
                yield return new WaitForSeconds(0.25f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Default, immediate: false, lockAfter: true);
                yield return new WaitForSeconds(0.5f);
                // defeat animation
                yield return new WaitForSeconds(1.5f);
                Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
                yield return new WaitForSeconds(0.5f);
                Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetColor(GameColors.Instance.nearBlack);
                Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetIntensity(1f, float.MaxValue);
                AudioController.Instance.StopAllLoops();
                Singleton<InteractionCursor>.Instance.SetHidden(hidden: true);
                yield return new WaitForSeconds(3f);
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
            else if (base.NumLives > 1)
            {
                AudioController.Instance.SetLoopVolume(0.5f, 0.5f);
            }
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
        }
        public override IEnumerator StartNewPhaseSequence()
        {
            TurnPlan.Clear();

            Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue);
            BattleSequence.CompletedBossPhases.Add(BattleSequence.CurrentBossPhase);

            bool finalPhase = NumLives == 1;

            // switch to the next phase if it's not the last phase; otherwise remove Airborne as a possible totem ability (assuming it's still a choice)
            if (!finalPhase)
                yield return BattleSequence.SwitchToNextEggPhase(true);
            else
                possibleAbilities.Remove(Ability.Flying);

            if (bossTotems)
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.OpponentTotem);
                yield return new WaitForSeconds(0.25f);
                yield return ReplaceTotemBottom();
                yield return new WaitForSeconds(0.5f);
                Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue);
            }

            if (!finalPhase)
                yield return this.QueueNewCards();
            else
                yield return StartGiantPhase();
        }
        public IEnumerator DefeatBigEyesSequence()
        {
            // scribble out eyes
            Singleton<CameraEffects>.Instance.Shake(0.5f, 0.5f);
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBrokenEggBig", TextDisplayer.MessageAdvanceMode.Input);
        }
        public IEnumerator DefeatSmallBeakSequence()
        {
            // 
            Singleton<CameraEffects>.Instance.Shake(0.5f, 0.5f);
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBrokenEggSmall", TextDisplayer.MessageAdvanceMode.Input);
        }
        public IEnumerator DefeatLongArmsSequence()
        {
            // lower arms
            Singleton<CameraEffects>.Instance.Shake(0.5f, 0.5f);
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBrokenEggLong", TextDisplayer.MessageAdvanceMode.Input);
        }

        private IEnumerator StartGiantPhase()
        {
            CardInfo beast = CardLoader.GetCardByName("wstl_!GIANTCARD_ApocalypseBird");
            beast.baseHealth = BattleSequence.CurrentEggCard.Health;
            yield return ClearBoard();
            yield return ClearQueue();
            // collapse apocalypse bird

            yield return new WaitForSeconds(0.5f);

            // build-up
            AudioController.Instance.SetLoopVolume(0.5f, 0.5f);
            AudioController.Instance.GetLoopSource(0).pitch += 1f;
            yield return new WaitForSeconds(1.5f);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(beast, BoardManager.Instance.OpponentSlotsCopy[0], 0.2f);
            yield return new WaitForSeconds(0.2f);
            AudioController.Instance.PlaySound3D("map_slam", MixerGroup.TableObjectsSFX, Singleton<BoardManager>.Instance.transform.position);
            AudioController.Instance.PlaySound2D("apocalypseRoar", MixerGroup.TableObjectsSFX, skipToTime: 0.25f);
            yield return new WaitForSeconds(1f);
        }

        public override IEnumerator IntroSequence(EncounterData encounter)
        {
            // expand base.IntroSequence so we can modify it further
            RunState.CurrentMapRegion.FadeOutAmbientAudio();
            
            // background music
            AudioController.Instance.SetLoopAndPlay("part1_finale_boss");
            AudioController.Instance.GetLoopSource(0).time = 1.75f;
            AudioController.Instance.SetLoopVolumeImmediate(0f);

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
                yield return base.AssembleTotem(totemData, new Vector3(1f, 0f, -1f), new Vector3(0f, 20f, 0f), this.InteractablesGlowColor, false);
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
            AudioController.Instance.FadeOutLoop(0.5f);
            yield return new WaitForSeconds(1.5f);

            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossPreIntro", TextDisplayer.MessageAdvanceMode.Input);

            // get rid of Leshy
            Tween.LocalPosition(
                LeshyAnimationController.Instance.head,
                LeshyAnimationController.Instance.GetBaseHeadPosition(false) + new Vector3(0f, 0f, 2f),
                0.2f, 0f, Tween.EaseInOut,
                completeCallback: () => LeshyAnimationController.Instance.head.localPosition += new Vector3(0f, 0f, 18f)
                );

            yield return new WaitForSeconds(0.5f);
            AudioController.Instance.SetLoopVolume(0.5f, 0.5f);
            AudioController.Instance.PlaySound2D("apocalypseRoar", MixerGroup.TableObjectsSFX);
            yield return new WaitForSeconds(0.25f);

            yield return base.FaceZoomSequence();
            // create Apocalypse Bird object
            apocalypseAnimation = Instantiate(CustomBossUtils.apocalypsePrefab, new Vector3(0.3f, 5.5f, 4.5f), Quaternion.identity);
            apocalypseAnimation.name = "ApocalypseBoss";

            Transform wing1 = apocalypseAnimation.transform.Find("Wing1");
            Transform wing2 = apocalypseAnimation.transform.Find("Wing2");
            Transform outer1 = wing1.Find("OuterWing");
            Transform outer2 = wing2.Find("OuterWing");

            HeadAnimator = apocalypseAnimation.transform.Find("Head").GetComponent<Animator>();
            NeckAnimator = apocalypseAnimation.transform.Find("Neck").GetComponent<Animator>();

            // wings and eyes are animated as groups
            ApocalypseWings[0] = wing1.GetComponent<Animator>();
            ApocalypseWings[1] = wing2.GetComponent<Animator>();
            ApocalypseWings[2] = outer1.GetComponent<Animator>();
            ApocalypseWings[3] = outer2.GetComponent<Animator>();

            ApocalypseEyes[0] = wing1.Find("Eyes").GetComponent<Animator>();
            ApocalypseEyes[1] = wing2.Find("Eyes").GetComponent<Animator>();
            ApocalypseEyes[2] = outer1.Find("Eyes").GetComponent<Animator>();
            ApocalypseEyes[3] = outer2.Find("Eyes").GetComponent<Animator>();

            MouthAnimator = apocalypseAnimation.transform.Find("Body").transform.Find("Mouth").GetComponent<Animator>();
            //Arm1Animator = apocalypseAnimation.transform.Find("Arm1").GetComponent<Animator>();
            //Arm2Animator = apocalypseAnimation.transform.Find("Arm2").GetComponent<Animator>();

            this.SetSceneEffectsShown(true);
            Singleton<CameraEffects>.Instance.Shake(0.5f, 0.25f);
            yield return new WaitForSeconds(0.1f);
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossIntro", TextDisplayer.MessageAdvanceMode.Input);
            yield return new WaitForSeconds(1f);

            // set scales
            Singleton<ViewManager>.Instance.SwitchToView(View.Scales);
            yield return new WaitForSeconds(0.2f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossBendScales1");
            LobotomyPlugin.PreventOpponentDamage = false;
            int damageThreshold = LifeManager.Instance.DamageUntilPlayerWin > 4 ? 4 : LifeManager.Instance.DamageUntilPlayerWin - 1;
            yield return LifeManager.Instance.ShowDamageSequence(damageThreshold, 1, toPlayer: false);
            LobotomyPlugin.PreventOpponentDamage = true;
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossBendScales2");

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossPrelude");
            yield return SetToIdle();
            
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
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

        private IEnumerator SetToIdle()
        {
            HeadAnimator.SetTrigger(START_IDLE);
            NeckAnimator.SetBool("Flare", false);
            CustomCoroutine.WaitThenExecute(0.4f, delegate
            {
                SetWingsToTrigger(START_IDLE);
                SetEyesToTrigger(START_IDLE);
            });
            yield return new WaitUntil(() => HeadAnimator.GetCurrentAnimatorStateInfo(0).IsName("headIdle"));
        }
        private void FlareWings(float wingDelay)
        {
            HeadAnimator.SetTrigger(START_FLARE);
            NeckAnimator.SetBool("Flare", true);
            CustomCoroutine.WaitThenExecute(wingDelay, delegate
            {
                SetWingsToTrigger(START_FLARE);
                SetEyesToTrigger(START_FLARE);
            });
        }
        private void SetWingsToTrigger(string trigger)
        {
            foreach (var anim in ApocalypseWings)
                anim.SetTrigger(trigger);
        }
        private void SetEyesToTrigger(string trigger)
        {
            foreach (var anim in ApocalypseEyes)
                anim.SetTrigger(trigger);
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