using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;

namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch]
    public static class SpeedPatch
    {
        [HarmonyPostfix, HarmonyPatch(typeof(DoCombatPhasePatches), nameof(DoCombatPhasePatches.ModifyAttackingSlots))]
        private static void SortByCardSpeed(List<CardSlot> __result)
        {
            __result.Sort((CardSlot a, CardSlot b) => CardSpeed(b) - CardSpeed(a));
        }

        // Player cards have a base of 3 Speed, Opponent cards have a base of 0 Speed
        // This is to simulate how Inscryption is turn-based, with the player always going first
        // The end result is that Bind only really affects Player cards, while Haste only really affects Opponent cards
        // In situations where both are present this result changes slightly, but the point remains
        public static int CardSpeed(CardSlot slot)
        {
            if (slot.Card == null)
                return 0;

            int cardSpeed = slot.Card.OpponentCard ? 0 : 3;
            cardSpeed -= slot.Card.GetAbilityStacks(Bind.iconId);
            cardSpeed += slot.Card.GetAbilityStacks(Haste.iconId);

            return cardSpeed;
        }
    }
}
