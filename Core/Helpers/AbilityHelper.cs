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
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.SetBasic(rulebookName, rulebookDescription, dialogue, triggerText, powerLevel)
                .SetCanStack(canStack)
                .SetOpponentUsable(opponent);

            if (foundInRulebook) info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

            if (modular) info.AddMetaCategories(AbilityMetaCategory.Part1Modular);

            Texture2D pixel = TextureLoader.LoadTextureFromFile(abilityName + CardHelper._PIXEL);
            if (pixel != null)
                info.SetPixelAbilityIcon(pixel);

            return AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromFile(abilityName + CardHelper._PNG));
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
        /*        public static AbilityManager.FullAbility CreateAbility<T>(
                    string pluginGuid,
                    string abilityName,
                    string rulebookName, string rulebookDescription,
                    string dialogue = null, string triggerText = null,
                    int powerLevel = 0,
                    bool opponent = false,
                    bool canStack = false,
                    bool modular = false,
                    bool foundInRulebook = false,
                    bool flipY = false, string flipTextureName = null)
                    where T : AbilityBehaviour
                {
                    AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
                    return CreateAbility<T>(
                        info, pluginGuid,
                        abilityName,
                        rulebookName, rulebookDescription,
                        dialogue, triggerText,
                        powerLevel,
                        opponent, canStack,
                        modular, foundInRulebook,
                        flipY, flipTextureName);
                }*/
        /*        public static AbilityManager.FullAbility CreateAbility<T>(
                    AbilityInfo info,
                    string pluginGuid,
                    string abilityName,
                    string rulebookName, string rulebookDescription,
                    string dialogue = null, string triggerText = null,
                    int powerLevel = 0,
                    bool opponent = false,
                    bool canStack = false,
                    bool modular = false,
                    bool foundInRulebook = false,
                    bool flipY = false, string flipTextureName = null)
                    where T : AbilityBehaviour
                {
                    info.SetBasicInfo(rulebookName, rulebookDescription, dialogue, triggerText, powerLevel)
                        .SetOpponentUsable(opponent)
                        .SetCanStack(canStack, canStack);

                    info.flipYIfOpponent = flipY;

                    Texture2D pixelTex = TextureLoader.LoadTextureFromFile($"{abilityName}_pixel.png");
                    if (pixelTex != null)
                        info.SetPixelAbilityIcon(pixelTex);

                    if (foundInRulebook)
                        info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                    if (modular)
                        info.AddMetaCategories(AbilityMetaCategory.Part1Modular);

                    AbilityManager.FullAbility ability = AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromFile(abilityName + ".png"));

                    if (flipTextureName != null)
                        ability.SetCustomFlippedTexture(TextureLoader.LoadTextureFromFile($"{flipTextureName}"));

                    return ability;
                }*/
        /*        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
                    string pluginGuid,
                    string abilityName,
                    string rulebookName, string rulebookDescription,
                    string dialogue = null, string triggerText = null,
                    int powerLevel = 0)
                {
                    AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
                    return CreateActivatedAbility<T>(
                        info, pluginGuid,
                        abilityName,
                        rulebookName, rulebookDescription,
                        dialogue, triggerText,
                        powerLevel);
                }*/
        /*        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
                    AbilityInfo info, string pluginGuid,
                    string abilityName,
                    string rulebookName, string rulebookDescription,
                    string dialogue = null, string triggerText = null,
                    int powerLevel = 0) where T : AbilityBehaviour
                {
                    AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
                    info.SetActivated();

                    return AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromFile($"{abilityName}.png"));
                }*/

        /*        public static AbilityManager.FullAbility CreateFillerAbility<T>(
                    AbilityInfo info,
                    string pluginGuid,
                    string abilityName,
                    string rulebookName, string rulebookDescription)
                    where T : AbilityBehaviour
                {
                    info.SetBasicInfo(rulebookName, rulebookDescription);
                    info.SetPixelAbilityIcon(TextureLoader.LoadTextureFromFile($"{abilityName}_pixel.png"));
                    info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);
                    info.SetPassive();

                    return AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromFile($"{abilityName}.png"));
                }*/
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
            statIconInfo.appliesToAttack = attack;
            statIconInfo.appliesToHealth = health;
            statIconInfo.SetDefaultPart1Ability();

            Texture2D iconTex = TextureLoader.LoadTextureFromFile($"{abilityName}.png");
            Texture2D pixelTex = TextureLoader.LoadTextureFromFile($"{abilityName}_pixel.png");

            if (iconTex != null)
                statIconInfo.SetIcon(iconTex);

            if (pixelTex != null)
                statIconInfo.SetPixelIcon(pixelTex);

            return StatIconManager.Add(pluginGuid, statIconInfo, typeof(T));
        }

        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreateSpecialAbility<T>(string pluginGuid, string rulebookName)
            where T : SpecialCardBehaviour => SpecialTriggeredAbilityManager.Add(pluginGuid, rulebookName, typeof(T));

        private static AbilityInfo SetBasic(this AbilityInfo info, string name, string desc, string dialogue = null, string triggerText = null, int powerLevel = 0)
        {
            info.rulebookName = name;
            info.rulebookDescription = desc;
            info.SetAbilityLearnedDialogue(dialogue);
            info.triggerText = triggerText;
            info.powerLevel = powerLevel;
            return info;
        }
    }
}
