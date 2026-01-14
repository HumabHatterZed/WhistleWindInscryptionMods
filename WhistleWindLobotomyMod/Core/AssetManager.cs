using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Resource;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod.Core {
    [HarmonyPatch]
    public static class AssetManager {
        private static AssetBundle musicBundle;

        internal static GameObject warningTargetPrefab;
        internal static GameObject ordealCounterPrefab;
        internal static GameObject ordealBannerPrefab;

        public static readonly List<AudioClip> sfxClips = new();
        public static readonly List<AudioClip> musicLoops = new();

        public static readonly List<GameObject> scenery = new();

        public static readonly Dictionary<string, List<SceneryData>> CustomSceneryData = new();
        public static int CardOffscreenLayer { get; internal set; }

        private static AssetBundle assetBundle;
        private static Stream assetBundleStream;

        internal static void Initialise() {
            CardOffscreenLayer = CardLoader.GetCardByName("!GIANTCARD_MOON").AnimatedPortrait.transform.GetChild(0).gameObject.layer;

            ResourceBankManager.Add(LobotomyPlugin.pluginGuid, new() {
                asset = TextureLoader.LoadTextureFromFile("sigilDelusion1.png", LobotomyPlugin.ModAssembly),
                path = "Art/Cards/AbilityIcons/sigilDelusion_1"
            });
            ResourceBankManager.Add(LobotomyPlugin.pluginGuid, new() {
                asset = TextureLoader.LoadTextureFromFile("sigilDelusion2.png", LobotomyPlugin.ModAssembly),
                path = "Art/Cards/AbilityIcons/sigilDelusion_2"
            });
            ResourceBankManager.Add(LobotomyPlugin.pluginGuid, new() {
                asset = TextureLoader.LoadTextureFromFile("sigilDelusion3.png", LobotomyPlugin.ModAssembly),
                path = "Art/Cards/AbilityIcons/sigilDelusion_3"
            });
            ResourceBankManager.Add(LobotomyPlugin.pluginGuid, new() {
                asset = TextureLoader.LoadTextureFromFile("sigilDelusion4.png", LobotomyPlugin.ModAssembly),
                path = "Art/Cards/AbilityIcons/sigilDelusion_4"
            });

            assetBundleStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WhistleWindLobotomyMod.lobmodassets");
            assetBundle = AssetBundle.LoadFromStream(assetBundleStream);
            warningTargetPrefab = assetBundle.LoadAsset<GameObject>("WarningTargetIcon");
            ordealCounterPrefab = assetBundle.LoadAsset<GameObject>("OrdealCounter");
            ordealBannerPrefab = assetBundle.LoadAsset<GameObject>("OrdealBanner");
            sfxClips.Add(assetBundle.LoadAsset<AudioClip>("soda_open"));
            sfxClips.Add(assetBundle.LoadAsset<AudioClip>("can_hit"));

            GameObject obj = assetBundle.LoadAsset<GameObject>("twisted_building");
            obj.AddComponent<MapElement>().Data = new MapElementData();

            GameObject obj2 = assetBundle.LoadAsset<GameObject>("twisted_building_2");
            obj2.AddComponent<MapElement>().Data = new MapElementData();

            ResourceBankManager.Add(LobotomyPlugin.pluginGuid, SceneryData.PREFABS_ROOT + "twisted_building", obj);
            ResourceBankManager.Add(LobotomyPlugin.pluginGuid, SceneryData.PREFABS_ROOT + "twisted_building_2", obj2);
            List<SceneryData> twistedBuildings = new() { ScriptableObject.CreateInstance<SceneryData>() };
            twistedBuildings[0].radius = 0.096f;
            twistedBuildings[0].minScale = new(6f, 6f);
            twistedBuildings[0].maxScale = new(8f, 8f);

            twistedBuildings[0].prefabNames = new() { "twisted_building", "twisted_building_2" };
            twistedBuildings[0].baseEulers = new(266f, 0f, 0f);

            CustomSceneryData.Add("twisted_building", twistedBuildings);

            OrdealUtils.InitOrdeals(assetBundle);
            LobOpponentUtils.InitBossObjects(assetBundle);

            using (Stream musicStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WhistleWindLobotomyMod.lobmodmusic")) {
                musicBundle = AssetBundle.LoadFromStream(musicStream);

                musicLoops.Add(musicBundle.LoadAsset<AudioClip>("second_trumpet_intro"));
                musicLoops.Add(musicBundle.LoadAsset<AudioClip>("second_trumpet_intro_loop"));
                musicLoops.Add(musicBundle.LoadAsset<AudioClip>("second_trumpet_main"));
                musicLoops.Add(musicBundle.LoadAsset<AudioClip>("second_trumpet_main_loop"));

                // streamed audio won't play if the bundle is unloaded
            }
        }

        public static void UnloadAssetBundle() {
            assetBundle.Unload(false);
            assetBundleStream.Dispose();
        }

        /// <summary>
        /// Recursively goes through each Transform in a given GameObjct and sets its layer to CardOffscreenLayer.
        /// </summary>
        /// <param name="obj"></param>
        private static void FixAnimatedPortraitLayers(GameObject obj) {
            if (obj.layer == 0)
                obj.layer = CardOffscreenLayer;
            foreach (Transform child in obj.transform) {
                FixAnimatedPortraitLayers(child.gameObject);
            }
        }

        /// <remarks>
        /// Must only be called when first loading the mod in Awake().
        /// </remarks>
        internal static GameObject GetAnimatedPortraitPrefab(string prefabName) {
            GameObject prefab = assetBundle.LoadAsset<GameObject>(prefabName);
            FixAnimatedPortraitLayers(prefab);
            return prefab;
        }

        /// <remarks>
        /// Must only be called when first loading the mod in Awake().
        /// </remarks>
        internal static GameObject GetGameObject(string prefabName) {
            GameObject prefab = assetBundle.LoadAsset<GameObject>(prefabName);
            return prefab;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(AudioController), nameof(AudioController.GetAudioClip))]
        public static void AddAudioClips(AudioController __instance) => __instance.SFX.AddRange(sfxClips.Where(x => !__instance.SFX.Contains(x)));

        [HarmonyPrefix, HarmonyPatch(typeof(AudioController), nameof(AudioController.GetLoopClip))]
        public static void AddAudioLoops(AudioController __instance) => __instance.Loops.AddRange(musicLoops.Where(x => !__instance.Loops.Contains(x)));
    }
}
