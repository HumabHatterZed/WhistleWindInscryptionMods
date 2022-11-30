using WhistleWind.Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;

namespace WhistleWind.AbnormalSigils.Core.Helpers
{
    public static class AbnormalAbilityHelper
    {
        [Flags]
        public enum AbilityGroup
        {
            None = 0,
            Normal = 1,
            Activated = 2,
            Special = 4,
            All = 8
        }
        private static AbilityGroup ForceModular => AbnormalConfigManager.Instance.MakeModular;
        private static AbilityGroup ForceDisable => AbnormalConfigManager.Instance.DisableModular;
        public static AbilityManager.FullAbility CreateAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription,
            string dialogue, int powerLevel = 0,
            bool modular = false, bool special = false,
            bool opponent = false, bool canStack = false,
            bool unobtainable = false,
            bool flipY = false, byte[] flipTexture = null)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.CheckModularity(unobtainable, special, modular, AbilityGroup.Normal);

            return AbilityHelper.CreateAbility<T>(
                info, pluginGuid, texture, gbcTexture,
                rulebookName, rulebookDescription, dialogue, powerLevel,
                opponent, canStack, false, false, flipY, flipTexture);
        }
        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription, string dialogue, int powerLevel = 0,
            bool special = false, bool unobtainable = false)
            where T : BetterActivatedAbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.CheckModularity(unobtainable, special, false, AbilityGroup.Activated);

            return AbilityHelper.CreateActivatedAbility<T>(
                info, pluginGuid, texture, gbcTexture,
                rulebookName, rulebookDescription, dialogue, powerLevel);
        }
        public static AbilityManager.FullAbility CreateRulebookAbility<T>(string rulebookName, string rulebookDescription)
            where T : AbilityBehaviour
        {
            return AbilityHelper.CreateFillerAbility<T>(
                pluginGuid, rulebookName, rulebookDescription,
                Artwork.sigilAbnormality, Artwork.sigilAbnormality_pixel);
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
        private static bool HasFlags(this AbilityGroup config, params AbilityGroup[] flags)
        {
            foreach (AbilityGroup flag in flags)
            {
                if (!config.HasFlag(flag))
                    return false;
            }
            return true;
        }
    }
}