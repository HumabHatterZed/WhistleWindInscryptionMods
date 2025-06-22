using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WhistleWind.AbnormalSigils.Core;

namespace WhistleWind.AbnormalSigils.Patches {
    [HarmonyPatch]
    public static class RuleBookPatches {
        public static PlayableCard CardForRuleBook = null;

        [HarmonyPrefix, HarmonyPatch(typeof(RuleBookController), nameof(RuleBookController.SetShown))]
        public static bool ResetAlteredDescriptions(bool shown) {
            if (!shown)
                CardForRuleBook = null;

            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(RuleBookController), nameof(RuleBookController.OpenToAbilityPage))]
        private static bool OpenToAbilityPage(string abilityName, PlayableCard card) {
            CardForRuleBook = card;
            return true;
        }
    }
}
