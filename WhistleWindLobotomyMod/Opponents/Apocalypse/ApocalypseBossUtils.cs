using DiskCardGame;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Opponents {
    public static class ApocalypseBossUtils {
        public static void ChangeTableColours() {
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

        internal static EncounterBlueprintData CreateStartingBlueprint() {
            string minion = (TurnManager.Instance.SpecialSequencer as ApocalypseBattleSequencer).ActiveEggMinion;

            EncounterBlueprintData encounter = EncounterManager.New("ApocalypseBossPlan", false)
                .AddDominantTribes(Tribe.Bird)
                .AddTurns(EncounterManager.CreateTurn(minion, minion));

            return encounter;
        }
        internal static RegionData CreateRegion() {
            RegionData trapper = RegionProgression.Instance.regions[2];
            RegionData leshy = RegionProgression.Instance.ascensionFinalRegion;
            RegionData angler = RegionProgression.Instance.regions[1];

            RegionData apocalypseRegion = RegionManager.New("wstl_the_black_forest", 3, false)
                .AddBosses(LobOpponentUtils.ApocalypseBossID)
                .AddDominantTribes(Tribe.Bird)
                .SetBoardColor(new(0.05f, 0.2f, 0.05f, 1f))
                .SetCardsColor(new(0.2f, 0.33f, 0f, 1f))
                .SetFogEnabled(true).SetFogAlpha(0.8f)
                .AddFillerScenery(new FillerSceneryEntry() { data = trapper.scarceScenery[1].data })
                .SetMapAlbedo(TextureLoader.LoadTextureFromFile("mapScroll_Albedo_TrueDarkness.png", LobotomyPlugin.ModAssembly))
                .SetDustParticlesEnabled(true).SetMapParticlesPrefabs(angler.mapParticlesPrefabs.ToArray());

            apocalypseRegion.fogProfile = ScriptableObject.CreateInstance<VolumetricFogAndMist.VolumetricFogProfile>();
            apocalypseRegion.fogProfile.color = new(0.5f, 0.5f, 0.5f, 1f);
            apocalypseRegion.fogProfile.lightColor = new(0.5f, 0.5f, 0.5f, 1f);
            apocalypseRegion.fogProfile.specularColor = new(0.5f, 0.5f, 0.5f, 1f);
            apocalypseRegion.predefinedNodes = ScriptableObject.CreateInstance<PredefinedNodes>();
            apocalypseRegion.predefinedNodes.nodeRows = new(leshy.predefinedNodes.nodeRows);
            apocalypseRegion.predefinedNodes.nodeRows[2] = new() {
                new BossBattleNodeData {
                    bossType = LobOpponentUtils.ApocalypseBossID,
                    specialBattleId = ApocalypseBattleSequencer.ID,
                    difficulty = 20,
                    position = new(0.5f, 0.86f)
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
