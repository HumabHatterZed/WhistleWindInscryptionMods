using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    [HarmonyPatch(typeof(BoardManager))]
    public static class BoardManagerPatch
    {
        // Resets NumOfBlessings when the event ends with WhiteNight on the board
        [HarmonyPostfix, HarmonyPatch(nameof(BoardManager.CleanUp))]
        private static void ResetBlessings(ref BoardManager __instance)
        {
            if (ConfigUtils.Instance.NumOfBlessings >= 12 && __instance.AllSlotsCopy.FindAll((CardSlot s) => s.Card != null && s.Card.Info.name == "wstl_whiteNight").Count > 0)
            {
                ConfigUtils.Instance.UpdateBlessings(-ConfigUtils.Instance.NumOfBlessings);
                WstlPlugin.Log.LogDebug($"Resetting the clock to [0].");
            }
        }
    }
}
