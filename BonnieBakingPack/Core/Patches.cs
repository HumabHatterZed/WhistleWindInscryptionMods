using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.PackManagement;
using InscryptionAPI;
using InscryptionAPI.Helpers;
using System.Reflection;

namespace BonniesBakingPack
{
    [HarmonyPatch]
    public static class Patches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(DrawRandomCardOnDeath), nameof(DrawRandomCardOnDeath.CardToDraw), MethodType.Getter)]
        private static void PhoneMouseCallsThePopo(DrawRandomCardOnDeath __instance, ref CardInfo __result)
        {
            if (__instance.Card?.Info.name == "bbp_mousePhone")
                __result = CardLoader.GetCardByName("bbp_policeWolf");
        }

/*        [HarmonyPrefix, HarmonyPatch(typeof(VariableStatBehaviour), nameof(VariableStatBehaviour.UpdateStats))]
        private static bool BingusIsInfinite(VariableStatBehaviour __instance)
        {
            if (__instance.IconType == BingusStatIcon.Icon)
            {
                int[] values = __instance.GetStatValues();
                __instance.statsMod.attackAdjustment = values[0];
                __instance.statsMod.healthAdjustment = values[1];
                return false;
            }
            return true;
        }*/
    }
}
