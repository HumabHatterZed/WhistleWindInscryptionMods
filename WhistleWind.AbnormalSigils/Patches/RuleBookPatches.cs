using DiskCardGame;
using HarmonyLib;

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
