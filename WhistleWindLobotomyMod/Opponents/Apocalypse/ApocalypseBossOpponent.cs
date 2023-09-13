using DiskCardGame;
using InscryptionAPI.Encounters;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Opponents.TrapperTrader;
using WhistleWindLobotomyMod.Core.SpecialSequencers;

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

        private GameObject apocalypseAnimation;

        // TO DO
        // create assetbundle
        // load asset as prefab
        // instantiate prefab as go
        // figure out animations

        public override IEnumerator IntroSequence(EncounterData encounter)
        {
            AudioController.Instance.FadeOutLoop(0.75f);
            yield return base.IntroSequence(encounter);
            yield return new WaitForSeconds(0.4f);
            base.bossSkull.EnterHand();
            yield return new WaitForSeconds(3.5f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            AudioController.Instance.SetLoopAndPlay("boss_prospector_base");
            AudioController.Instance.SetLoopAndPlay("boss_prospector_ambient", 1);
            base.SpawnScenery("ForestTableEffects");
            yield return new WaitForSeconds(0.5f);
            AudioController.Instance.PlaySound2D("prospector_trees_enter", MixerGroup.TableObjectsSFX, 0.2f);
            yield return new WaitForSeconds(1.5f);

            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossPreIntro", TextDisplayer.MessageAdvanceMode.Input);
            yield return new WaitForSeconds(0.15f);
            // hide leshy and spawn the bird
            Tween.Position(OpponentAnimationController.Instance.transform, OpponentAnimationController.Instance.transform.position + Vector3.forward, 0.3f, 0f,
                completeCallback: () => OpponentAnimationController.Instance.gameObject.SetActive(false));
            
            yield return new WaitForSeconds(1.5f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(0.166f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return base.FaceZoomSequence();
            yield return new WaitForSeconds(1.418f);

            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossIntro", TextDisplayer.MessageAdvanceMode.Input);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
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