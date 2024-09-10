using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.RuleBook;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils.Core
{
    [HarmonyPatch]
    internal class StatusEffectPages
    {
        internal static void AddStatusEntries()
        {
            RuleBookManager.New(
                modGuid: AbnormalPlugin.pluginGuid,
                pageType: PageRangeType.Abilities,
                subsectionName: "Status Effects",
                getInsertPositionFunc: GetInsertPosition,
                createPagesFunc: CreatePages);
        }

        private static int GetInsertPosition(PageRangeInfo pageRangeInfo, List<RuleBookPageInfo> pages)
        {
            return pages.FindLastIndex(rbi => rbi.pagePrefab == pageRangeInfo.rangePrefab) + 1;
        }
        private static List<RuleBookPageInfo> CreatePages(RuleBookInfo instance, PageRangeInfo currentRange, AbilityMetaCategory metaCategory)
        {
            List<RuleBookPageInfo> retval = new();
            List<StatusEffectManager.FullStatusEffect> statuses = StatusEffectManager.AllStatusEffects.Where(
                x => x.IconInfo.metaCategories.Contains(metaCategory)
                ).ToList();

            foreach (StatusEffectManager.FullStatusEffect statusEffect in statuses)
            {
                RuleBookPageInfo page = new();
                instance.FillAbilityPage(page, currentRange, (int)statusEffect.IconInfo.ability);
                retval.Add(page);
            }

            RuleBookPageInfo page3 = new();
            instance.FillAbilityPage(page3, currentRange, (int)SeeMore.ability);
            retval.Add(page3);

            return retval;
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

        [HarmonyPrefix, HarmonyPatch(typeof(RuleBookInfo), nameof(RuleBookInfo.AbilityShouldBeAdded))]
        private static bool StatusShouldBeAddedRegularly(int abilityIndex, AbilityMetaCategory rulebookCategory, ref bool __result)
        {
            if (StatusEffectManager.AllStatusEffects.EffectByIcon((Ability)abilityIndex)?.SigilRulebookEntry == false)
            {
                return __result = false;
            }
            return true;
        }
    }
}
