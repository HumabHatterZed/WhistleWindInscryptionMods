using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
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

        public override int StartingLives => 3;
        public override string DefeatedPlayerDialogue => "Twilight falls...";
        public override bool GiveCurrencyOnDefeat => false;
        private ApocalypseBattleSequencer BattleSequence => TurnManager.Instance.SpecialSequencer as ApocalypseBattleSequencer;

        private GameObject apocalypseAnimation;
        public Transform apocalypseHead;
        private Transform apocalypseWing1;
        private Transform apocalypseWing2;
        private Transform apocalypseArm1;
        private Transform apocalypseArm2;
        private Transform apocalypseMouth;

        // TO DO
        // figure out animations

        // switch totem out each phase
        private bool bossTotems;
        private List<Ability> possibleAbilities = new()
        {
            Ability.Sharp,
            ThickSkin.ability
        };

        public override IEnumerator StartNewPhaseSequence()
        {
            TurnPlan.Clear();
            switch (NumLives)
            {
                case 2:
                    yield return SwitchToNextPhase(false);
                    break;
                case 1:
                    yield return SwitchToNextPhase(true);
                    break;
            }
        }

        private IEnumerator SwitchToNextPhase(bool finalPhase)
        {
            if (bossTotems)
            {
                TotemItemData totemData = CreateOpponentTotemData();
                if (finalPhase)
                    totemData.bottom.effectParams.ability = BitterEnemies.ability;

                yield return new WaitForSeconds(0.25f);
                yield return base.DisassembleTotem();
                yield return new WaitForSeconds(0.5f);
                yield return base.AssembleTotem(totemData, new Vector3(1f, 0f, -1f), new Vector3(0f, 20f, 0f), this.InteractablesGlowColor, particlesActive: false);
                yield return new WaitForSeconds(0.5f);
            }

            EncounterBlueprintData data = BattleSequence.EggPhases[SeededRandom.Range(0, BattleSequence.EggPhases.Count, SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<GlobalTriggerHandler>.Instance.NumTriggersThisBattle + 1)];
            BattleSequence.EggPhases.Remove(data);

            yield return this.ReplaceWithCustomBlueprint(data);
        }

        public override IEnumerator LifeLostSequence()
        {
            Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
            yield return new WaitForSeconds(0.5f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default, immediate: false, lockAfter: true);
            // reset parts to idle
            yield return new WaitForSeconds(1f);

            // disable a battle effect
            if (BattleSequence.BrokeBigEyes)
                yield return CloseEyesSequence();

            if (BattleSequence.BrokeSmallBeak)
                yield return ShutBeakSequence();

            if (BattleSequence.BrokeLongArms)
                yield return BreakArmsSequence();

            yield return new WaitForSeconds(0.25f);

            if (base.NumLives == 0)
            {
                AscensionStatsData.TryIncrementStat(AscensionStat.Type.BossesDefeated);
                yield return new WaitForSeconds(0.25f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Default, immediate: false, lockAfter: true);
                yield return new WaitForSeconds(0.5f);
                // defeat animation
                yield return new WaitForSeconds(1.2f);
                yield return base.TransitionFromFinalBoss();
                yield break;
            }
        }

        public override IEnumerator IntroSequence(EncounterData encounter)
        {
            // expand base.IntroSequence so we can modify it further
            AudioController.Instance.FadeOutLoop(0.75f);
            RunState.CurrentMapRegion.FadeOutAmbientAudio();
            yield return ReducePlayerLivesSequence();
            yield return new WaitForSeconds(0.4f);
            // don't instantiate the boss skull; we won't be using it
            this.SetSceneEffectsShown(shown: true);
            if (AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems))
            {
                bossTotems = true;
                yield return new WaitForSeconds(0.25f);
                ChallengeActivationUI.TryShowActivation(AscensionChallenge.BossTotems);

                TotemItemData totemData = CreateOpponentTotemData();

                yield return base.AssembleTotem(totemData, new Vector3(1f, 0f, -1f), new Vector3(0f, 20f, 0f), this.InteractablesGlowColor, particlesActive: false);
                if (!DialogueEventsData.EventIsPlayed("ChallengeBossTotems"))
                {
                    yield return new WaitForSeconds(0.5f);
                    yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("ChallengeBossTotems", TextDisplayer.MessageAdvanceMode.Input);
                }
                Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            }

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            AudioController.Instance.SetLoopAndPlay("boss_prospector_base");
            AudioController.Instance.SetLoopAndPlay("boss_prospector_ambient", 1);
            base.SpawnScenery("ForestTableEffects");
            this.sceneryObject.transform.Find("GodRaysEffect").gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            AudioController.Instance.PlaySound2D("prospector_trees_enter", MixerGroup.TableObjectsSFX, 0.2f);
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
            // create Apocalypse Bird object
            yield return base.FaceZoomSequence();
            apocalypseAnimation = Instantiate(CustomBossUtils.apocalypsePrefab, new Vector3(0.3f, 5.5f, 4.5f), Quaternion.identity);
            apocalypseAnimation.name = "ApocalypseBoss";
            apocalypseHead = apocalypseAnimation.transform.Find("Head");
            apocalypseWing1 = apocalypseAnimation.transform.Find("Wing1");
            apocalypseWing2 = apocalypseAnimation.transform.Find("Wing2");
            apocalypseMouth = apocalypseAnimation.transform.Find("Body").transform.Find("Mouth");
            apocalypseHead = apocalypseAnimation.transform.Find("Head");

            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossIntro", TextDisplayer.MessageAdvanceMode.Input);
            yield return new WaitForSeconds(0.5f);

            // set scales
            Singleton<ViewManager>.Instance.SwitchToView(View.Scales);
            yield return new WaitForSeconds(0.2f);
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBendScales1", TextDisplayer.MessageAdvanceMode.Input);
            yield return LifeManager.Instance.ShowDamageSequence(LifeManager.Instance.DamageUntilPlayerWin - 1, 1, toPlayer: false);
            LobotomyPlugin.PreventOpponentDamage = true;
            yield return new WaitForSeconds(0.5f);
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBendScales2", TextDisplayer.MessageAdvanceMode.Input);

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            // fancy body animations
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossPrelude", TextDisplayer.MessageAdvanceMode.Input);
            var i = apocalypseAnimation.GetComponentsInChildren<Animator>();
            apocalypseHead.GetComponent<Animator>().SetTrigger("StartIdle");

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }

        public IEnumerator CloseEyesSequence()
        {
            // scribble out eyes
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBrokenEggBig", TextDisplayer.MessageAdvanceMode.Input);
            yield break;
        }
        public IEnumerator ShutBeakSequence()
        {
            // 
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBrokenEggSmall", TextDisplayer.MessageAdvanceMode.Input);
            yield break;
        }
        public IEnumerator BreakArmsSequence()
        {
            // lower arms
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBrokenEggLong", TextDisplayer.MessageAdvanceMode.Input);
            yield break;
        }

        private TotemItemData CreateOpponentTotemData()
        {
            int index = SeededRandom.Range(0, possibleAbilities.Count, SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<GlobalTriggerHandler>.Instance.NumTriggersThisBattle + 1);
            Ability ability = possibleAbilities[index];
            possibleAbilities.Remove(ability);
            return new()
            {
                top = new(Tribe.Bird),
                bottom = new(ability)
            };
        }
    }
}