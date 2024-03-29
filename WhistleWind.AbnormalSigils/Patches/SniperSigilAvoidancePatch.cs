﻿using DiskCardGame;
using HarmonyLib;
using InscryptionCommunityPatch.Card;

namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch]
    internal class SniperSigilAvoidancePatch
    {
        [HarmonyPostfix, HarmonyPatch(typeof(SniperFix), nameof(SniperFix.WillDieFromSharp))]
        private static void AddExtraChecks(PlayableCard pc, CardSlot slot, ref bool __result)
        {
            if (slot.Card.HasAbility(Punisher.ability) || (slot.Card.HasAbility(Reflector.ability) && pc.Attack >= pc.Health))
                __result = true;
        }
    }
}
