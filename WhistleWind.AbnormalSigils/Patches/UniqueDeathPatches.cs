using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch(typeof(PlayableCard))]
    internal class UniqueDeathPatches
    {
        [HarmonyPrefix, HarmonyPatch(nameof(PlayableCard.Die))]
        private static bool TransformOnDie(ref IEnumerator __result, PlayableCard __instance, bool wasSacrifice, PlayableCard killer)
        {
            if (__instance.HasAbility(Slime.ability) && __instance.LacksTrait(AbnormalPlugin.LovingSlime))
            {
                __result = HelperMethods.DieDontDestroy(__instance, wasSacrifice, killer);
                return false;
            }

            return true;
        }
    }
}
