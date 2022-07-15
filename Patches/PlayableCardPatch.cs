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
    [HarmonyPatch(typeof(PlayableCard))]
    public class PlayableCardPatch
    {
        // Increases damage taken by amount of Prudence a card has
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.TakeDamage))]
        public static void TakePrudenceDamage(PlayableCard __instance, ref int damage)
        {
            int prudence = !(__instance.Info.GetExtendedPropertyAsInt("wstl:Prudence") != null) ? 0 : (int)__instance.Info.GetExtendedPropertyAsInt("wstl:Prudence");
            if (prudence > 0)
            {
                damage += prudence;
            }
        }
    }
}
