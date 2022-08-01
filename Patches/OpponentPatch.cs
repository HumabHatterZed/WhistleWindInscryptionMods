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
