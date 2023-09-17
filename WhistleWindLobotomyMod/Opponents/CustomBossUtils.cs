using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Opponents
{
    [HarmonyPatch]
    public static class CustomBossUtils
    {
        public static AssetBundle bossBundle;
        public static GameObject apocalypsePrefab;

        internal static void InitBossObjects()
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WhistleWindLobotomyMod.lobmodbosses");
            bossBundle = AssetBundle.LoadFromStream(stream);
            apocalypsePrefab = bossBundle.LoadAsset<GameObject>("ApocalypseBoss");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(LifeManager), nameof(LifeManager.ShowDamageSequence))]
        internal static bool PreventOpponentDamage(bool toPlayer)
        {
            if (TurnManager.Instance.Opponent is ApocalypseBossOpponent && !toPlayer)
                return (TurnManager.Instance.Opponent as ApocalypseBossOpponent).apocalypseHead == null;

            return true;
        }
    }
}
