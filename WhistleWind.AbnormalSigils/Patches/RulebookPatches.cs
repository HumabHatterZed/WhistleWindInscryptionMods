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

namespace WhistleWind.AbnormalSigils.Patches
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
                    if (card.HasAbility(Conductor.ability))
                    {
                        var component = card.GetComponent<Conductor>();
                        if (!component || component.turnCount == 0)
                            return true;

                        AbilitiesUtil.GetInfo(DynamicDescs[0]).rulebookDescription = ConductorDescriptions[Mathf.Min(2, component.turnCount - 1)];
                        changedRulebook = true;
                    }
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
        }


        [HarmonyPatch(typeof(RuleBookInfo), nameof(RuleBookInfo.AbilityShouldBeAdded))]
        [HarmonyPostfix]
        private static void StatusShouldBeAddedRegularly(int abilityIndex, AbilityMetaCategory rulebookCategory, ref bool __result)
        {
            if ((Ability)abilityIndex == SeeMore.ability)
                __result = false;

            if (StatusEffectManager.AllStatusEffects.Exists(x => x.IconId == (Ability)abilityIndex && !x.AddNormalRulebookEntry))
                __result = false;
        }
        [HarmonyPatch(typeof(RuleBookInfo), "ConstructPageData", new Type[] { typeof(AbilityMetaCategory) })]
        [HarmonyPostfix]
        private static void FixRulebook(AbilityMetaCategory metaCategory, RuleBookInfo __instance, ref List<RuleBookPageInfo> __result)
        {
            List<FullAbility> newAbilities = AllAbilities.FindAll(x => !BaseGameAbilities.Contains(x));
            if (newAbilities.Count <= 0)
                return;

            foreach (PageRangeInfo pageRangeInfo in __instance.pageRanges)
            {
                // regular abilities
                if (pageRangeInfo.type != PageRangeType.Abilities)
                    continue;

                int insertPosition = __result.FindLastIndex(rbi => rbi.pagePrefab == pageRangeInfo.rangePrefab) + 1;
                int curPageNum = (int)Ability.NUM_ABILITIES;
                List<FullAbility> addedAbilities = newAbilities.Where(x => __instance.AbilityShouldBeAdded((int)x.Id, metaCategory)).ToList();
                curPageNum += addedAbilities.Count + 1;

                List<StatusEffectManager.FullStatusEffect> statuses = new(StatusEffectManager.AllStatusEffects);
                if (SaveManager.SaveFile.IsPart1)
                    statuses.RemoveAll(x => !x.statusMetaCategories.Contains(StatusEffectManager.StatusMetaCategory.Part1StatusEffect));

                else if (SaveManager.SaveFile.IsPart3)
                    statuses.RemoveAll(x => !x.statusMetaCategories.Contains(StatusEffectManager.StatusMetaCategory.Part3StatusEffect));

                else if (SaveManager.SaveFile.IsGrimora)
                    statuses.RemoveAll(x => !x.statusMetaCategories.Contains(StatusEffectManager.StatusMetaCategory.GrimoraStatusEffect));

                else if (SaveManager.SaveFile.IsMagnificus)
                    statuses.RemoveAll(x => !x.statusMetaCategories.Contains(StatusEffectManager.StatusMetaCategory.MagnificusStatusEffect));

                foreach (var status in statuses)
                {
                    RuleBookPageInfo info = new()
                    {
                        pagePrefab = pageRangeInfo.rangePrefab,
                        headerText = string.Format(Localization.Translate("APPENDIX XII, SUBSECTION II - STATUS EFFECTS {0}"), curPageNum)
                    };
                    __instance.FillAbilityPage(info, pageRangeInfo, (int)status.IconId);
                    __result.Insert(insertPosition, info);
                    curPageNum += 1;
                    insertPosition += 1;
                }

                RuleBookPageInfo seeMore = new()
                {
                    pagePrefab = pageRangeInfo.rangePrefab,
                    headerText = string.Format(Localization.Translate("APPENDIX XII, SUBSECTION II - STATUS EFFECTS {0}"), curPageNum)
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
            "Adjacent creatures gain Power equal to half this card's Power, rounded down. This effect changes next turn.",
            "Allied creatures gain Power equal to half this card's Power, rounded down. This effect changes next turn.",
            "All other creatures gain Power equal to this card's Power."
        };
    }
}
