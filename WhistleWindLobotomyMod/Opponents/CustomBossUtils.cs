using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Opponents
{
    [HarmonyPatch]
    public static class CustomBossUtils
    {
        [HarmonyPatch(typeof(AudioController), "GetAudioClip")]
        [HarmonyPrefix]
        public static void AddAudios(AudioController __instance)
        {
            __instance.SFX.AddRange(bossAudioClips.Where(x => !__instance.SFX.Contains(x)));
        }

        internal static void InitBossObjects()
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WhistleWindLobotomyMod.lobmodbosses");
            bossBundle = AssetBundle.LoadFromStream(stream);
            apocalypsePrefab = bossBundle.LoadAsset<GameObject>("ApocalypseBoss");
            bossAudioClips = new()
            {
                bossBundle.LoadAsset<AudioClip>("apocalypseRoar")
            };

            apocalypseRegion = ApocalypseBossUtils.CreateRegion();
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
        }

        public static RegionData apocalypseRegion;
        public static RegionData saviourRegion;
        public static RegionData adultRegion;
        public static RegionData jesterRegion;

        public static AssetBundle bossBundle;
        public static GameObject apocalypsePrefab;
        public static List<AudioClip> bossAudioClips;
    }
}
