using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class AbilityHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static AbilityManager.FullAbility New<T>(
            string pluginGuid, string abilityName,
            string rulebookName, string rulebookDescription,
            int powerLevel, bool foundInRulebook,
            string dialogue = null, string triggerText = null,
            bool canStack = false, bool modular = false, bool opponent = false)
            where T : AbilityBehaviour
        {
            Texture2D icon = TextureLoader.LoadTextureFromFile(abilityName + CardHelper._PNG);
            Texture2D pixel = TextureLoader.LoadTextureFromFile(abilityName + CardHelper._PIXEL);
            return New<T>(pluginGuid, rulebookName, rulebookDescription, icon, powerLevel, foundInRulebook, pixel, dialogue, triggerText, canStack, modular, opponent);
        }

        public static AbilityManager.FullAbility New<T>(
            string pluginGuid, string rulebookName, string rulebookDescription,
            Texture2D icon, int powerLevel, bool foundInRulebook,
            Texture2D pixelIcon = null, string dialogue = null, string triggerText = null,
            bool canStack = false, bool modular = false, bool opponent = false)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.SetBasic(rulebookName, rulebookDescription, dialogue, triggerText, powerLevel)
                .SetCanStack(canStack)
                .SetOpponentUsable(opponent);

            if (foundInRulebook) info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

            if (modular) info.AddMetaCategories(AbilityMetaCategory.Part1Modular);

            if (pixelIcon != null) info.SetPixelAbilityIcon(pixelIcon);

            return AbilityManager.Add(pluginGuid, info, typeof(T), icon);
        }

        public static AbilityManager.FullAbility NewActivated<T>(
            string pluginGuid, string abilityName, string rulebookName, string rulebookDescription,
            int powerLevel, bool foundInRulebook, string dialogue = null, string triggerText = null,
            bool canStack = false, bool modular = false, bool opponent = false)
            where T : AbilityBehaviour
        {
            AbilityManager.FullAbility full = New<T>(pluginGuid, abilityName, rulebookName, rulebookDescription, powerLevel, foundInRulebook, dialogue, triggerText, canStack, modular, opponent);
            full.Info.SetActivated();
            return full;
        }
        public static AbilityManager.FullAbility NewFiller<T>(
            string pluginGuid, string abilityName,
            string rulebookName, string rulebookDescription)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.SetPassive();
            return New<T>(pluginGuid, abilityName, rulebookName, rulebookDescription, 0, true);
        }
        public static AbilityManager.FullAbility NewFiller<T>(
            string pluginGuid, Texture2D icon,
            string rulebookName, string rulebookDescription)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.SetPassive();
            return New<T>(pluginGuid, rulebookName, rulebookDescription, icon, 0, true);
        }

        public static StatIconManager.FullStatIcon CreateStatIcon<T>(
            string pluginGuid,
            string abilityName,
            string name, string description,
            bool attack, bool health)
            where T : VariableStatBehaviour
        {
            StatIconInfo statIconInfo = ScriptableObject.CreateInstance<StatIconInfo>();
            statIconInfo.rulebookName = name;
            statIconInfo.rulebookDescription = description;
            statIconInfo.gbcDescription = description;
            statIconInfo.SetAppliesToStats(attack, health);
            statIconInfo.SetDefaultPart1Ability();

            Texture2D iconTex = TextureLoader.LoadTextureFromFile($"{abilityName}.png");
            Texture2D pixelTex = TextureLoader.LoadTextureFromFile($"{abilityName}_pixel.png");

            if (iconTex != null) statIconInfo.SetIcon(iconTex);

            if (pixelTex != null) statIconInfo.SetPixelIcon(pixelTex);

            return StatIconManager.Add(pluginGuid, statIconInfo, typeof(T));
        }

        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreateSpecialAbility<T>(string pluginGuid, string rulebookName)
            where T : SpecialCardBehaviour => SpecialTriggeredAbilityManager.Add(pluginGuid, rulebookName, typeof(T));

        private static AbilityInfo SetBasic(this AbilityInfo info, string name, string desc, string dialogue = null, string triggerText = null, int powerLevel = 0)
        {
            info.rulebookName = name;
            info.rulebookDescription = desc;
            info.SetAbilityLearnedDialogue(dialogue);
            Debug.Log($"Dialogue: {info.abilityLearnedDialogue != null}");
            info.triggerText = triggerText;
            info.powerLevel = powerLevel;
            return info;
        }

        public static AbilityManager.FullAbility SetPart3Rulebook(this AbilityManager.FullAbility full)
        {
            full.Info.AddMetaCategories(AbilityMetaCategory.Part3Rulebook);
            return full;
        }
        public static AbilityManager.FullAbility SetGrimoraRulebook(this AbilityManager.FullAbility full)
        {
            full.Info.AddMetaCategories(AbilityMetaCategory.GrimoraRulebook);
            return full;
        }
        public static AbilityManager.FullAbility SetMagnificusRulebook(this AbilityManager.FullAbility full)
        {
            full.Info.AddMetaCategories(AbilityMetaCategory.MagnificusRulebook);
            return full;
        }
    }
}
