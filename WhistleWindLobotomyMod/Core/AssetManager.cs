using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Resource;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod.Core {
    [HarmonyPatch]
    public static class AssetManager {
        public static AssetBundle BossBundle { get; private set; }
        public static AssetBundle AssetBundle { get; private set; }

        internal static GameObject warningTargetPrefab;
        internal static GameObject ordealCounterPrefab;
        internal static GameObject ordealBannerPrefab;

        public static readonly List<AudioClip> sfxClips = new();
        public static readonly List<AudioClip> musicLoops = new();

        public static readonly List<GameObject> scenery = new();

        public static readonly Dictionary<string, List<SceneryData>> CustomSceneryData = new();

        internal static void Initialise() {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WhistleWindLobotomyMod.lobmodbosses")) {
                BossBundle = AssetBundle.LoadFromStream(stream);
            }

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WhistleWindLobotomyMod.lobmodassets")) {
                AssetBundle = AssetBundle.LoadFromStream(stream);
            }

            warningTargetPrefab = AssetBundle.LoadAsset<GameObject>("WarningTargetIcon");
            ordealCounterPrefab = AssetBundle.LoadAsset<GameObject>("OrdealCounter");
            ordealBannerPrefab = AssetBundle.LoadAsset<GameObject>("OrdealBanner");
            sfxClips.Add(AssetBundle.LoadAsset<AudioClip>("soda_open"));
            sfxClips.Add(AssetBundle.LoadAsset<AudioClip>("can_hit"));

            GameObject obj = AssetBundle.LoadAsset<GameObject>("twisted_building");
            obj.AddComponent<MapElement>().Data = new MapElementData();

            GameObject obj2 = AssetBundle.LoadAsset<GameObject>("twisted_building_2");
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

            ResourceBankManager.Add(LobotomyPlugin.pluginGuid, "Prefabs/Environment/TableEffects/" + "CityTableEffects", BossBundle.LoadAsset<GameObject>("CityTableEffects"));

            LobOpponentUtils.InitBossObjects();
            OrdealUtils.InitOrdeals();
        }

        [HarmonyPrefix, HarmonyPatch(typeof(AudioController), nameof(AudioController.GetAudioClip))]
        public static void AddAudioClips(AudioController __instance) => __instance.SFX.AddRange(sfxClips.Where(x => !__instance.SFX.Contains(x)));

        [HarmonyPrefix, HarmonyPatch(typeof(AudioController), nameof(AudioController.GetLoopClip))]
        public static void AddAudioLoops(AudioController __instance) => __instance.Loops.AddRange(musicLoops.Where(x => !__instance.Loops.Contains(x)));
    }
}
