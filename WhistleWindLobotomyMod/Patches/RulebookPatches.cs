using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.Text;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;
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

                    if (card.HasTrait(LobotomyCardManager.Apostle))
                    {
                        changedRulebook = true;
                        AbilitiesUtil.GetInfo(DynamicAbilities[1]).rulebookDescription = "[creature] will enter a downed state instead of dying. Downed creatures are invulnerable under special conditions.";
                        AbilitiesUtil.GetInfo(DynamicAbilities[2]).rulebookDescription = $"While {card.Info.DisplayedNameLocalized} is on the board, remove ally Terrain and Pelt cards and transform the rest into random Apostles.";
                        AbilitiesUtil.GetInfo(DynamicAbilities[3]).rulebookDescription = "Kill WhiteNight and all Apostles on the board then deal 33 direct damage.";
                    }
                    if (CustomOpponentUtils.FightingCustomOpponent(true))
                    {
                        if (CustomOpponentUtils.IsCustomBoss<ApocalypseBossOpponent>())
                        {
                            changedRulebook = true;
                            AbilitiesUtil.GetInfo(DynamicAbilities[4]).rulebookDescription = TurnManager.Instance.Opponent.NumLives switch
                            {
                                1 => "At the end of combat, this card will mark random spaces with coloured targets, then attack those spaces on its next turn. Red: this card's damage is doubled; White: this card's damage is halved and it heals equal to its Power; Yellow: no special effect.",
                                _ => "Every 3 turns, change the active egg effect. On the final phase, this sigil changes behaviour. Opponent cards may move at the end of the turn. This card cannot go below 90/60/30 Health."
                            };
                            AppendToBaseDescription(5, GetApocalypseThresholds());
                            AppendToBaseDescription(6, GetApocalypseThresholds());
                            AppendToBaseDescription(7, GetApocalypseThresholds());

                            if (TurnManager.Instance.Opponent.NumLives == 1)
                                AbilitiesUtil.GetInfo(DynamicAbilities[8]).rulebookDescription =
                                    "At the end of the owner's turn, deal direct damage to the owner proportional to how much damage this card received during the turn.";
                        }
                    }
                }
                return true;
            }
        }

        private static void AppendToBaseDescription(int dynamicIndex, string stringToAppend)
        {
            AbilityInfo info = AbilitiesUtil.GetInfo(DynamicAbilities[dynamicIndex]);
            info.rulebookDescription = info.GetBaseRulebookDescription() + stringToAppend;
        }
        private static string GetApocalypseThresholds()
        {
            if (ApocalypseThresholdString != null)
                return ApocalypseThresholdString;

            return " Upon reaching 80/60/40 Health, permanently disable this effect then switch phase.";
/*            StringBuilder builder = new(" Upon reaching ");
            ApocalypseBattleSequencer sequence = CustomBossUtils.AsCustomBoss<ApocalypseBossOpponent>().BattleSequence;
            builder.Append(sequence.BossHealthThreshold(4))
                .Append("/").Append(sequence.BossHealthThreshold(3))
                .Append("/").Append(sequence.BossHealthThreshold(2))
                .Append(" Health, permanently disable this effect then switch phase.");

            return ApocalypseThresholdString = builder.ToString();*/
        }

        internal static string ApocalypseThresholdString = null;
        private static readonly List<Ability> DynamicAbilities = new()
        {
            Ability.DebuffEnemy,
            ApostleSigil.ability,
            TrueSaviour.ability,
            Confession.ability,
            ApocalypseAbility.ability,
            BigEyes.ability,
            SmallBeak.ability,
            LongArms.ability,
            UnjustScale.ability
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
            "StrafeSwap",
            "Transformer"
        };
    }
}
