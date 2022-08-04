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
    [HarmonyPatch(typeof(GrimoraSaveData))]
    public static class GrimoraTesting
    {
        // Controls custom emission rules for added cards
        // E.g., forced emissions (always glowy), custom colours
        [HarmonyPostfix, HarmonyPatch(nameof(GrimoraSaveData.Initialize))]
        public static void Postfix(ref GrimoraSaveData __instance)
        {
            CardInfo info = CardLoader.GetCardByName("wstl_testingDummy");

            for (int i = 0; i < 3; i++)
            {
                __instance.deck.AddCard(info);
            }
        }
    }
}
