using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Tango;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using EncounterBuilder = DiskCardGame.EncounterBuilder;

namespace WhistleWindLobotomyMod.Opponents
{
    public abstract class LobotomyBattleSequencer : BossBattleSequencer
    {
        public virtual bool PreventScaleDamage { get; set; } = true;
        public virtual bool DirectDamageGivesBones { get; set; } = true;

        public bool drewInitialHand = false;

        public virtual IEnumerator PreDrawOpeningHand()
        {
            yield break;
        }
        public virtual IEnumerator PostDrawOpeningHand()
        {
            yield break;
        }
    }

    public abstract class OrdealBattleSequencer : LobotomyBattleSequencer
    {
        public OrdealColour ordealColour;
        public override Opponent.Type BossType => OrdealUtils.OpponentID;
        public override StoryEvent DefeatedStoryEvent => LobotomyPlugin.OrdealDefeated;
        public abstract EncounterData ConstructOrdealBlueprint(EncounterData encounterData);

        public override IEnumerator OpponentUpkeep()
        {
            yield return base.OpponentUpkeep();

            // extend the turn plan infinitely
            if (TurnManager.Instance.Opponent.NumTurnsTaken >= TurnManager.Instance.Opponent.TurnPlan.Count - 1)
            {
                // remove cards that should only appear once during the entire battle
                TurnManager.Instance.Opponent.Blueprint.turns.RemoveAll(t => t.Exists(c => c.card.HasTrait(Trait.DeathcardCreationNonOption)));
                List<List<CardInfo>> turnPlan = EncounterBuilder.BuildOpponentTurnPlan(TurnManager.Instance.Opponent.Blueprint, TurnManager.Instance.Opponent.Difficulty);
                TurnManager.Instance.Opponent.ReplaceAndAppendTurnPlan(turnPlan);
            }
        }

        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
        {
            if (nodeData == null)
            {
                Debug.Log("NodeData is null");
                return null;
            }

            ordealColour = (nodeData as OrdealBattleNodeData).ordealColour;
            EncounterData encounterData = new()
            {
                opponentType = OrdealUtils.OpponentID,
                Blueprint = EncounterManager.New("", false).SetDifficulty(0, nodeData.difficulty)
            };

            switch (ordealColour)
            {
                case OrdealColour.Green:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeMechanical).SetRedundantAbilities(Piercing.ability);
                    break;
                case OrdealColour.Crimson:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeFae).SetRedundantAbilities(Ability.ExplodeOnDeath);
                    break;
                case OrdealColour.Violet:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeDivine).SetRedundantAbilities(Scorching.ability);
                    break;
                case OrdealColour.Amber:
                    encounterData.Blueprint.AddDominantTribes(Tribe.Insect).SetRedundantAbilities(Ability.WhackAMole);
                    break;
                default:
                    encounterData.Blueprint.AddDominantTribes(AbnormalPlugin.TribeAnthropoid).SetRedundantAbilities(Persistent.ability);
                    break;
            }

            ConstructOrdealBlueprint(encounterData);
            if ((nodeData as OrdealBattleNodeData).totemOpponent)
                encounterData.opponentTotem = EncounterBuilder.BuildOpponentTotem(encounterData.Blueprint.dominantTribes[0], nodeData.difficulty, encounterData.Blueprint.redundantAbilities);

            encounterData.opponentTurnPlan = EncounterBuilder.BuildOpponentTurnPlan(encounterData.Blueprint, nodeData.difficulty + RunState.Run.DifficultyModifier);
            //encounterData.opponentTurnPlan.Randomize();
            return encounterData;
        }
    }

    // composite opponent that doubles as a Totem opponent
    public class OrdealOpponent : Part1Opponent
    {
        public bool totemOpponent;
        private Color totemGlowColour;
        public static OrdealBattleSequencer BattleSequencer => TurnManager.Instance.SpecialSequencer as OrdealBattleSequencer;
        public override IEnumerator IntroSequence(EncounterData encounter)
        {
            totemOpponent = encounter.opponentTotem != null;
            totemGlowColour = BattleSequencer.ordealColour switch
            {
                OrdealColour.Green => GameColors.Instance.limeGreen,
                OrdealColour.Violet => GameColors.Instance.purple,
                OrdealColour.Crimson => GameColors.Instance.glowRed,
                OrdealColour.Amber => GameColors.Instance.orange,
                OrdealColour.Indigo => GameColors.Instance.blue,
                _ => GameColors.Instance.lightGray
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
            if (showEffects)
            {
                Color cardLightColour;
                Color mainHighlightColour;
                Color mainDefaultColour;
                Color queueHighlightColour;
                Color queueDefaultColour;

                switch (BattleSequencer.ordealColour)
                {
                    case OrdealColour.Green:
                        cardLightColour = GameColors.Instance.seafoam;
                        mainHighlightColour = mainDefaultColour = GameColors.Instance.darkLimeGreen;
                        queueHighlightColour = queueDefaultColour = GameColors.Instance.darkSeafoam;
                        break;
                    case OrdealColour.Violet:
                        cardLightColour = GameColors.Instance.fuschia;
                        mainHighlightColour = mainDefaultColour = GameColors.Instance.darkPurple;
                        queueHighlightColour = queueDefaultColour = GameColors.Instance.darkFuschia;
                        break;
                    case OrdealColour.Crimson:
                        cardLightColour = GameColors.Instance.orange;
                        mainHighlightColour = mainDefaultColour = GameColors.Instance.darkRed;
                        queueHighlightColour = queueDefaultColour = GameColors.Instance.brownOrange;
                        break;
                    case OrdealColour.Amber:
                        cardLightColour = GameColors.Instance.marigold;
                        mainHighlightColour = mainDefaultColour = GameColors.Instance.brownOrange;
                        queueHighlightColour = queueDefaultColour = GameColors.Instance.darkGold;
                        break;
                    case OrdealColour.Indigo:
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
            else
            {
                Singleton<TableVisualEffectsManager>.Instance.ResetTableColors();
            }
        }
    }
}
