using DiskCardGame;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Opponents
{
    public static class ApocalypseBossUtils
    {
        public static void ChangeTableColours()
        {
            Color slotColour = GameColors.Instance.darkRed;
            slotColour.a = 0.5f;

            Singleton<TableVisualEffectsManager>.Instance.ChangeTableColors(
                GameColors.Instance.darkRed,
                GameColors.Instance.brown,
                GameColors.Instance.glowRed,
                slotColour,
                GameColors.Instance.darkRed,
                GameColors.Instance.glowRed,
                GameColors.Instance.gray,
                GameColors.Instance.gray,
                GameColors.Instance.lightGray);
        }

        internal static void KillPlayerSequence()
        {
            ApocalypseBossOpponent opponent = TurnManager.Instance.Opponent as ApocalypseBossOpponent;
            opponent.MasterAnimator.SetTrigger("KillPlayer");
            opponent.MasterAnimator.SetLayerWeight(1, 0f);
            opponent.MasterAnimator.SetLayerWeight(2, 0f);
            opponent.MasterAnimator.SetLayerWeight(3, 0f);
            opponent.MasterAnimator.SetLayerWeight(4, 0f);
        }
        internal static IEnumerator ExhaustedSequence(CardDrawPiles instance, CardSlot giantSlot)
        {
            if (instance.turnsSinceExhausted == 0)
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossCardsExhausted", TextDisplayer.MessageAdvanceMode.Input);

            Singleton<ViewManager>.Instance.SwitchToView(View.Board, immediate: false, lockAfter: true);
            yield return new WaitForSeconds(0.25f);
            giantSlot.Card.AddTemporaryMod(new CardModificationInfo(1, 0));
            giantSlot.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(1f);
        }
        internal static EncounterBlueprintData CreateStartingBlueprint()
        {
            string minion = (TurnManager.Instance.SpecialSequencer as ApocalypseBattleSequencer).ActiveEggMinion;

            EncounterBlueprintData encounter = EncounterManager.New("ApocalypseBossPlan", false)
                .AddDominantTribes(Tribe.Bird)
                .AddTurns(EncounterManager.CreateTurn(minion, minion));

            return encounter;
        }
        internal static RegionData CreateRegion()
        {
            RegionData trapper = RegionProgression.Instance.regions[2];
            RegionData leshy = RegionProgression.Instance.ascensionFinalRegion;

            RegionData apocalypseRegion = ScriptableObject.CreateInstance<RegionData>();
            apocalypseRegion.name = "wstl_the_black_forest";
            apocalypseRegion.boardLightColor = new(0f, 0.3f, 0f, 1f);
            apocalypseRegion.cardsLightColor = new(0.2f, 0.33f, 0f, 1f);
            apocalypseRegion.dominantTribes = new() { Tribe.Bird };
            apocalypseRegion.bosses = new() { ApocalypseBossOpponent.ID };
            apocalypseRegion.fillerScenery = new() { new FillerSceneryEntry() { data = trapper.scarceScenery[1].data } };
            apocalypseRegion.fogAlpha = 0.75f;
            apocalypseRegion.fogEnabled = true;
            apocalypseRegion.fogProfile = ScriptableObject.CreateInstance<VolumetricFogAndMist.VolumetricFogProfile>();
            apocalypseRegion.fogProfile.color = new(0.5f, 0.5f, 0.5f, 1f);
            apocalypseRegion.fogProfile.lightColor = new(0.5f, 0.5f, 0.5f, 1f);
            apocalypseRegion.fogProfile.specularColor = new(0.5f, 0.5f, 0.5f, 1f);
            apocalypseRegion.mapAlbedo = leshy.mapAlbedo;
            apocalypseRegion.mapEmission = leshy.mapEmission;
            apocalypseRegion.mapEmissionColor = leshy.mapEmissionColor;
            apocalypseRegion.predefinedNodes = ScriptableObject.CreateInstance<PredefinedNodes>();
            apocalypseRegion.predefinedNodes.nodeRows = new()
            {
                new() {
                    new NodeData { position = new(0.5f, 0.42f) }
                },
                new()
                {
                    new CardMergeNodeData { position = new(0.315f, 0.65f) },
                    new GainConsumablesNodeData { position = new(0.435f, 0.64f) },
                    new TradePeltsNodeData { position = new(0.565f, 0.66f) },
                    new BuildTotemNodeData { position = new(0.685f, 0.64f) }
                },
                new()
                {
                    new BossBattleNodeData
                    {
                        bossType = ApocalypseBossOpponent.ID,
                        specialBattleId = ApocalypseBattleSequencer.ID,
                        difficulty = 20,
                        position = new(0.5f, 0.86f)
                    }
                }
            };
            return apocalypseRegion;
        }
    }
}
