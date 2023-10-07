using DiskCardGame;
using HarmonyLib;
using InscryptionAPI;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal static class CustomBossPatches
    {
        internal static bool FightingCustomBoss()
        {
            if (TurnManager.Instance?.Opponent != null)
            {
                if (TurnManager.Instance.Opponent is ApocalypseBossOpponent)
                    return true;
                // add other bosses later
            }
            return false;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(BleachPotItem), nameof(BleachPotItem.ActivateSequence))]
        private static IEnumerator PreventBleaching(IEnumerator enumerator, BleachPotItem __instance)
        {
            // prevent bleach usage during custom bosses (they rely pretty heavily on their sigils)
            if (!FightingCustomBoss())
            {
                yield return enumerator;
                yield break;
            }

            __instance.PlayExitAnimation();
            yield return new WaitForSeconds(0.4f);
            yield return TextDisplayer.Instance.ShowUntilInput("The bleach dissolves into colourless fumes, rendering the brush useless.");
        }
        [HarmonyPostfix, HarmonyPatch(typeof(BleachPotItem), nameof(BleachPotItem.ExtraActivationPrerequisitesMet))]
        private static void RemoveBleachOnGrab(ref bool __result)
        {
            // always remove bleach when it's chosen
            if (FightingCustomBoss())
                __result = true;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(LifeManager), nameof(LifeManager.ShowDamageSequence))]
        private static IEnumerator PreventOpponentDamage(IEnumerator enumerator, bool toPlayer)
        {
            if (!toPlayer && LobotomyPlugin.PreventOpponentDamage)
                yield break;

            yield return enumerator;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RunState), nameof(RunState.CurrentMapRegion), MethodType.Getter)]
        private static void ReplaceWithCustomBossRegion(ref RegionData __result)
        {
            if (SaveFile.IsAscension && RunState.Run.regionTier == RegionProgression.Instance.regions.Count - 1)
            {
                if (AscensionSaveData.Data.ChallengeIsActive(FinalApocalypse.Id))
                    __result = CustomBossUtils.apocalypseRegion;
/*                else if (AscensionSaveData.Data.ChallengeIsActive(FinalComing.Id))
                    __result = CustomBossUtils.saviourRegion;
                else if (AscensionSaveData.Data.ChallengeIsActive(FinalTrick.Id))
                    __result = CustomBossUtils.adultRegion;
                else if (AscensionSaveData.Data.ChallengeIsActive(FinalJester.Id))
                    __result = CustomBossUtils.jesterRegion;*/
            }
        }
    }
}
