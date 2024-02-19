using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

// Patches to make abilities function properly
namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(PlayableCard))]
    internal class PlayableCardPatches
    {
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.CanBeSacrificed), MethodType.Getter)]
        private static void CannotSacrificeApostles(PlayableCard __instance, ref bool __result)
        {
            if (__instance.HasTrait(Apostle))
                __result = false;
        }

        [HarmonyPrefix, HarmonyPatch(nameof(PlayableCard.Die))]
        private static bool DontDestroyCardsOnDeath(ref IEnumerator __result, PlayableCard __instance, bool wasSacrifice, PlayableCard killer)
        {
            if (__instance.HasSpecialAbility(Smile.specialAbility) && __instance.Info.name != "wstl_mountainOfBodies")
            {
                __result = HelperMethods.DieDontDestroy(__instance, wasSacrifice, killer);
                return false;
            }

            if (__instance.HasTrait(Apostle))
            {
                // if killed by WhiteNight or One Sin, die normally
                if (killer != null && killer.HasAnyOfAbilities(Confession.ability, TrueSaviour.ability))
                    return true;

                bool friendlySaviour = BoardManager.Instance.GetSlotsCopy(!__instance.OpponentCard).Exists(x => x.Card?.HasAbility(TrueSaviour.ability) ?? false);

                // Downed Apostles die normally without an ally WhiteNight
                // Active Apostles always perform the special death
                if (__instance.Info.name.EndsWith("Down") && !friendlySaviour)
                    return true;

                __result = HelperMethods.DieDontDestroy(__instance, wasSacrifice, killer);
                return false;
            }
            return true;
        }
    }
}
