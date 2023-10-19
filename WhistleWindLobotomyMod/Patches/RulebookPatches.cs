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
                    AbilitiesUtil.GetInfo(DynamicAbilities[0]).rulebookName = "Stinky";
                    for (int i = 1; i < DynamicAbilities.Count; i++)
                        AbilitiesUtil.GetInfo(DynamicAbilities[i]).ResetDescription();

                    changedRulebook = false;
                }
                return true;
            }
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.OpenToAbilityPage))]
            private static bool OpenToAbilityPage(string abilityName, PlayableCard card)
            {
                if (card != null)
                {
                    if (abilityName == "DebuffEnemy" && card.Info.displayedName == "Ppodae")
                    {
                        changedRulebook = true;
                        AbilitiesUtil.GetInfo(DynamicAbilities[0]).rulebookName = "Cute Lil Guy";
                    }

                    if (card.HasTrait(LobotomyCardManager.TraitApostle))
                    {
                        changedRulebook = true;
                        AbilitiesUtil.GetInfo(DynamicAbilities[1]).rulebookDescription = "[creature] will enter a downed state instead of dying. Downed creatures are invulnerable under special conditions.";
                        AbilitiesUtil.GetInfo(DynamicAbilities[2]).rulebookDescription = $"While {card.Info.DisplayedNameLocalized} is on the board, remove ally Terrain and Pelt cards and transform the rest into random Apostles.";
                        AbilitiesUtil.GetInfo(DynamicAbilities[3]).rulebookDescription = "Kill WhiteNight and all Apostles on the board then deal 33 direct damage.";
                    }
                    if (TurnManager.Instance?.Opponent != null && TurnManager.Instance.Opponent is ApocalypseBossOpponent)
                    {
                        AbilitiesUtil.GetInfo(DynamicAbilities[4]).rulebookDescription = TurnManager.Instance.Opponent.NumLives switch
                        {
                            1 => "On combat's end, mark opposing spaces with coloured icons. This card strikes those spaces on its turns. Red: double this card's damage; White: halve this card's damage then heal it equal to its Power; Yellow; No special effect.",
                            _ => "Every 3 turns, switch the active egg effect and sigils. On the final phase, this sigil changes behaviour. Opponent cards may move at the end of the turn, and this card cannot go below 90/60/30 Health."
                        };
                        AbilitiesUtil.GetInfo(DynamicAbilities[5]).rulebookDescription = AbilitiesUtil.GetInfo(DynamicAbilities[5]).GetBaseRulebookDescription() + " Upon reaching 90/60/30 Health, permanently disable this effect then switch phase.";
                        AbilitiesUtil.GetInfo(DynamicAbilities[6]).rulebookDescription = AbilitiesUtil.GetInfo(DynamicAbilities[6]).GetBaseRulebookDescription() + " Upon reaching 90/60/30 Health, permanently disable this effect then switch phase.";
                        AbilitiesUtil.GetInfo(DynamicAbilities[7]).rulebookDescription = AbilitiesUtil.GetInfo(DynamicAbilities[7]).GetBaseRulebookDescription() + " Upon reaching 90/60/30 Health, permanently disable this effect then switch phase.";
                        changedRulebook = true;
                    }
                }
                return true;
            }
        }

        private static readonly List<Ability> DynamicAbilities = new()
        {
            Ability.DebuffEnemy,
            Apostle.ability,
            TrueSaviour.ability,
            Confession.ability,
            ApocalypseAbility.ability,
            BigEyes.ability,
            SmallBeak.ability,
            LongArms.ability
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
