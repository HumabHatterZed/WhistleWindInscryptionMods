using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class AbilityHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static AbilityManager.FullAbility CreateAbility<T>(
            string pluginGuid,
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription,
            string dialogue, int powerLevel = 0,
            bool opponent = false, bool canStack = false,
            bool modular = false, bool foundInRulebook = true,
            bool flipY = false, byte[] flipTexture = null)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            return CreateAbility<T>(
                info, pluginGuid, texture, gbcTexture,
                rulebookName, rulebookDescription, dialogue, powerLevel,
                opponent, canStack, modular, foundInRulebook, flipY, flipTexture);
        }
        public static AbilityManager.FullAbility CreateAbility<T>(
            AbilityInfo info, string pluginGuid,
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription,
            string dialogue, int powerLevel = 0,
            bool opponent = false, bool canStack = false,
            bool modular = false, bool foundInRulebook = false,
            bool flipY = false, byte[] flipTexture = null)
            where T : AbilityBehaviour
        {
            info.SetBasicInfo(rulebookName, rulebookDescription, dialogue, powerLevel);
            info.SetPixelAbilityIcon(TextureLoader.LoadTextureFromBytes(gbcTexture));

            info.SetOpponentUsable(opponent);
            info.SetCanStack(canStack, canStack);

            info.flipYIfOpponent = flipY;

            if (foundInRulebook)
                info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

            if (modular)
                info.AddMetaCategories(AbilityMetaCategory.Part1Modular);

            AbilityManager.FullAbility ability = AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromBytes(texture));

            if (flipTexture != null)
                ability.SetCustomFlippedTexture(TextureLoader.LoadTextureFromBytes(flipTexture));

            return ability;
        }
        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
            string pluginGuid, byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription, string dialogue, int powerLevel = 0)
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            return CreateActivatedAbility<T>(
                info, pluginGuid, texture, gbcTexture,
                rulebookName, rulebookDescription, dialogue, powerLevel);
        }
        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
            AbilityInfo info, string pluginGuid, byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription, string dialogue, int powerLevel = 0)
        {
            info.SetBasicInfo(rulebookName, rulebookDescription, dialogue, powerLevel);
            info.pixelIcon = TextureLoader.LoadTextureFromBytes(gbcTexture).ConvertTexture();
            info.activated = true;

            return AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromBytes(texture));
        }
        public static AbilityManager.FullAbility CreateFillerAbility<T>(
            string pluginGuid,
            string rulebookName, string rulebookDescription,
            byte[] fillerArtBytes, byte[] fillerPixelArtBytes)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            return CreateFillerAbility<T>(
                info, pluginGuid, rulebookName, rulebookDescription, fillerArtBytes, fillerPixelArtBytes);
        }
        public static AbilityManager.FullAbility CreateFillerAbility<T>(
            AbilityInfo info, string pluginGuid,
            string rulebookName, string rulebookDescription,
            byte[] fillerArtBytes, byte[] fillerPixelArtBytes)
            where T : AbilityBehaviour
        {
            info.SetBasicInfo(rulebookName, rulebookDescription, "", 0);
            info.SetPixelAbilityIcon(TextureLoader.LoadTextureFromBytes(fillerPixelArtBytes));
            info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);
            info.SetPassive();

            return AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromBytes(fillerArtBytes));
        }
        public static StatIconManager.FullStatIcon CreateStatIcon<T>(
            string pluginGuid,
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
            statIconInfo.SetIcon(TextureLoader.LoadTextureFromBytes(texture));
            statIconInfo.SetPixelIcon(TextureLoader.LoadTextureFromBytes(pixelTexture));
            statIconInfo.SetDefaultPart1Ability();

            return StatIconManager.Add(pluginGuid, statIconInfo, typeof(T));
        }
        public static StatIconManager.FullStatIcon CreateStatIcon<T>(
            string pluginGuid,
            string name, string description,
            Texture2D texture, Texture2D pixelTexture, bool attack, bool health)
            where T : VariableStatBehaviour
        {
            StatIconInfo statIconInfo = ScriptableObject.CreateInstance<StatIconInfo>();
            statIconInfo.rulebookName = name;
            statIconInfo.rulebookDescription = description;
            statIconInfo.gbcDescription = description;
            statIconInfo.appliesToAttack = attack;
            statIconInfo.appliesToHealth = health;
            statIconInfo.SetIcon(texture);
            statIconInfo.SetPixelIcon(pixelTexture);
            statIconInfo.SetDefaultPart1Ability();

            return StatIconManager.Add(pluginGuid, statIconInfo, typeof(T));
        }

        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreateSpecialAbility<T>(string pluginGuid, string rulebookName)
            where T : SpecialCardBehaviour => SpecialTriggeredAbilityManager.Add(pluginGuid, rulebookName, typeof(T));

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
