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
        // Special Abilities
        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreateSpecialAbility<T>(string rulebookName, string rulebookDesc)
            where T : SpecialCardBehaviour
        {
            return SpecialTriggeredAbilityManager.Add(pluginGuid, rulebookName, typeof(T));
        }
        // Stat Icons
        public static StatIconManager.FullStatIcon CreateStatIcon<T>(
            string name, string description, byte[] texture, byte[] pixelTexture, bool attack = true, bool health = false)
            where T : VariableStatBehaviour
        {
            StatIconInfo statIconInfo = StatIconManager.New(pluginGuid, name, description, typeof(T)).SetDefaultPart1Ability();
            statIconInfo.iconGraphic = WstlTextureHelper.LoadTextureFromResource(texture);
            statIconInfo.SetPixelIcon(WstlTextureHelper.LoadTextureFromResource(pixelTexture));
            statIconInfo.appliesToAttack = attack;
            statIconInfo.appliesToHealth = health;

            return StatIconManager.Add(pluginGuid, statIconInfo, typeof(T));
        }
        // Activated Ability
        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription,
            string dialogue, int powerLevel = 0) where T : ActivatedAbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            Texture2D tex = WstlTextureHelper.LoadTextureFromResource(texture);
            Texture2D gbcTex = WstlTextureHelper.LoadTextureFromResource(gbcTexture);
            info.SetPixelAbilityIcon(gbcTex);
            info.rulebookName = rulebookName;
            info.rulebookDescription = rulebookDescription;
            info.abilityLearnedDialogue = SetAbilityInfoDialogue(dialogue);
            info.powerLevel = powerLevel;
            info.activated = true;
            info.passive = false;
            info.canStack = false;
            info.opponentUsable = false;
            info.flipYIfOpponent = false;
            info.metaCategories = new() { AbilityMetaCategory.Part1Rulebook };

            return AbilityManager.Add(pluginGuid, info, typeof(T), tex);
        }
        // Ability
        public static AbilityManager.FullAbility CreateAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription,
            string dialogue, int powerLevel = 0,
            bool addModular = false, bool isPassive = false,
            bool canStack = false, bool opponent = false,
            bool flipY = false, byte[] customY = null,
            bool overrideModular = false)
            where T:AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            Texture2D tex = WstlTextureHelper.LoadTextureFromResource(texture);
            Texture2D flippedTex = customY != null ? WstlTextureHelper.LoadTextureFromResource(customY) : null;
            Texture2D gbcTex = gbcTexture != null ? WstlTextureHelper.LoadTextureFromResource(gbcTexture) : null;
            List<AbilityMetaCategory> list = new() { AbilityMetaCategory.Part1Rulebook };
            if ((addModular || ConfigUtils.Instance.AllModular) && !overrideModular)
            {
                list.Add(AbilityMetaCategory.Part1Modular);
            }
            info.SetPixelAbilityIcon(gbcTex);
            info.rulebookName = rulebookName;
            info.rulebookDescription = rulebookDescription;
            info.powerLevel = powerLevel;
            info.passive = isPassive;
            info.canStack = canStack;
            info.opponentUsable = opponent;
            info.flipYIfOpponent = flipY;
            info.metaCategories = list;
            info.abilityLearnedDialogue = SetAbilityInfoDialogue(dialogue);
            info.activated = false;
            if (flippedTex != null) { info.SetCustomFlippedTexture(flippedTex); }

            return AbilityManager.Add(pluginGuid, info, typeof(T), tex);
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
