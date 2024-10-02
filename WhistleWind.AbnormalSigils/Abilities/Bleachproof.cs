using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Bleachproof()
        {
            const string rulebookName = "Bleachproof";
            const string rulebookDescription = "[creature] cannot have its sigils removed by the Bleach Pot.";
            Bleachproof.ability = AbnormalAbilityHelper.CreateAbility<Bleachproof>(
                "sigilBleachproof",
                rulebookName, rulebookDescription, powerLevel: 2,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }

    [HarmonyPatch]
    public class Bleachproof : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        [HarmonyPostfix, HarmonyPatch(typeof(BleachPotItem), nameof(BleachPotItem.GetValidOpponentSlots))]
        private static void RemoveImmuneCards(ref List<CardSlot> __result)
        {
            __result.RemoveAll(x => x.Card.HasAbility(Bleachproof.ability));
        }
    }
}
