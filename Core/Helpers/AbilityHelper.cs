using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class AbilityHelper // Base code taken from GrimoraMod and SigilADay_julienperge
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
        public static AbilityGroup _forceModular;
        public static AbilityGroup _forceDisable;
        private static AbilityGroup ForceModular => _forceDisable;
        private static AbilityGroup ForceDisable => _forceDisable;
        public static AbilityManager.FullAbility CreateAbility<T>(
            string pluginGuid, byte[] texture, byte[] gbcTexture,
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
            info.SetPixelAbilityIcon(TextureLoader.LoadTextureFromBytes(gbcTexture));

            info.opponentUsable = opponent;
            info.canStack = canStack;
            info.passive = isPassive;
            info.flipYIfOpponent = flipY;

            info.CheckModularity(unobtainable, special, modular, AbilityGroup.Normal);

            AbilityManager.FullAbility ability = AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromBytes(texture));

            if (flipTexture != null)
                ability.SetCustomFlippedTexture(TextureLoader.LoadTextureFromBytes(flipTexture));

            return ability;
        }
        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
            string pluginGuid, byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription, string dialogue, int powerLevel = 0,
            bool special = false, bool unobtainable = false)
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            info.SetBasicInfo(rulebookName, rulebookDescription, dialogue, powerLevel);
            info.pixelIcon = TextureLoader.LoadTextureFromBytes(gbcTexture).ConvertTexture();
            info.activated = true;

            info.CheckModularity(unobtainable, special, false, AbilityGroup.Activated);

            return AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromBytes(texture));
        }
        public static AbilityManager.FullAbility CreateRulebookAbility<T>(string pluginGuid, string rulebookName, string rulebookDescription, byte[] defaultArt)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            info.SetBasicInfo(rulebookName, rulebookDescription, "", 0);
            info.SetPixelAbilityIcon(TextureLoader.LoadTextureFromBytes(defaultArt));

            return AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromBytes(defaultArt));
        }
        public static StatIconManager.FullStatIcon CreateStatIcon<T>(
            string pluginGuid, string name, string description,
            byte[] texture, byte[] pixelTexture, bool attack, bool health)
            where T : VariableStatBehaviour
        {
            StatIconInfo statIconInfo = ScriptableObject.CreateInstance<StatIconInfo>();
            statIconInfo.rulebookName = name;
            statIconInfo.rulebookDescription = description;
            statIconInfo.gbcDescription = description;
            statIconInfo.appliesToAttack = attack;
            statIconInfo.appliesToHealth = health;
            statIconInfo.iconGraphic = TextureLoader.LoadTextureFromBytes(texture);
            statIconInfo.SetPixelIcon(TextureLoader.LoadTextureFromBytes(pixelTexture));
            statIconInfo.SetDefaultPart1Ability();

            return StatIconManager.Add(pluginGuid, statIconInfo, typeof(T));
        }

        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreateSpecialAbility<T>(string pluginGuid, string rulebookName)
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
