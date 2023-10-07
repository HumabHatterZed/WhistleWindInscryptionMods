using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal class RulebookPatches
    {
        [HarmonyPatch(typeof(RuleBookController))]
        private static class RulebookControllerPatches
        {
            private static bool changedRulebook = false;
            // Reset the descriptions of WhiteNight-related abilities
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.SetShown))]
            public static bool ResetAlteredDescriptions(bool shown)
            {
                if (!shown && changedRulebook)
                {
                    DynamicAbilities[0].rulebookName = "Stinky";
                    for (int i = 1; i < DynamicAbilities.Count; i++)
                        DynamicAbilities[i].ResetDescription();

                    changedRulebook = false;
                }
                return true;
            }
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.OpenToAbilityPage))]
            private static bool OpenToAbilityPage(string abilityName, PlayableCard card)
            {
                if (card)
                {
                    if (abilityName == "DebuffEnemy" && card.Info.displayedName == "Ppodae")
                    {
                        changedRulebook = true;
                        DynamicAbilities[0].rulebookName = "Cute Lil Guy";
                    }

                    if (card.HasTrait(LobotomyCardManager.TraitApostle))
                    {
                        changedRulebook = true;
                        DynamicAbilities[1].rulebookDescription = "[creature] will enter a downed state instead of dying. Downed creatures are invulnerable under special conditions.";
                        DynamicAbilities[2].rulebookDescription = $"While {card.Info.DisplayedNameLocalized} is on the board, remove ally Terrain and Pelt cards and transform the rest into random Apostles.";
                        DynamicAbilities[3].rulebookDescription = "Kill WhiteNight and all Apostles on the board then deal 33 direct damage.";
                    }
                    if (TurnManager.Instance?.Opponent != null && TurnManager.Instance.Opponent is ApocalypseBossOpponent)
                    {
                        changedRulebook = true;
                        DynamicAbilities[4].rulebookDescription = TurnManager.Instance.Opponent.NumLives switch
                        {
                            4 => "Every 3 turns, trigger then switch the active egg effect. This card cannot go below 90 Health. During the final phase, this sigil changes behaviour.",
                            3 => "Every 3 turns, trigger then switch the active egg effect. This card cannot go below 60 Health. During the final phase, this sigil changes behaviour.",
                            2 => "Every 3 turns, trigger then switch the active egg effect. This card cannot go below 30 Health. During the final phase, this sigil changes behaviour.",
                            _ => "Apocalypse Bird will kill small creatures, like Squirrels, at the start of its turn, and heal 1 Health per creature killed this way."
                        };
                    }
                }
                return true;
            }
        }

        private static readonly List<AbilityInfo> DynamicAbilities = new()
        {
            AbilitiesUtil.GetInfo(Ability.DebuffEnemy),
            AbilitiesUtil.GetInfo(Apostle.ability),
            AbilitiesUtil.GetInfo(TrueSaviour.ability),
            AbilitiesUtil.GetInfo(Confession.ability),
            AbilitiesUtil.GetInfo(ApocalypseAbility.ability)
        };

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
