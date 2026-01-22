using DiskCardGame;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Encounters;
using InscryptionAPI.Guid;
using InscryptionAPI.Regions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents {
    // Ordeals require you to defeat a certain number of opponent cards; they utilise card repositioning, deck renewal, and bone harvesting
    public static class OrdealUtils {
        public static Opponent.Type OpponentID { get; internal set; }

        public static ViewInfo OrdealViewInfo;
        public static readonly View ViewCounter = GuidManager.GetEnumValue<View>(LobotomyPlugin.pluginGuid, "ViewCounter");
        public static readonly ViewController.ViewTransitionInput[] AcceptableViewTransitions = new ViewController.ViewTransitionInput[4];

        public static Texture2D[] DawnAnim;
        public static Texture2D[] DawnTotemAnim;
        public static Texture2D[] NoonAnim;
        public static Texture2D[] NoonTotemAnim;
        public static Texture2D[] DuskAnim;
        public static Texture2D[] DuskTotemAnim;
        public static Texture2D[] MidnightAnim;
        public static Texture2D[] MidnightTotemAnim;

        public static Texture2D[] GreenMidnightAnim;
        public static Texture2D[] VioletMidnightAnim;
        public static Texture2D[] AmberMidnightAnim;
        public static Texture2D[] IndigoMidnightAnim;

        public static Texture2D[] WhiteOrdealAnim;

        public static Texture2D[] OrdealNodeMats;

        public static string GreenDawn;
        public static string GreenNoon;
        public static string GreenDusk;
        public static string GreenMidnight;

        public static string VioletDawn;
        public static string VioletNoon;
        public static string VioletMidnight;

        public static string CrimsonDawn;
        public static string CrimsonNoon;
        public static string CrimsonDusk;

        public static string AmberDawn;
        public static string AmberDusk;
        public static string AmberMidnight;

        public static string IndigoNoon;
        public static string IndigoMidnight;

        public static string WhiteOrdeal;

        private const string ORDEAL_SUBTITLE = "The {0} of {1}";
        public static bool OpponentIsOrdeal() => TurnManager.Instance.Opponent != null && TurnManager.Instance.Opponent is OrdealOpponent;
        public static OrdealType ChooseRandomOrdealType(params OrdealType[] possibleOrdeals) => possibleOrdeals[UnityEngine.Random.Range(0, possibleOrdeals.Length)];

        public static Color GetOrdealColor(OrdealType type) {
            return type switch {
                OrdealType.Green => GameColors.Instance.darkLimeGreen,
                OrdealType.Violet => new Color(0.7f, 0f, 1f),
                OrdealType.Crimson => GameColors.Instance.glowRed,
                OrdealType.Amber => GameColors.Instance.orange,
                OrdealType.Indigo => GameColors.Instance.blue,
                _ => GameColors.Instance.gray
            };
        }
        public static string GetOrdealTitle(OrdealType type, int tier) {
            return LobotomyDialogue.BannerStrings[type][tier][0];
        }
        public static string GetOrdealSubtitle(OrdealType type, int tier) {
            string arg0 = tier switch {
                0 => "Dawn",
                1 => "Noon",
                2 => "Dusk",
                _ => "Midnight"
            };
            return string.Format(ORDEAL_SUBTITLE, arg0, type.ToString());
        }
        public static string GetOrdealIntroDescription(OrdealType type, int tier) {
            return LobotomyDialogue.BannerStrings[type][tier][1];
        }
        public static string GetOrdealOutroDescription(OrdealType type, int tier) {
            return LobotomyDialogue.BannerStrings[type][tier][2];
        }

        internal static void InitOrdeals(AssetBundle bundle) {
            GreenDawn = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealGreenDawn", typeof(OrdealGreenDawn)).Id;
            GreenNoon = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealGreenNoon", typeof(OrdealGreenNoon)).Id;
            GreenDusk = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealGreenDusk", typeof(OrdealGreenDusk)).Id;
            GreenMidnight = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealGreenMidnight", typeof(OrdealGreenMidnight)).Id;

            VioletDawn = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealVioletDawn", typeof(OrdealVioletDawn)).Id;
            VioletNoon = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealVioletNoon", typeof(OrdealVioletNoon)).Id;
            VioletMidnight = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealVioletMidnight", typeof(OrdealVioletMidnight)).Id;

            CrimsonDawn = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealCrimsonDawn", typeof(OrdealCrimsonDawn)).Id;
            CrimsonNoon = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealCrimsonNoon", typeof(OrdealCrimsonNoon)).Id;
            CrimsonDusk = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealCrimsonDusk", typeof(OrdealCrimsonDusk)).Id;

            AmberDawn = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealAmberDawn", typeof(OrdealAmberDawn)).Id;
            AmberDusk = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealAmberDusk", typeof(OrdealAmberDusk)).Id;
            AmberMidnight = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealAmberMidnight", typeof(OrdealAmberMidnight)).Id;

            IndigoNoon = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealIndigoNoon", typeof(OrdealIndigoNoon)).Id;
            IndigoMidnight = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealIndigoMidnight", typeof(OrdealIndigoMidnight)).Id;

            WhiteOrdeal = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealWhite", typeof(OrdealWhite)).Id;
            OpponentID = OpponentManager.Add(LobotomyPlugin.pluginGuid, "OrdealOpponent", null, typeof(OrdealOpponent), null).Id;
            OrdealViewInfo = new() {
                camPosition = new Vector3(0f, 7.65f, -5.15f),
                fov = 35f
            };
            AcceptableViewTransitions[0] = new ViewController.ViewTransitionInput(View.OpponentQueue, ViewCounter, Button.LookUp);
            AcceptableViewTransitions[1] = new ViewController.ViewTransitionInput(ViewCounter, View.OpponentQueue, Button.LookDown);
            AcceptableViewTransitions[2] = new ViewController.ViewTransitionInput(ViewCounter, View.Consumables, Button.LookRight);
            AcceptableViewTransitions[3] = new ViewController.ViewTransitionInput(ViewCounter, View.Scales, Button.LookLeft);

            DawnAnim = NodeHelper.GetNodeTextureList("nodeOrdealDawn1", "nodeOrdealDawn2", "nodeOrdealDawn3", "nodeOrdealDawn4").ToArray();
            DawnTotemAnim = NodeHelper.GetNodeTextureList("nodeOrdealDawnTotem1", "nodeOrdealDawnTotem2", "nodeOrdealDawnTotem3", "nodeOrdealDawnTotem4").ToArray();
            NoonAnim = NodeHelper.GetNodeTextureList("nodeOrdealNoon1", "nodeOrdealNoon2", "nodeOrdealNoon3", "nodeOrdealNoon4").ToArray();
            NoonTotemAnim = NodeHelper.GetNodeTextureList("nodeOrdealNoonTotem1", "nodeOrdealNoonTotem2", "nodeOrdealNoonTotem3", "nodeOrdealNoonTotem4").ToArray();
            DuskAnim = NodeHelper.GetNodeTextureList("nodeOrdealDusk1", "nodeOrdealDusk2", "nodeOrdealDusk3", "nodeOrdealDusk4").ToArray();
            DuskTotemAnim = NodeHelper.GetNodeTextureList("nodeOrdealDuskTotem1", "nodeOrdealDuskTotem2", "nodeOrdealDuskTotem3", "nodeOrdealDuskTotem4").ToArray();
            MidnightAnim = NodeHelper.GetNodeTextureList("nodeOrdealMidnight1", "nodeOrdealMidnight2", "nodeOrdealMidnight3", "nodeOrdealMidnight4").ToArray();
            MidnightTotemAnim = NodeHelper.GetNodeTextureList("nodeOrdealMidnightTotem1", "nodeOrdealMidnightTotem2", "nodeOrdealMidnightTotem3", "nodeOrdealMidnightTotem4").ToArray();

            GreenMidnightAnim = NodeHelper.GetNodeTextureList("nodeGreenMidnight1", "nodeGreenMidnight2", "nodeGreenMidnight3", "nodeGreenMidnight4").ToArray();
            VioletMidnightAnim = NodeHelper.GetNodeTextureList("nodeVioletMidnight1", "nodeVioletMidnight2", "nodeVioletMidnight3", "nodeVioletMidnight4").ToArray();
            AmberMidnightAnim = NodeHelper.GetNodeTextureList("nodeAmberMidnight1", "nodeAmberMidnight2", "nodeAmberMidnight3", "nodeAmberMidnight4").ToArray();
            IndigoMidnightAnim = NodeHelper.GetNodeTextureList("nodeIndigoMidnight1", "nodeIndigoMidnight2", "nodeIndigoMidnight3", "nodeIndigoMidnight4").ToArray();

            WhiteOrdealAnim = NodeHelper.GetNodeTextureList("nodeOrdealFinal1", "nodeOrdealFinal2", "nodeOrdealFinal3", "nodeOrdealFinal4").ToArray();

            OrdealCounterManager.dawnSprite = bundle.LoadAsset<Sprite>("ordeal_counter_dawn");
            OrdealCounterManager.noonSprite = bundle.LoadAsset<Sprite>("ordeal_counter_noon");
            OrdealCounterManager.duskSprite = bundle.LoadAsset<Sprite>("ordeal_counter_dusk");
            OrdealCounterManager.midnightSprite = bundle.LoadAsset<Sprite>("ordeal_counter_midnight");

            OrdealNodeMats = [
                TextureLoader.LoadTextureFromFile("scratched_green.png", LobotomyPlugin.ModAssembly),
                TextureLoader.LoadTextureFromFile("scratched_red.png", LobotomyPlugin.ModAssembly),
                TextureLoader.LoadTextureFromFile("scratched_purple.png", LobotomyPlugin.ModAssembly),
                TextureLoader.LoadTextureFromFile("scratched_orange.png", LobotomyPlugin.ModAssembly),
                TextureLoader.LoadTextureFromFile("scratched_blue.png", LobotomyPlugin.ModAssembly),
                TextureLoader.LoadTextureFromFile("scratched_white.png", LobotomyPlugin.ModAssembly)
            ];

            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Green_start"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Green_end"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Crimson_start"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Crimson_end"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Violet_start"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Violet_end"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Amber_start"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Amber_end"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Indigo_start"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Indigo_end"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("White_start"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("White_end"));

            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Violet_portal_on"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Violet_portal_off"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Violet_eye_start"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("Violet_eye_move"));
        }

        internal static RegionData CreateWhiteOrdealRegion() {
            RegionData trapper = RegionProgression.Instance.regions[2];
            RegionData angler = RegionProgression.Instance.regions[1];
            RegionData leshy = RegionProgression.Instance.ascensionFinalRegion;

            RegionData whiteOrdealRegion = RegionManager.New("wstl_day_46", 3, false)
                .AddBosses(OpponentID)
                .AddDominantTribes(AbnormalPlugin.TribeAnthropoid)
                .SetBoardColor(new(0.5f, 0.4f, 0.3f, 1f))
                .SetCardsColor(new(0.8f, 0.7f, 0.6f, 1f))
                .SetFogEnabled(true).SetFogAlpha(0.8f)
                .SetMapAlbedo(TextureLoader.LoadTextureFromFile("mapScroll_Albedo_City.png", LobotomyPlugin.ModAssembly));

            whiteOrdealRegion.fogProfile = ScriptableObject.CreateInstance<VolumetricFogAndMist.VolumetricFogProfile>();
            whiteOrdealRegion.fogProfile.color = new(0.7f, 0.6f, 0.8f, 1f);
            whiteOrdealRegion.fogProfile.lightColor = new(0.6f, 0.7f, 0.8f, 1f);
            whiteOrdealRegion.fogProfile.specularColor = new(0.7f, 0.6f, 0.8f, 1f);

            if (AssetManager.CustomSceneryData.TryGetValue("twisted_building", out List<SceneryData> data)) {
                whiteOrdealRegion.fillerScenery = data.Select(x => new FillerSceneryEntry() { data = x }).ToList();
            }
            else {
                LobotomyPlugin.Log.LogWarning("Could not get twisted_building scenery data");
            }


            whiteOrdealRegion.predefinedNodes = ScriptableObject.CreateInstance<PredefinedNodes>();
            whiteOrdealRegion.predefinedNodes.nodeRows = new(leshy.predefinedNodes.nodeRows);
            whiteOrdealRegion.predefinedNodes.nodeRows[2] = new() {
                new OrdealBossBattleNodeData {
                    bossType = OpponentID,
                    specialBattleId = WhiteOrdeal,
                    ordealType = OrdealType.White,
                    tier = 3,
                    difficulty = 20,
                    position = new(0.5f, 0.86f)
                }
            };

            DialogueManager.GenerateRegionIntroductionEvent(LobotomyPlugin.pluginGuid, whiteOrdealRegion, new()
            {
                "At long last, you approach the end of your journey.",
                "Buildings loom far into the distance, leering down with hundreds of glowing eyes.",
                "Your destination lies just ahead, but someone will undoubtedly try to stop you.",
                "Preparing for the worst, you march onward into [c:bR]The City[c:]."
            });

            return whiteOrdealRegion;
        }
    }

    public class OrdealBattleNodeData : CardBattleNodeData {
        public bool totemOpponent;
        public int tier;
        public OrdealType ordealType;
    }
    public class OrdealBossBattleNodeData : BossBattleNodeData {
        public bool totemOpponent;
        public int tier;
        public OrdealType ordealType;
    }

    public enum OrdealType {
        Green = 0,  // G G G G
        Crimson = 1,// C C C _
        Violet = 2, // V V _ V
        Amber = 3,  // A _ A A
        Indigo = 4, // _ I _ _
        White = 5   // W W W W
    }
}