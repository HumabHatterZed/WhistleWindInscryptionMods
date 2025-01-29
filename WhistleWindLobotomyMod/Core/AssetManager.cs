using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod.Core
{
    [HarmonyPatch]
    public static class AssetManager
    {
        public static AssetBundle BossBundle { get; private set; }
        public static AssetBundle AssetBundle { get; private set; }

        internal static GameObject warningTargetPrefab;
        internal static GameObject ordealCounterPrefab;
        internal static GameObject ordealBannerPrefab;

        public static readonly List<AudioClip> sfxClips = new();
        public static readonly List<AudioClip> musicLoops = new();

        internal static void Initialise()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WhistleWindLobotomyMod.lobmodbosses"))
            {
                BossBundle = AssetBundle.LoadFromStream(stream);
            }

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WhistleWindLobotomyMod.lobmodassets"))
            {
                AssetBundle = AssetBundle.LoadFromStream(stream);
            }

            warningTargetPrefab = AssetBundle.LoadAsset<GameObject>("WarningTargetIcon");
            ordealCounterPrefab = AssetBundle.LoadAsset<GameObject>("OrdealCounter");
            ordealBannerPrefab = AssetBundle.LoadAsset<GameObject>("OrdealBanner");
            LobOpponentUtils.InitBossObjects();
            OrdealUtils.InitOrdeals();
        }

        [HarmonyPrefix, HarmonyPatch(typeof(AudioController), nameof(AudioController.GetAudioClip))]
        public static void AddAudioClips(AudioController __instance) => __instance.SFX.AddRange(sfxClips.Where(x => !__instance.SFX.Contains(x)));

        [HarmonyPrefix, HarmonyPatch(typeof(AudioController), nameof(AudioController.GetLoopClip))]
        public static void AddAudioLoops(AudioController __instance) => __instance.Loops.AddRange(musicLoops.Where(x => !__instance.Loops.Contains(x)));
    }
}
