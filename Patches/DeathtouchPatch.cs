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
    [HarmonyPatch(typeof(Deathtouch))]
    public class DeathtouchPatch
    {
        // Makes WhiteNight, its Apostles, and Hundreds of Good Deeds immune to Touch of Death
        // Effectively gives them Made of Stone but without the whole 'they're not made of stone' thing
        [HarmonyPrefix, HarmonyPatch(nameof(Deathtouch.RespondsToDealDamage))]
        public static bool ImmunetoDeathTouch(ref int amount, ref PlayableCard target)
        {
            bool whiteNightEvent = !target.HasAbility(TrueSaviour.ability) && !target.HasAbility(Apostle.ability) && !target.HasAbility(Confession.ability);
            if (amount > 0 && target != null && !target.Dead)
            {
                return whiteNightEvent && !target.HasAbility(Ability.MadeOfStone);
            }
            return false;
        }
    }
}
