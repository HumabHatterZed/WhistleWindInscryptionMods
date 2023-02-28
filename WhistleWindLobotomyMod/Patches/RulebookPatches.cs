using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal class RulebookPatches
    {
        [HarmonyPatch(typeof(RuleBookController))]
        private static class OpenToAbilityPage_patch
        {
            // Reset the descriptions of WhiteNight-related abilities
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.SetShown))]
            public static bool ResetAlteredDescriptions(bool shown)
            {
                if (!shown)
                {
                    AbilityManager.FullAbility apostle = AbilityManager.AllAbilities.Find(x => x.Id == Apostle.ability);
                    AbilityManager.FullAbility saviour = AbilityManager.AllAbilities.Find(x => x.Id == TrueSaviour.ability);
                    AbilityManager.FullAbility confession = AbilityManager.AllAbilities.Find(x => x.Id == Confession.ability);
                    AbilitiesUtil.GetInfo(Apostle.ability).rulebookDescription = apostle.BaseRulebookDescription;
                    AbilitiesUtil.GetInfo(TrueSaviour.ability).rulebookDescription = saviour.BaseRulebookDescription;
                    AbilitiesUtil.GetInfo(Confession.ability).rulebookDescription = confession.BaseRulebookDescription;
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
                        if (info.name.Equals("LatchDeathShield"))
                            info.rulebookDescription = "When this card perishes, give a card the Armoured sigil.";
                        
                        __result = true;
                    }
                }
            }
        }
    }
}
