using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using UnityEngine;

using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static InscryptionAPI.Card.AbilityManager;

namespace WhistleWind.AbnormalSigils.Core.Helpers
{
    public static class AbnormalAbilityHelper
    {
        public static FullAbility CreateAbility<T>(
            string abilityName,
            string rulebookName, string rulebookDescription,
            string dialogue = null, string triggerText = null,
            int powerLevel = 0,
            bool modular = false, bool special = false,
            bool opponent = false, bool canStack = false,
            bool unobtainable = false,
            bool flipY = false, string flipTextureName = null)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.CheckModularity(unobtainable, special, modular, AbilityGroup.Normal);

            return AbilityHelper.CreateAbility<T>(
                info, pluginGuid,
                abilityName,
                rulebookName, rulebookDescription,
                dialogue, triggerText,
                powerLevel,
                opponent, canStack, false, false, flipY, flipTextureName);
        }

        public static FullAbility CreateActivatedAbility<T>(
            string abilityName,
            string rulebookName, string rulebookDescription,
            string dialogue = null , string triggerText = null,
            int powerLevel = 0,
            bool special = false, bool unobtainable = false)
            where T : ExtendedActivatedAbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.CheckModularity(unobtainable, special, false, AbilityGroup.Activated);

            return AbilityHelper.CreateActivatedAbility<T>(
                info, pluginGuid,
                abilityName,
                rulebookName, rulebookDescription,
                dialogue, triggerText,
                powerLevel);
        }

        private static AbilityInfo CheckModularity(this AbilityInfo info, bool unobtainable, bool special, bool makeModular, AbilityGroup defaultGroup)
        {
            if (ForceDisable.HasFlag(AbilityGroup.All) ||
                ForceDisable.HasFlags(AbilityGroup.Normal, AbilityGroup.Activated, AbilityGroup.Special))
                return info;

            if (unobtainable)
                info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);
            else if (special && !ForceDisable.HasFlag(AbilityGroup.Special))
            {
                info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                if (ForceModular.HasFlag(AbilityGroup.All) || ForceModular.HasFlag(AbilityGroup.Special))
                    info.AddMetaCategories(AbilityMetaCategory.Part1Modular);
            }
            else if (!ForceDisable.HasFlag(defaultGroup))
            {
                info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                if (makeModular || ForceModular.HasFlag(AbilityGroup.All) || ForceModular.HasFlag(defaultGroup))
                    info.AddMetaCategories(AbilityMetaCategory.Part1Modular);
            }
            return info;
        }

        private static AbilityGroup ForceModular => AbnormalConfigManager.Instance.MakeModular;
        private static AbilityGroup ForceDisable => AbnormalConfigManager.Instance.DisableModular;

        [Flags]
        public enum AbilityGroup
        {
            None = 0,
            Normal = 1,
            Activated = 2,
            Special = 4,
            All = 8
        }
    }
}