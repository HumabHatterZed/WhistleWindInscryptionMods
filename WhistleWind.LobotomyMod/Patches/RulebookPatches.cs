using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;

namespace WhistleWind.LobotomyMod.Patches
{
    [HarmonyPatch]
    internal class RulebookPatches
    {
        [HarmonyPatch(typeof(RuleBookController))]
        public static class OpenToAbilityPage_patch
        {
            // Reset the descriptions of WhiteNight-related abilities
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.SetShown))]
            public static bool ResetAlteredDescriptions(bool shown)
            {
                if (!shown)
                {
                    AbilitiesUtil.GetInfo(Apostle.ability).rulebookDescription = LobotomyPlugin.ApostleHiddenDescription;
                    AbilitiesUtil.GetInfo(TrueSaviour.ability).rulebookDescription = LobotomyPlugin.TrueSaviourHiddenDescription;
                    AbilitiesUtil.GetInfo(Confession.ability).rulebookDescription = LobotomyPlugin.ConfessionHiddenDescription;
                }
                return true;
            }
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.OpenToAbilityPage))]
            public static bool OpenToAbilityPage(PlayableCard card)
            {
                if (card != null && card.HasAnyOfAbilities(Apostle.ability, TrueSaviour.ability, Confession.ability))
                {
                    AbilitiesUtil.GetInfo(Apostle.ability).rulebookDescription = LobotomyPlugin.ApostleRevealedDescription;
                    AbilitiesUtil.GetInfo(TrueSaviour.ability).rulebookDescription = LobotomyPlugin.TrueSaviourRevealedDescription;
                    AbilitiesUtil.GetInfo(Confession.ability).rulebookDescription = LobotomyPlugin.ConfessionRevealedDescription;
                }
                return true;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RuleBookInfo), nameof(RuleBookInfo.AbilityShouldBeAdded))]
        private static void AddKayceeAbilities(ref int abilityIndex, ref bool __result)
        {
            if (SaveManager.SaveFile.IsPart1)
            {
                AbilityInfo info = AbilitiesUtil.GetInfo((Ability)abilityIndex);

                if (!SaveFile.IsAscension && info.metaCategories.Contains(AbilityMetaCategory.AscensionUnlocked))
                {
                    if (info.name.Equals("BoneDigger") || info.name.Equals("DeathShield") ||
                        info.name.Equals("DoubleStrike") || info.name.Equals("GainAttackOnKill") ||
                        info.name.Equals("StrafeSwap") || info.name.Equals("Morsel"))
                    {
                        __result = true;
                    }
                }
                if (SaveManager.SaveFile.IsPart1 && info.metaCategories.Contains(AbilityMetaCategory.Part3Rulebook))
                {
                    if (info.name.Equals("GainBattery") || info.name.Equals("LatchDeathShield") ||
                        info.name.Equals("MoveBeside"))
                    {
                        __result = true;
                    }
                }
            }
        }
    }
}
