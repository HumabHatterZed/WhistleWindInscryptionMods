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

namespace WhistleWindLobotomyMod
{
    public static class AbilityHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        private const string modPrefix = "wstl";

        // Special Abilities
        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreateSpecialAbility<T>(string rulebookName, string rulebookDesc)
            where T : SpecialCardBehaviour
        {
            return SpecialTriggeredAbilityManager.Add(modPrefix, rulebookName, typeof(T));
        }
        // Activated Ability
        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
            byte[] texture,
            string rulebookName, string rulebookDescription, string dialogue,
            int powerLevel = 0) where T : ActivatedAbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            Texture2D tex = WstlTextureHelper.LoadTextureFromResource(texture);
            List<AbilityMetaCategory> list = new() { AbilityMetaCategory.Part1Rulebook };
            info.rulebookName = rulebookName;
            info.rulebookDescription = rulebookDescription;
            info.powerLevel = powerLevel;
            info.passive = false;
            info.canStack = false;
            info.opponentUsable = false;
            info.flipYIfOpponent = false;
            info.activated = true;
            info.metaCategories = list;
            info.abilityLearnedDialogue = SetAbilityInfoDialogue(dialogue);

            return AbilityManager.Add(modPrefix, info, typeof(T), tex);
        }
        // Ability
        public static AbilityManager.FullAbility CreateAbility<T>(
            byte[] texture,
            string rulebookName, string rulebookDescription, string dialogue,
            int powerLevel = 0,
            bool addModular = false, bool isPassive = false, bool canStack = false,
            bool opponent = false, bool flipY = false, byte[] customY = null,
            bool overrideModular = false)
            where T:AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            Texture2D tex = WstlTextureHelper.LoadTextureFromResource(texture);
            Texture2D flippedTex = null;
            if (customY != null) { flippedTex = WstlTextureHelper.LoadTextureFromResource(customY); }
            List<AbilityMetaCategory> list = new() { AbilityMetaCategory.Part1Rulebook };
            if ((addModular || ConfigUtils.Instance.AllModular) && !overrideModular)
            {
                list.Add(AbilityMetaCategory.Part1Modular);
            }
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

            return AbilityManager.Add(modPrefix, info, typeof(T), tex);
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
