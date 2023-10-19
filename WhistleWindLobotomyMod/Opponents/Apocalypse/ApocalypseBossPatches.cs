using DiskCardGame;
using HarmonyLib;
using InscryptionAPI;
using InscryptionAPI.Card;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal static class ApocalypseBossPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.GetPassiveAttackBuffs))]
        private static void ChangeColourDuringBigEyes(ref int __result)
        {
            if (TurnManager.Instance?.Opponent != null && TurnManager.Instance.Opponent is ApocalypseBossOpponent boss)
            {
                if (boss.BattleSequence.ActiveEggEffect == ActiveEggEffect.BigEyes)
                    __result = -1; // easiest way to change the colour
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.Attack), MethodType.Getter)]
        private static void NegatePowerChangeDuringBigEyes(PlayableCard __instance, ref int __result)
        {
            if (TurnManager.Instance?.Opponent != null && TurnManager.Instance.Opponent is ApocalypseBossOpponent boss)
            {
                if (boss.BattleSequence.ActiveEggEffect == ActiveEggEffect.BigEyes)
                    __result = Mathf.Max(0, __instance.Info.Attack);
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(MapDataReader), nameof(MapDataReader.SpawnMapObjects))]
        private static void MakeTheBlackForestBlack(MapDataReader __instance)
        {
            if (RunState.CurrentMapRegion == null || RunState.CurrentMapRegion != CustomBossUtils.apocalypseRegion)
                return;

            foreach (var i in __instance.scenery)
                i.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", Texture2D.blackTexture);
        }
    }
}
