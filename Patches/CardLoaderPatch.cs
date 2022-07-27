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
        // Controls custom emission rules for added cards
        // E.g., forced emissions (always glowy), custom colours
        [HarmonyPostfix, HarmonyPatch(nameof(CardLoader.GetUnlockedCards))]
        public static void RemoveBackWardClock(ref List<CardInfo> __result)
        {
            if (PersistentValues.HasUsedBackwardClock)
            {
                __result.RemoveAll((CardInfo x) => x.name == "wstl_backwardClock");
            }
        }
    }
}
