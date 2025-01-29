using DiskCardGame;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
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
            apocalypseRegion.bosses = new() { LobOpponentUtils.ApocalypseBossID };
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
                        bossType = LobOpponentUtils.ApocalypseBossID,
                        specialBattleId = ApocalypseBattleSequencer.ID,
                        difficulty = 20,
                        position = new(0.5f, 0.86f)
                    }
                }
            };

            DialogueManager.GenerateRegionIntroductionEvent(LobotomyPlugin.pluginGuid, apocalypseRegion, new()
            {
                "Your journey has brought here, to a dark, oppressive forest.",
                "The sky above you is an inky void, empty of even a single star.",
                "A horrible roar rattles your bones, and a fearful chill overcomes you.",
                "Somewhere beyond the dark trees, there's a monster.",
                "[c:bR]A monster in the Black Forest.[c:]"
            }, new() { new() {
                "Your journey has brought here, to an unknown yet familiar forest.",
                "Somewhere beyond the dark trees, you know, there's a monster.",
                "[c:bR]A monster in the Black Forest.[c:]"
            }});

            return apocalypseRegion;
        }
    }
}
