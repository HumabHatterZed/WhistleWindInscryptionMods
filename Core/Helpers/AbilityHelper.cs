using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using DiskCardGame;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static WhistleWindLobotomyMod.WstlPlugin;

namespace WhistleWindLobotomyMod.Core.Helpers
{
    public static class AbilityHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        // Ability
        public static AbilityManager.FullAbility CreateAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription, string dialogue, int powerLevel = 0,
            bool addModular = false, bool opponent = false, bool canStack = false, bool isPassive = false,
            bool overrideModular = false, bool flipY = false, byte[] flipTexture = null)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            info.SetPixelAbilityIcon(WstlTextureHelper.LoadTextureFromResource(gbcTexture));

            info.rulebookName = rulebookName;
            info.rulebookDescription = rulebookDescription;
            info.powerLevel = powerLevel;
            info.abilityLearnedDialogue = SetAbilityInfoDialogue(dialogue);
            
            info.opponentUsable = opponent;
            info.canStack = canStack;
            info.passive = isPassive;

            info.flipYIfOpponent = flipY;

            info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

            if (!overrideModular && (addModular || ConfigManager.Instance.AllModular))
                info.AddMetaCategories(AbilityMetaCategory.Part1Modular);

            AbilityManager.FullAbility ability = AbilityManager.Add(pluginGuid, info, typeof(T), WstlTextureHelper.LoadTextureFromResource(texture));

            if (flipTexture != null)
                ability.SetCustomFlippedTexture(WstlTextureHelper.LoadTextureFromResource(flipTexture));

            return ability;
        }
        // Activated Ability
        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription, string dialogue, int powerLevel = 0,
            bool overrideModular = false)
            where T : ActivatedAbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            info.pixelIcon = WstlTextureHelper.LoadTextureFromResource(gbcTexture).ConvertTexture();

            info.rulebookName = rulebookName;
            info.rulebookDescription = rulebookDescription;
            info.abilityLearnedDialogue = SetAbilityInfoDialogue(dialogue);
            info.powerLevel = powerLevel;

            info.activated = true;

            info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

            if (!overrideModular && ConfigManager.Instance.AllModular)
                info.AddMetaCategories(AbilityMetaCategory.Part1Modular);

            return AbilityManager.Add(pluginGuid, info, typeof(T), WstlTextureHelper.LoadTextureFromResource(texture));
        }
        // Stat Icons
        public static StatIconManager.FullStatIcon CreateStatIcon<T>(
            string name, string description, byte[] texture, byte[] pixelTexture, bool attack, bool health)
            where T : VariableStatBehaviour
        {
            StatIconInfo statIconInfo = ScriptableObject.CreateInstance<StatIconInfo>();
            statIconInfo.rulebookName = name;
            statIconInfo.rulebookDescription = description;
            statIconInfo.appliesToAttack = attack;
            statIconInfo.appliesToHealth = health;
            statIconInfo.iconGraphic = WstlTextureHelper.LoadTextureFromResource(texture);
            statIconInfo.SetPixelIcon(WstlTextureHelper.LoadTextureFromResource(pixelTexture));
            statIconInfo.SetDefaultPart1Ability();

            return StatIconManager.Add(pluginGuid, statIconInfo, typeof(T));
        }
        // Special Abilities
        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreateSpecialAbility<T>(string rulebookName, string rulebookDesc)
            where T : SpecialCardBehaviour
        {
            return SpecialTriggeredAbilityManager.Add(pluginGuid, rulebookName, typeof(T));
        }
        // Adds AbilityInfo dialogue
        public static DialogueEvent.LineSet SetAbilityInfoDialogue(string dialogue)
        {
            return new DialogueEvent.LineSet(new List<DialogueEvent.Line>()
                {
                    new()
                    {
                        text = dialogue
                    }
                }
            );
        }
    }
}
