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
    [HarmonyPatch(typeof(CardGainAbility))]
    public static class CardGainAbilityPatch
    {
        // Makes WhiteNight, its Apostles, and Hundreds of Good Deeds immune to Touch of Death
        // Effectively gives them Made of Stone but without the whole 'they're not made of stone' thing
        [HarmonyPrefix, HarmonyPatch(nameof(CardGainAbility.RespondsToOtherCardAssignedToSlot))]
        public static bool NullCheck(ref PlayableCard otherCard)
        {
            if (!otherCard.Dead && otherCard.Slot.IsPlayerSlot && Singleton<CardGainAbility>.Instance.RespondsToOtherCardDrawn(otherCard) && !otherCard.HasAbility(Singleton<TotemTriggerReceiver>.Instance.Data.bottom.effectParams.ability))
            {
                return !otherCard.Info.Mods.Exists((CardModificationInfo x) => x.fromEvolve);
            }
            return false;
        }
    }
}
