using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace WhistleWindLobotomyMod.Opponents
{
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
    }
}
