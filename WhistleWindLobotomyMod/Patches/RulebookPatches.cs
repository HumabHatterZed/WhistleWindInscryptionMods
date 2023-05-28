using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

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
                    AbilitiesUtil.GetInfo(Apostle.ability).ResetDescription();
                    AbilitiesUtil.GetInfo(TrueSaviour.ability).ResetDescription();
                    AbilitiesUtil.GetInfo(Confession.ability).ResetDescription();
                }
                return true;
            }
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.OpenToAbilityPage))]
            private static bool OpenToAbilityPage(PlayableCard card)
            {
                if (card != null && card.HasAnyOfAbilities(Apostle.ability, TrueSaviour.ability, Confession.ability))
                {
                    AbilitiesUtil.GetInfo(Apostle.ability).rulebookDescription = "[creature] will enter a downed state instead of dying. Downed creatures are invulnerable under special conditions.";
                    AbilitiesUtil.GetInfo(TrueSaviour.ability).rulebookDescription = $"While {card.Info.DisplayedNameLocalized} is on the board, remove ally Terrain and Pelt cards and transform the rest into random Apostles.";
                    AbilitiesUtil.GetInfo(Confession.ability).rulebookDescription = "Kill WhiteNight and all Apostles on the board then deal 33 direct damage.";
                }
                return true;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RuleBookInfo), nameof(RuleBookInfo.AbilityShouldBeAdded))]
        private static void AddKayceeAbilities(ref int abilityIndex, ref bool __result)
        {
            if (!SaveManager.SaveFile.IsPart1 || __result)
                return;

            AbilityInfo info = AbilitiesUtil.GetInfo((Ability)abilityIndex);

            if (!abilityNames.Contains(info.name))
                return;

            __result = true;

            switch (info.name)
            {
                case "Sniper":
                    info.rulebookName = "Marksman";
                    info.triggerText = "Your beast strikes with precision.";
                    info.SetIcon(TextureLoader.LoadTextureFromBytes(Artwork.sigilMarksman));
                    info.SetPixelAbilityIcon(TextureLoader.LoadTextureFromBytes(Artwork.sigilMarksman_pixel));
                    return;
                case "Sentry":
                    info.rulebookName = "Quick Draw";
                    info.triggerText = "The early bird gets the worm.";
                    info.SetIcon(TextureLoader.LoadTextureFromBytes(Artwork.sigilQuickDraw));
                    info.SetPixelAbilityIcon(TextureLoader.LoadTextureFromBytes(Artwork.sigilQuickDraw_pixel));
                    info.SetCanStack();
                    return;
            }
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
