using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using static InscryptionAPI.Card.AbilityManager;
using static InscryptionAPI.Card.StatIconManager;

namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch]
    internal class RulebookPatches
    {
/*        [HarmonyPatch(typeof(RuleBookController))]
        private static class RulebookControllerPatches
        {
            private static bool changedRulebook = false;
            // Reset the descriptions of WhiteNight-related abilities
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.SetShown))]
            public static bool ResetAlteredDescriptions(bool shown)
            {
                if (!shown && changedRulebook)
                {
                    for (int i = 0; i < DynamicDescs.Count; i++)
                        AbilitiesUtil.GetInfo(DynamicDescs[i]).ResetDescription();

                    changedRulebook = false;
                }

                return true;
            }
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.OpenToAbilityPage))]
            private static bool OpenToAbilityPage(PlayableCard card)
            {
                if (card != null)
                {
                    if (card.GetDisplayedStatusEffects(false).Count > 5)
                    {
                        List<Ability> abilities = card.GetDisplayedStatusEffects(false);
                        abilities.Sort((a, b) => Mathf.Abs(AbilitiesUtil.GetInfo(b).powerLevel) - Mathf.Abs(AbilitiesUtil.GetInfo(a).powerLevel));
                        abilities.RemoveRange(0, 4);
                        AbilitiesUtil.GetInfo(DynamicDescs[1]).rulebookDescription = StatusOverflow(card, abilities);
                        changedRulebook = true;
                    }
                }
                return true;
            }
        }*/


        [HarmonyPrefix, HarmonyPatch(typeof(RuleBookInfo), nameof(RuleBookInfo.AbilityShouldBeAdded))]
        private static bool StatusShouldBeAddedRegularly(int abilityIndex, AbilityMetaCategory rulebookCategory, ref bool __result)
        {
            if ((Ability)abilityIndex == SeeMore.ability)
            {
                __result = false;
                return false;
            }

            if (StatusEffectManager.AllStatusEffects.EffectByIcon((Ability)abilityIndex)?.SigilRulebookEntry == false)
            {
                __result = false;
                return false;
            }

            return true;
        }

        [HarmonyPatch(typeof(RuleBookInfo), "ConstructPageData", new Type[] { typeof(AbilityMetaCategory) })]
        [HarmonyPostfix]
        private static void FixRulebook(AbilityMetaCategory metaCategory, RuleBookInfo __instance, ref List<RuleBookPageInfo> __result)
        {
            //List<FullAbility> newAbilities = AllAbilities.FindAll(x => !BaseGameAbilities.Contains(x));
            if (StatusEffectManager.AllStatusEffects.Count == 0)
                return;

            foreach (PageRangeInfo pageRangeInfo in __instance.pageRanges.Where(x => x.type == PageRangeType.Abilities))
            {
                int curPageNum = 1;
                int insertPosition = __result.FindLastIndex(rbi => rbi.pagePrefab == pageRangeInfo.rangePrefab) + 1;
                List<StatusEffectManager.FullStatusEffect> statuses = StatusEffectManager.AllStatusEffects.Where(x => x.IconInfo.metaCategories.Contains(metaCategory)).ToList();
/*                if (SaveManager.SaveFile.IsPart1)
                    statuses.RemoveAll(x => !x.IconInfo.metaCategories.Contains(AbilityMetaCategory.Part1Rulebook));

                else if (SaveManager.SaveFile.IsPart3)
                    statuses.RemoveAll(x => !x.IconInfo.metaCategories.Contains(AbilityMetaCategory.Part3Rulebook));

                else if (SaveManager.SaveFile.IsGrimora)
                    statuses.RemoveAll(x => !x.IconInfo.metaCategories.Contains(AbilityMetaCategory.GrimoraRulebook));

                else if (SaveManager.SaveFile.IsMagnificus)
                    statuses.RemoveAll(x => !x.IconInfo.metaCategories.Contains(AbilityMetaCategory.MagnificusRulebook));
*/
                foreach (StatusEffectManager.FullStatusEffect status in statuses)
                {
                    RuleBookPageInfo info = new()
                    {
                        pagePrefab = pageRangeInfo.rangePrefab,
                        headerText = string.Format(Localization.Translate("APPENDIX XII, SUBSECTION C - STATUS EFFECTS {0}"), curPageNum)
                    };
                    __instance.FillAbilityPage(info, pageRangeInfo, (int)status.IconInfo.ability);
                    __result.Insert(insertPosition, info);
                    curPageNum += 1;
                    insertPosition += 1;
                }

                RuleBookPageInfo seeMore = new()
                {
                    pagePrefab = pageRangeInfo.rangePrefab,
                    headerText = string.Format(Localization.Translate("APPENDIX XII, SUBSECTION C - STATUS EFFECTS {0}"), curPageNum)
                };
                __instance.FillAbilityPage(seeMore, pageRangeInfo, (int)SeeMore.ability);
                __result.Insert(insertPosition, seeMore);
            }
        }
        private static string StatusOverflow(PlayableCard card, List<Ability> distinct)
        {
            StringBuilder sb = new();
            List<Ability> abilities = card.GetDisplayedStatusEffects(true);
            for (int i = 0; i < distinct.Count; i++)
            {
                AbilityInfo info = AbilitiesUtil.GetInfo(distinct[i]);

                sb.Append(info.rulebookName + ":" + abilities.Count(x => x == info.ability));

                if (i < distinct.Count - 1)
                    sb.Append(" | ");
            }

            return sb.ToString();
        }

        private static readonly List<Ability> DynamicDescs = new()
        {
            Conductor.ability,
            SeeMore.ability
        };
        private static readonly List<string> ConductorDescriptions = new()
        {
            "Adjacent creatures gain Power equal to half this card's base Power, rounded down. This effect changes next turn.",
            "Allied creatures on the board gain Power equal to half this card's base Power, rounded down. This effect changes next turn.",
            "All other creatures on the board gain Power equal to this card's Power."
        };
    }
}
