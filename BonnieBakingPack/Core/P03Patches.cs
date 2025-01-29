using DiskCardGame;
using HarmonyLib;
using Infiniscryption.P03SigilLibrary.Sigils;

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
