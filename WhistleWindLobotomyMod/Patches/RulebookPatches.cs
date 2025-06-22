using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Patches {
    [HarmonyPatch]
    internal class RulebookPatches {
        /// <summary>
        /// Adds certain sigils to the Act 1 rulebook.
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(RuleBookInfo), nameof(RuleBookInfo.AbilityShouldBeAdded))]
        private static void AddKayceeAbilities(ref int abilityIndex, ref bool __result) {
            if (__result || !SaveManager.SaveFile.IsPart1)
                return;

            AbilityInfo info = AbilitiesUtil.GetInfo((Ability)abilityIndex);
            if (!abilityNames.Contains(info.name))
                return;

            __result = true;
        }

        private static readonly List<string> abilityNames = new()
        {
            "BoneDigger",
            "DeathShield",
            "DoubleStrike",
            "GainAttackOnKill",
            "GainBattery",
            "LatchDeathShield",
            "Morsel",
            "MoveBeside",
            "Sentry",
            "Sniper",
            "StrafeSwap",
            "Transformer"
        };
    }
}
