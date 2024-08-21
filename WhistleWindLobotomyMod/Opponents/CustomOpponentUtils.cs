using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Opponents
{
    [HarmonyPatch]
    public static class CustomOpponentUtils
    {
        public static bool FightingCustomOpponent(bool bossOnly)
        {
            if (TurnManager.Instance?.Opponent != null)
            {
                if (bossOnly)
                    return TurnManager.Instance.Opponent is LobotomyBossOpponent;
                else
                    return TurnManager.Instance.Opponent is LobotomyBossOpponent || TurnManager.Instance.Opponent is OrdealOpponent;
            }
            return false;
        }

        public static bool DirectDamageGivesBones()
        {
            if (TurnManager.Instance.SpecialSequencer != null)
            {
                return (TurnManager.Instance.SpecialSequencer as LobotomyBattleSequencer).DirectDamageGivesBones;
            }
            return false;
        }
        public static bool IsCustomBoss<T>() where T : LobotomyBossOpponent
        {
            return TurnManager.Instance.Opponent is T;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(AudioController), nameof(AudioController.GetAudioClip))]
        public static void AddAudioClips(AudioController __instance) => __instance.SFX.AddRange(bossSFX.Where(x => !__instance.SFX.Contains(x)));

        [HarmonyPrefix, HarmonyPatch(typeof(AudioController), nameof(AudioController.GetLoopClip))]
        public static void AddAudioLoops(AudioController __instance) => __instance.Loops.AddRange(bossLoop.Where(x => !__instance.Loops.Contains(x)));

        internal static void InitBossObjects()
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WhistleWindLobotomyMod.lobmodbosses");
            bossBundle = AssetBundle.LoadFromStream(stream);
            apocalypsePrefab = bossBundle.LoadAsset<GameObject>("ApocalypseBoss");
            bossSFX = new()
            {
                bossBundle.LoadAsset<AudioClip>("bird_roar"),
                bossBundle.LoadAsset<AudioClip>("bird_mouth"),
                bossBundle.LoadAsset<AudioClip>("bird_down"),
                bossBundle.LoadAsset<AudioClip>("bird_laser_fire"),
                bossBundle.LoadAsset<AudioClip>("bird_dead")
            };
            bossLoop = new()
            {
                bossBundle.LoadAsset<AudioClip>("second_trumpet_intro"),
                bossBundle.LoadAsset<AudioClip>("second_trumpet_intro_loop"),
                bossBundle.LoadAsset<AudioClip>("second_trumpet_main"),
                bossBundle.LoadAsset<AudioClip>("second_trumpet_main_loop")
            };

            ApocalypseBossID = OpponentManager.Add(
                LobotomyPlugin.pluginGuid, "ApocalypseBossOpponent",
                ApocalypseBattleSequencer.ID, typeof(ApocalypseBossOpponent),
                NodeHelper.GetNodeTextureList("nodeApocalypseBoss1", "nodeApocalypseBoss2", "nodeApocalypseBoss3", "nodeApocalypseBoss4")
                ).Id;

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

        public static Opponent.Type ApocalypseBossID { get; private set; }
        public static Opponent.Type SaviourBossID { get; private set; }

        public static AssetBundle bossBundle;
        public static GameObject apocalypsePrefab;

        public static List<AudioClip> bossSFX;
        public static List<AudioClip> bossLoop;

        public enum LobotomyBoss
        {
            Apocalypse,
            Saviour,
            Adult,
            Jester
        }
    }
}
