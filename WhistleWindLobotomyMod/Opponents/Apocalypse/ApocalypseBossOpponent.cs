using DiskCardGame;
using InscryptionAPI.Encounters;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;
using InscryptionAPI.Card;
using System.Linq;
using WhistleWind.AbnormalSigils;
using HarmonyLib;

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

        public override IEnumerator LifeLostSequence()
        {
            TurnPlan.Clear();
            switch (NumLives)
            {
                case 2:
                    break;
                case 1:
                    break;
            }
            yield break;
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
                int difficulty = Mathf.Min(encounter.Difficulty, 15);

                // only allow a select few abilities to be used by the boss
                List<Ability> redundantAbilities = (from AbilityInfo x in AbilityManager.AllAbilityInfos
                                                    where x.ability != ThickSkin.ability // && other
                                                    select x.ability).ToList();

                TotemItemData totemData = DiskCardGame.EncounterBuilder.BuildOpponentTotem(encounter.Blueprint.dominantTribes[0], difficulty, redundantAbilities);
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
            // pass through the boss game object as an easy-ish check
            yield return LifeManager.Instance.ShowDamageSequence(LifeManager.Instance.DamageUntilPlayerWin - 1, 1, toPlayer: false);

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
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBrokenEggBig", TextDisplayer.MessageAdvanceMode.Input);
            yield break;
        }
        public IEnumerator ShutBeakSequence()
        {
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBrokenEggSmall", TextDisplayer.MessageAdvanceMode.Input);
            yield break;
        }
        public IEnumerator BreakArmsSequence()
        {
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBrokenEggLong", TextDisplayer.MessageAdvanceMode.Input);
            yield break;
        }
    }
}