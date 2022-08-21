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
    [HarmonyPatch(typeof(ResourcesManager))]
    public static class ResourcesManagerPatch
    {
        // Prevents bones from dropping under certain conditions
        [HarmonyPostfix, HarmonyPatch(nameof(ResourcesManager.AddBones))]
        public static IEnumerator AddBones(IEnumerator enumerator, CardSlot slot)
        {
            if (slot != null && slot.Card != null)
            {
                bool train = slot.Card.Info.GetExtendedProperty("wstl:KilledByTrain") != null && (bool)slot.Card.Info.GetExtendedPropertyAsBool("wstl:KilledByTrain");
                bool whiteNight = slot.Card.Info.HasAbility(TrueSaviour.ability) || slot.Card.Info.HasAbility(Apostle.ability) || slot.Card.Info.HasAbility(Confession.ability);
                if (train || whiteNight)
                {
                    if (train)
                    {
                        slot.Card.Info.SetExtendedProperty("wstl:KilledByTrain", false);
                    }
                    yield break;
                }
            }
            yield return enumerator;
        }
    }
    [HarmonyPatch(typeof(BoardManager))]
    public static class BoardManagerPatch
    {
        // Resets NumOfBlessings when the event ends with WhiteNight on the board
        [HarmonyPostfix, HarmonyPatch(nameof(BoardManager.CleanUp))]
        private static void ResetBlessings(ref BoardManager __instance)
        {
            if (__instance.AllSlotsCopy.FindAll((CardSlot s) => s.Card != null && s.Card.Info.name == "wstl_whiteNight").Count > 0)
            {
                ConfigUtils.Instance.UpdateBlessings(-ConfigUtils.Instance.NumOfBlessings);
                WstlPlugin.Log.LogDebug($"Resetting the clock to [0].");
            }
        }
    }
    [HarmonyPatch(typeof(Opponent))]
    public class OpponentPatch
    {
        // Adds Nothing There to the deck when chosen in a card choice (Trader, Boss Box, etc.)
        [HarmonyPostfix, HarmonyPatch(nameof(Opponent.OutroSequence))]
        public static IEnumerator ResetEffects(IEnumerator enumerator)
        {
            if (WstlSaveManager.HasSeenApocalypseEffects)
            {
                WstlSaveManager.HasSeenApocalypseEffects = false;
                Singleton<TableVisualEffectsManager>.Instance.ResetTableColors();
            }
            yield return enumerator;
        }
    }
}
