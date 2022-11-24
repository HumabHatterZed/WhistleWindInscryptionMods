using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core.Helpers
{
    public static class AbilityHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
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

            AbilityClass makeModular = (AbilityClass)AbnormalConfigManager.Instance.MakeModular;
            AbilityClass disableModular = (AbilityClass)AbnormalConfigManager.Instance.DisableModular;

            info.SetBasicInfo(rulebookName, rulebookDescription, dialogue, powerLevel);
            info.SetPixelAbilityIcon(TextureLoader.LoadTextureFromBytes(gbcTexture));

            info.opponentUsable = opponent;
            info.canStack = canStack;
            info.passive = isPassive;
            info.flipYIfOpponent = flipY;

            info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

            AbilityManager.FullAbility ability = AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromBytes(texture));

            if (flipTexture != null)
                ability.SetCustomFlippedTexture(TextureLoader.LoadTextureFromBytes(flipTexture));

            return ability;
        }
        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription, string dialogue, int powerLevel = 0,
            bool special = false, bool unobtainable = false)
            where T : ActivatedAbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            AbilityClass makeModular = (AbilityClass)AbnormalConfigManager.Instance.MakeModular;
            AbilityClass disableModular = (AbilityClass)AbnormalConfigManager.Instance.DisableModular;

            info.SetBasicInfo(rulebookName, rulebookDescription, dialogue, powerLevel);
            info.pixelIcon = TextureLoader.LoadTextureFromBytes(gbcTexture).ConvertTexture();

            info.activated = true;

            if (unobtainable)
                info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);
            else
            {
                if (special)
                {
                    if (!disableModular.HasFlag(AbilityClass.Special))
                    {
                        info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                        if (makeModular.HasFlag(AbilityClass.Special))
                            info.AddMetaCategories(AbilityMetaCategory.Part1Modular);
                    }
                }
                else if (!disableModular.HasFlag(AbilityClass.Activated))
                {
                    info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                    if (makeModular.HasFlag(AbilityClass.Activated))
                        info.AddMetaCategories(AbilityMetaCategory.Part1Modular);
                }
            }

            return AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromBytes(texture));
        }
        public static AbilityManager.FullAbility CreateRulebookAbility<T>(string rulebookName, string rulebookDescription)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            info.SetBasicInfo(rulebookName, rulebookDescription, "", 0);
            info.SetPixelAbilityIcon(TextureLoader.LoadTextureFromBytes(Artwork.sigilAbnormality_pixel));

            return AbilityManager.Add(pluginGuid, info, typeof(T), TextureLoader.LoadTextureFromBytes(Artwork.sigilAbnormality));
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
            statIconInfo.iconGraphic = TextureLoader.LoadTextureFromBytes(texture);
            statIconInfo.SetPixelIcon(TextureLoader.LoadTextureFromBytes(pixelTexture));
            statIconInfo.SetDefaultPart1Ability();

            return StatIconManager.Add(pluginGuid, statIconInfo, typeof(T));
        }

        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreateSpecialAbility<T>(string rulebookName)
            where T : SpecialCardBehaviour => SpecialTriggeredAbilityManager.Add(pluginGuid, rulebookName, typeof(T));
        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreatePaperTalkingCard<T>(string rulebookName)
            where T : PaperTalkingCard => SpecialTriggeredAbilityManager.Add(pluginGuid, rulebookName, typeof(T));

        private static AbilityInfo SetBasicInfo(this AbilityInfo info, string name, string desc, string dialogue, int powerLevel)
        {
            info.rulebookName = name;
            info.rulebookDescription = desc;
            info.abilityLearnedDialogue = SetAbilityInfoDialogue(dialogue);
            info.powerLevel = powerLevel;
            return info;
        }

        [Flags]
        private enum AbilityClass
        {
            None = 0,
            Normal = 1,
            Activated = 2,
            Special = 4
        }
        private static DialogueEvent.LineSet SetAbilityInfoDialogue(string dialogue) => new DialogueEvent.LineSet(new List<DialogueEvent.Line>() { new() { text = dialogue } });
    }
}
