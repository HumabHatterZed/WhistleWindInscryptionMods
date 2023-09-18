using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Patches
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
                {
                    AbilitiesUtil.GetInfo(Ability.DebuffEnemy).rulebookName = "Stinky";
                    AbilitiesUtil.GetInfo(Apostle.ability).ResetDescription();
                    AbilitiesUtil.GetInfo(TrueSaviour.ability).ResetDescription();
                    AbilitiesUtil.GetInfo(Confession.ability).ResetDescription();
                }
                return true;
            }
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.OpenToAbilityPage))]
            private static bool OpenToAbilityPage(string abilityName, PlayableCard card)
            {
                if (card)
                {
                    if (abilityName == "DebuffEnemy" && card.Info.displayedName == "Ppodae")
                        AbilitiesUtil.GetInfo(Ability.DebuffEnemy).rulebookName = "Cute Lil Guy";

                    if (card.HasTrait(LobotomyCardManager.TraitApostle))
                    {
                        AbilitiesUtil.GetInfo(Apostle.ability).rulebookDescription = "[creature] will enter a downed state instead of dying. Downed creatures are invulnerable under special conditions.";
                        AbilitiesUtil.GetInfo(TrueSaviour.ability).rulebookDescription = $"While {card.Info.DisplayedNameLocalized} is on the board, remove ally Terrain and Pelt cards and transform the rest into random Apostles.";
                        AbilitiesUtil.GetInfo(Confession.ability).rulebookDescription = "Kill WhiteNight and all Apostles on the board then deal 33 direct damage.";
                    }
                }
                return true;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RuleBookInfo), nameof(RuleBookInfo.AbilityShouldBeAdded))]
        private static void AddKayceeAbilities(ref int abilityIndex, ref bool __result)
        {
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
            "StrafeSwap"
        };
    }
}
