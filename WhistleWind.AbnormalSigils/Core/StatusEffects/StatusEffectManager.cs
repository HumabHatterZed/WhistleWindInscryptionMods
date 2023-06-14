using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using UnityEngine;

using WhistleWind.Core.Helpers;
using static InscryptionAPI.Card.AbilityManager;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static InscryptionAPI.Card.SpecialTriggeredAbilityManager;
using InscryptionAPI.Guid;
using HarmonyLib;

namespace WhistleWind.AbnormalSigils.Core
{
    public static class StatusEffectManager
    {
        public static readonly List<AbilityInfo> StatusAbilityInfos = new();
        public static List<Ability> StatusAbilities => StatusAbilityInfos.ConvertAll(x => x.ability);

        public static readonly Dictionary<bool, Dictionary<FullSpecialTriggeredAbility, FullAbility>> StatusEffects = new()
        {
            { true, new() },
            { false, new() }
        };

        public static bool IsStatusMod(this CardModificationInfo mod, bool positiveMod)
        {
            return mod.singletonId?.StartsWith(positiveMod ? "status_effect" : "bad_status_effect") ?? false;
        }
        public static CardModificationInfo StatusMod(string name, bool positiveEffect, bool inheritable = false)
        {
            return new()
            {
                singletonId = (positiveEffect ? "" : "bad_") + "status_effect_" + name,
                nonCopyable = !inheritable
            };
        }
        public static void StatusEffect<T, U>(
            ref Ability statusAbility, ref SpecialTriggeredAbility statusSpecial,
            string guid, string iconTexture, string name, string description,
            bool positiveEffect = false, IconColour iconColour = IconColour.Default,
            params AbilityMetaCategory[] metaCategories)
            where T : AbilityBehaviour
            where U : SpecialCardBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = name;
            info.rulebookDescription = description;
            info.powerLevel = positiveEffect ? 0 : -1;
            info.SetPixelAbilityIcon(TextureLoader.LoadTextureFromFile($"{iconTexture}_pixel"))
                .SetCanStack().SetPassive()
                .SetExtendedProperty("wstl:StatusEffect", iconColour switch
                {
                    IconColour.Red => "red",
                    IconColour.Brown => "brown",
                    IconColour.Green => "green",
                    _ => "black"
                });
            if (metaCategories.Length > 0)
                info.AddMetaCategories(metaCategories);
            
            FullAbility fullAbility = Add(guid, info, typeof(T), TextureLoader.LoadTextureFromFile($"{iconTexture}"));
            FullSpecialTriggeredAbility special = Add(guid, name, typeof(U));

            StatusAbilityInfos.Add(fullAbility.Info);
            StatusEffects[positiveEffect].Add(special, fullAbility);

            statusAbility = fullAbility.Id;
            statusSpecial = special.Id;
        }

        public static AbilityMetaCategory Part1StatusEffect = GuidManager.GetEnumValue<AbilityMetaCategory>(pluginGuid, "Part1StatusEffect");
        public static AbilityMetaCategory Part3StatusEffect = GuidManager.GetEnumValue<AbilityMetaCategory>(pluginGuid, "Part3StatusEffect");
        public enum IconColour
        {
            Default,
            Green,
            Brown,
            Red
        }
    }

    [HarmonyPatch]
    internal static class RulebookStatusEffects
    {
        [HarmonyPatch(typeof(RuleBookInfo), "ConstructPageData", new Type[] { typeof(AbilityMetaCategory) })]
        [HarmonyPostfix]
        private static void StatusEffectsRulebook(RuleBookInfo __instance, ref List<RuleBookPageInfo> __result)
        {
            if (StatusEffectManager.StatusAbilityInfos.Count <= 0)
                return;

            foreach (PageRangeInfo pageRangeInfo in __instance.pageRanges)
            {
                // regular abilities
                if (pageRangeInfo.type != PageRangeType.Abilities)
                    continue;

                int insertPosition = __result.FindLastIndex(rbi => rbi.pagePrefab == pageRangeInfo.rangePrefab) + 1;
                int curPageNum = 1; // name displayed in the header
                List<AbilityInfo> abilitiesToAdd = new(StatusEffectManager.StatusAbilityInfos);

                if (SaveManager.SaveFile.IsPart1)
                    abilitiesToAdd.RemoveAll(x => !x.metaCategories.Contains(StatusEffectManager.Part1StatusEffect));
                else
                    abilitiesToAdd.RemoveAll(x => !x.metaCategories.Contains(StatusEffectManager.Part3StatusEffect));

                foreach (Ability ab in abilitiesToAdd.ConvertAll(x => x.ability))
                {
                    RuleBookPageInfo info = new()
                    {
                        pagePrefab = pageRangeInfo.rangePrefab,
                        headerText = string.Format(Localization.Translate("APPENDIX XII, SUBSECTION II - STATUS EFFECTS {0}"), curPageNum)
                    };
                    __instance.FillAbilityPage(info, pageRangeInfo, (int)ab);
                    __result.Insert(insertPosition, info);
                    curPageNum++;
                    insertPosition++;
                }
            }
        }
    }
}