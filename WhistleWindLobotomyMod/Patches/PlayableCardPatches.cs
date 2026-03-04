using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

// Patches to make abilities function properly
namespace WhistleWindLobotomyMod.Patches {
    [HarmonyPatch(typeof(PlayableCard))]
    internal class PlayableCardPatches {
        [HarmonyPrefix, HarmonyPatch(nameof(PlayableCard.OnStatsChanged))]
        private static bool SweepersHaveDifferentAttackColour(PlayableCard __instance) {
            if (__instance.Info.appearanceBehaviour.Contains(OrdealBackgroundIndigo.appearance) || __instance.Info.appearanceBehaviour.Contains(OrdealBackgroundIndigoTerrain.appearance)) {
                __instance.UpdateStatsText();
                __instance.RenderInfo.attackTextColor = ((__instance.GetPassiveAttackBuffs() + __instance.GetStatIconAttackBuffs() != 0) ? GameColors.Instance.limeGreen : Color.black);
                __instance.RenderInfo.temporaryMods = __instance.temporaryMods;
                __instance.RenderCard();
                if (__instance.OnBoard) {
                    __instance.UpdateFaceUpOnBoardEffects();
                }
                return false;
            }
            return true;
        }


        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.CanBeSacrificed), MethodType.Getter)]
        private static void CannotSacrificeApostles(PlayableCard __instance, ref bool __result) {
            if (__instance.HasTrait(Apostle))
                __result = false;
        }

        [HarmonyPrefix, HarmonyPatch(nameof(PlayableCard.Die))]
        private static bool DontDestroyCardsOnDeath(ref IEnumerator __result, PlayableCard __instance, bool wasSacrifice, PlayableCard killer) {
            if (!wasSacrifice) {
                if (__instance.HasSpecialAbility(Smile.specialAbility) && __instance.Info.name != Cards.mountainOfBodies) {
                    __result = HelperMethods.DieDontDestroy(__instance, wasSacrifice, killer);
                    return false;
                }

                if (__instance.HasTrait(Apostle)) {
                    // if killed by WhiteNight or One Sin, die normally
                    if (killer != null && killer.HasAnyOfAbilities(Confession.ID, TrueSaviour.ID))
                        return true;

                    bool friendlySaviour = BoardManager.Instance.GetCards(!__instance.OpponentCard).Exists(x => x.HasAbility(TrueSaviour.ID));

                    // Downed Apostles die normally without an ally WhiteNight
                    // Active Apostles always perform the special death
                    if (__instance.Info.name.EndsWith("Down") && !friendlySaviour)
                        return true;

                    __result = HelperMethods.DieDontDestroy(__instance, wasSacrifice, killer);
                    return false;
                }
            }

            return true;
        }
    }
}
