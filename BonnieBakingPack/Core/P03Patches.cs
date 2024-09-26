using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using GBC;
using HarmonyLib;
using Infiniscryption.P03SigilLibrary.Sigils;
using Infiniscryption.PackManagement;
using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BonniesBakingPack
{
    public static class P03Patches
    {
        [HarmonyPrefix, HarmonyPatch(typeof(FuelManager), "Render3DFuel"), HarmonyBefore(BakingPlugin.ScrybeCompat.P03Sigil)]
        private static bool FixNullInfoError(Card __instance)
        {
            return __instance?.Info != null;
        }
    }
}
