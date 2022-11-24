﻿using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;

namespace WhistleWind.AbnormalSigils.Core.Helpers
{
    public static class AbnormalAbilityHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        [Flags]
        public enum AbilityGroup
        {
            None = 0,
            Normal = 1,
            Activated = 2,
            Special = 4,
            All
        }
        private static AbilityGroup ForceModular => AbnormalConfigManager.Instance.MakeModular;
        private static AbilityGroup ForceDisable => AbnormalConfigManager.Instance.DisableModular;
        public static AbilityManager.FullAbility CreateAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription,
            string dialogue, int powerLevel = 0,
            bool modular = false, bool special = false,
            bool opponent = false, bool canStack = false,
            bool isPassive = false, bool unobtainable = false,
            bool flipY = false, byte[] flipTexture = null)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            info.SetBasicInfo(rulebookName, rulebookDescription, dialogue, powerLevel);
            info.SetPixelAbilityIcon(AbnormalTextureLoader.LoadTextureFromBytes(gbcTexture));

            info.opponentUsable = opponent;
            info.canStack = canStack;
            info.passive = isPassive;
            info.flipYIfOpponent = flipY;

            info.CheckModularity(unobtainable, special, modular, AbilityGroup.Normal);

            AbilityManager.FullAbility ability = AbilityManager.Add(pluginGuid, info, typeof(T), AbnormalTextureLoader.LoadTextureFromBytes(texture));

            if (flipTexture != null)
                ability.SetCustomFlippedTexture(AbnormalTextureLoader.LoadTextureFromBytes(flipTexture));

            return ability;
        }
        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription, string dialogue, int powerLevel = 0,
            bool special = false, bool unobtainable = false)
            where T : BetterActivatedAbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            info.SetBasicInfo(rulebookName, rulebookDescription, dialogue, powerLevel);
            info.pixelIcon = AbnormalTextureLoader.LoadTextureFromBytes(gbcTexture).ConvertTexture();
            info.activated = true;

            info.CheckModularity(unobtainable, special, false, AbilityGroup.Activated);

            return AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromBytes(texture));
        }
        public static AbilityManager.FullAbility CreateRulebookAbility<T>(string rulebookName, string rulebookDescription)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            info.SetBasicInfo(rulebookName, rulebookDescription, "", 0);
            info.SetPixelAbilityIcon(AbnormalTextureLoader.LoadTextureFromBytes(Artwork.sigilAbnormality_pixel));

            return AbilityManager.Add(pluginGuid, info, typeof(T), AbnormalTextureLoader.LoadTextureFromBytes(Artwork.sigilAbnormality));
        }
        public static StatIconManager.FullStatIcon CreateStatIcon<T>(
            string name, string description,
            byte[] texture, byte[] pixelTexture, bool attack, bool health)
            where T : VariableStatBehaviour
        {
            StatIconInfo statIconInfo = ScriptableObject.CreateInstance<StatIconInfo>();
            statIconInfo.rulebookName = name;
            statIconInfo.rulebookDescription = description;
            statIconInfo.gbcDescription = description;
            statIconInfo.appliesToAttack = attack;
            statIconInfo.appliesToHealth = health;
            statIconInfo.iconGraphic = AbnormalTextureLoader.LoadTextureFromBytes(texture);
            statIconInfo.SetPixelIcon(AbnormalTextureLoader.LoadTextureFromBytes(pixelTexture));
            statIconInfo.SetDefaultPart1Ability();

            return StatIconManager.Add(pluginGuid, statIconInfo, typeof(T));
        }

        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreateSpecialAbility<T>(string rulebookName)
            where T : SpecialCardBehaviour => SpecialTriggeredAbilityManager.Add(pluginGuid, rulebookName, typeof(T));

        private static AbilityInfo CheckModularity(this AbilityInfo info, bool unobtainable, bool special, bool makeModular, AbilityGroup defaultGroup)
        {
            if (ForceDisable.HasFlag(AbilityGroup.All))
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
        private static AbilityInfo SetBasicInfo(this AbilityInfo info, string name, string desc, string dialogue, int powerLevel)
        {
            info.rulebookName = name;
            info.rulebookDescription = desc;
            info.abilityLearnedDialogue = SetAbilityInfoDialogue(dialogue);
            info.powerLevel = powerLevel;
            return info;
        }
        private static DialogueEvent.LineSet SetAbilityInfoDialogue(string dialogue) => new DialogueEvent.LineSet(new List<DialogueEvent.Line>() { new() { text = dialogue } });
    }
}
