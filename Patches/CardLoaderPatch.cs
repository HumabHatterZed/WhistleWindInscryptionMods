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
    [HarmonyPatch(typeof(CardLoader))]
    public static class CardLoaderPatch
    {
        // Removes select cards following specific sequences
        [HarmonyPostfix, HarmonyPatch(nameof(CardLoader.GetUnlockedCards))]
        public static void RemoveUniqueCards(ref List<CardInfo> __result)
        {
            if (PersistentValues.HasUsedBackwardClock)
            {
                __result.RemoveAll((CardInfo x) => x.name == "wstl_backwardClock");
            }
            if (PersistentValues.HasApocalypse)
            {
                __result.RemoveAll((CardInfo x) => x.name == "wstl_punishingBird"
                || x.name == "wstl_bigBird" || x.name == "wstl_judgementBird");
            }
        }
    }
}
