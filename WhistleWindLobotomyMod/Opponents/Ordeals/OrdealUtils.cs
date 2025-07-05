using DiskCardGame;
using InscryptionAPI.Encounters;
using InscryptionAPI.Guid;
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

        public static Texture2D[] OrdealNodeMats;

        public static readonly string GreenDawn = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealGreenDawn", typeof(OrdealGreenDawn)).Id;
        public static readonly string GreenNoon = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealGreenNoon", typeof(OrdealGreenNoon)).Id;
        public static readonly string GreenDusk = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealGreenDusk", typeof(OrdealGreenDusk)).Id;
        public static readonly string GreenMidnight = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealGreenMidnight", typeof(OrdealGreenMidnight)).Id;

        public static readonly string VioletDawn = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealVioletDawn", typeof(OrdealVioletDawn)).Id;
        public static readonly string VioletNoon = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealVioletNoon", typeof(OrdealVioletNoon)).Id;
        public static readonly string VioletMidnight = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealVioletMidnight", typeof(OrdealVioletMidnight)).Id;

        public static readonly string CrimsonDawn = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealCrimsonDawn", typeof(OrdealCrimsonDawn)).Id;
        public static readonly string CrimsonNoon = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealCrimsonNoon", typeof(OrdealCrimsonNoon)).Id;
        public static readonly string CrimsonDusk = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealCrimsonDusk", typeof(OrdealCrimsonDusk)).Id;

        public static readonly string AmberDawn = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealAmberDawn", typeof(OrdealAmberDawn)).Id;
        public static readonly string AmberDusk = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealAmberDusk", typeof(OrdealAmberDusk)).Id;
        public static readonly string AmberMidnight = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealAmberMidnight", typeof(OrdealAmberMidnight)).Id;

        public static readonly string IndigoNoon = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealIndigoNoon", typeof(OrdealIndigoNoon)).Id;

        public static readonly string WhiteOrdeal = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealWhite", typeof(OrdealWhite)).Id;

        public static AudioClip[] OrdealSFX;

        public static bool OpponentIsOrdeal() => TurnManager.Instance.Opponent != null && TurnManager.Instance.Opponent is OrdealOpponent;
        public static OrdealType ChooseRandomOrdealType(params OrdealType[] possibleOrdeals) => possibleOrdeals[UnityEngine.Random.Range(0, possibleOrdeals.Length - 1)];

        public static string GetOrdealIntroDescription(OrdealType type, int tier) {
            return LobotomyDialogue.BannerIntroDescriptions[type][tier];
        }
        public static string GetOrdealOutroDescription(OrdealType type, int tier) {
            return LobotomyDialogue.BannerOutroDescriptions[type][tier];
        }

        internal static void InitOrdeals() {
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

            OrdealCounterManager.dawnSprite = AssetManager.AssetBundle.LoadAsset<Sprite>("ordeal_counter_dawn");
            OrdealCounterManager.noonSprite = AssetManager.AssetBundle.LoadAsset<Sprite>("ordeal_counter_noon");
            OrdealCounterManager.duskSprite = AssetManager.AssetBundle.LoadAsset<Sprite>("ordeal_counter_dusk");
            OrdealCounterManager.midnightSprite = AssetManager.AssetBundle.LoadAsset<Sprite>("ordeal_counter_midnight");

            OrdealNodeMats = [
                TextureLoader.LoadTextureFromFile("scratched_green.png", LobotomyPlugin.ModAssembly),
                TextureLoader.LoadTextureFromFile("scratched_red.png", LobotomyPlugin.ModAssembly),
                TextureLoader.LoadTextureFromFile("scratched_purple.png", LobotomyPlugin.ModAssembly),
                TextureLoader.LoadTextureFromFile("scratched_orange.png", LobotomyPlugin.ModAssembly),
                TextureLoader.LoadTextureFromFile("scratched_blue.png", LobotomyPlugin.ModAssembly),
                TextureLoader.LoadTextureFromFile("scratched_white.png", LobotomyPlugin.ModAssembly)
            ];

            OrdealSFX = [
                AssetManager.AssetBundle.LoadAsset<AudioClip>("Green_start"),
                AssetManager.AssetBundle.LoadAsset<AudioClip>("Green_end"),
                AssetManager.AssetBundle.LoadAsset<AudioClip>("Crimson_start"),
                AssetManager.AssetBundle.LoadAsset<AudioClip>("Crimson_end"),
                AssetManager.AssetBundle.LoadAsset<AudioClip>("Violet_start"),
                AssetManager.AssetBundle.LoadAsset<AudioClip>("Violet_end"),
                AssetManager.AssetBundle.LoadAsset<AudioClip>("Amber_start"),
                AssetManager.AssetBundle.LoadAsset<AudioClip>("Amber_end"),
                AssetManager.AssetBundle.LoadAsset<AudioClip>("Indigo_start"),
                AssetManager.AssetBundle.LoadAsset<AudioClip>("Indigo_end"),
                AssetManager.AssetBundle.LoadAsset<AudioClip>("White_start"),
                AssetManager.AssetBundle.LoadAsset<AudioClip>("White_end")
            ];

            AssetManager.sfxClips.AddRange(OrdealSFX);
        }

        internal static RegionData CreateWhiteOrdealRegion() {
            RegionData trapper = RegionProgression.Instance.regions[2];
            RegionData leshy = RegionProgression.Instance.ascensionFinalRegion;

            RegionData whiteOrdealRegion = ScriptableObject.CreateInstance<RegionData>();
            whiteOrdealRegion.name = "wstl_day_46";
            whiteOrdealRegion.boardLightColor = new(6f, 0.6f, 0.6f, 1f);
            whiteOrdealRegion.cardsLightColor = new(0.4f, 0.4f, 0.4f, 1f);
            whiteOrdealRegion.dominantTribes = new() { AbnormalPlugin.TribeAnthropoid };
            whiteOrdealRegion.bosses = new() { OpponentID };
            whiteOrdealRegion.fogAlpha = 0.75f;
            whiteOrdealRegion.fogEnabled = true;
            whiteOrdealRegion.fogProfile = ScriptableObject.CreateInstance<VolumetricFogAndMist.VolumetricFogProfile>();
            whiteOrdealRegion.fogProfile.color = new(0.7f, 0.7f, 0.7f, 1f);
            whiteOrdealRegion.fogProfile.lightColor = new(0.7f, 0.7f, 0.7f, 1f);
            whiteOrdealRegion.fogProfile.specularColor = new(0.7f, 0.7f, 0.7f, 1f);
            whiteOrdealRegion.mapAlbedo = leshy.mapAlbedo;
            whiteOrdealRegion.mapEmission = leshy.mapEmission;
            whiteOrdealRegion.mapEmissionColor = leshy.mapEmissionColor;
            whiteOrdealRegion.predefinedNodes = ScriptableObject.CreateInstance<PredefinedNodes>();
            whiteOrdealRegion.predefinedNodes.nodeRows = new()
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
                        bossType = OpponentID,
                        specialBattleId = WhiteOrdeal,
                        difficulty = 20,
                        position = new(0.5f, 0.86f)
                    }
                }
            };
            return whiteOrdealRegion;
        }
    }

    public class OrdealBattleNodeData : CardBattleNodeData {
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