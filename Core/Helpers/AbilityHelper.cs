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

namespace WhistleWindLobotomyMod
{
    public static class AbilityHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        // Ability
        public static AbilityManager.FullAbility CreateAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription, string dialogue, int powerLevel = 0,
            bool addModular = false, bool opponent = false, bool canStack = false, bool isPassive = false,
            bool flipY = false, byte[] customY = null,
            bool overrideModular = false)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            //Texture2D flippedTex = customY != null ? WstlTextureHelper.LoadTextureFromResource(customY) : null;

            info.SetPixelAbilityIcon(WstlTextureHelper.LoadTextureFromResource(gbcTexture));

            info.rulebookName = rulebookName;
            info.rulebookDescription = rulebookDescription;
            info.powerLevel = powerLevel;
            info.abilityLearnedDialogue = SetAbilityInfoDialogue(dialogue);
            
            info.opponentUsable = opponent;
            info.canStack = canStack;
            info.passive = isPassive;

            info.flipYIfOpponent = flipY;

            List<AbilityMetaCategory> list = new() { AbilityMetaCategory.Part1Rulebook };
            if ((addModular || ConfigManager.Instance.AllModular) && !overrideModular) { list.Add(AbilityMetaCategory.Part1Modular); }
            info.metaCategories = list;

            return AbilityManager.Add(pluginGuid, info, typeof(T), WstlTextureHelper.LoadTextureFromResource(texture));
        }
        // Activated Ability
        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription, string dialogue, int powerLevel = 0)
            where T : ActivatedAbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            Texture2D gbcTex = WstlTextureHelper.LoadTextureFromResource(gbcTexture);
            info.pixelIcon = gbcTex.ConvertTexture();

            info.rulebookName = rulebookName;
            info.rulebookDescription = rulebookDescription;
            info.abilityLearnedDialogue = SetAbilityInfoDialogue(dialogue);
            info.powerLevel = powerLevel;

            info.activated = true;
            info.passive = false;
            info.canStack = false;
            info.opponentUsable = false;
            info.flipYIfOpponent = false;

            Texture2D tex = WstlTextureHelper.LoadTextureFromResource(texture);
            info.metaCategories = new() { AbilityMetaCategory.Part1Rulebook };

            return AbilityManager.Add(pluginGuid, info, typeof(T), tex);
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
