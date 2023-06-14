using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;

namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch]
    internal class RulebookPatches
    {
        [HarmonyPatch(typeof(RuleBookController))]
        private static class RulebookControllerPatches
        {
            // Reset the descriptions of WhiteNight-related abilities
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.SetShown))]
            public static bool ResetAlteredDescriptions(bool shown)
            {
                if (!shown)
                    AbilitiesUtil.GetInfo(Conductor.ability).ResetDescription();

                return true;
            }
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.OpenToAbilityPage))]
            private static bool OpenToAbilityPage(PlayableCard card)
            {
                if (card != null && card.HasAbility(Conductor.ability))
                {
                    var component = card.GetComponent<Conductor>();
                    if (!component || component.turnCount == 0)
                        return true;

                    AbilitiesUtil.GetInfo(Conductor.ability).rulebookDescription = ConductorDescriptions[Mathf.Min(2, component.turnCount - 1)];
                }
                return true;
            }
        }

        private static readonly List<string> ConductorDescriptions = new()
        {
            "Adjacent creatures gain Power equal to half this card's Power, rounded down. This effect changes next turn.",
            "Allied creatures gain Power equal to half this card's Power, rounded down. This effect changes next turn.",
            "All other creatures gain Power equal to this card's Power."
        };
    }
}
