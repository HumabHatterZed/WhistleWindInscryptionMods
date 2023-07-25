using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using UnityEngine;

using WhistleWind.Core.Helpers;
using static InscryptionAPI.Card.AbilityManager;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static InscryptionAPI.Card.SpecialTriggeredAbilityManager;
using InscryptionAPI.Guid;
using HarmonyLib;
using InscryptionAPI.Triggers;
using Sirenix.Utilities;

namespace WhistleWind.AbnormalSigils.StatusEffects
{
    public class StatusEffectFillerAbility : AbilityBehaviour
    {
        // Ability.None - this is a dummy class so we don't need to care about it
        public override Ability Ability => Ability.None;
    }

    public static class StatusEffectManager
    {
        public static readonly Dictionary<Type, AbilityInfo> AllStatusEffects = new();
        public static readonly Dictionary<Ability, Color> AllIconColours = new();

        public static CardModificationInfo StatusMod(string name, bool positiveEffect, bool inheritable = false)
        {
            return new()
            {
                singletonId = (positiveEffect ? "" : "bad_") + "status_effect_" + name,
                nonCopyable = !inheritable
            };
        }

        public static bool IsStatusMod(this CardModificationInfo mod)
        {
            return mod.singletonId?.Contains("status_effect") ?? false;
        }
        public static bool IsStatusMod(this CardModificationInfo mod, bool positiveEffect)
        {
            return mod.singletonId?.StartsWith(positiveEffect ? "status" : "bad") ?? false;
        }
        public static Tuple<SpecialTriggeredAbility, Ability> NewStatusEffect<T>(
            string pluginGuid,
            string rulebookName,
            string rulebookDescription,
            string iconTexture,
            string pixelIconTexture = null,
            int powerLevel = 0,
            List<StatusMetaCategory> categories = null,
            Color iconColour = default
            ) where T : StatusEffectBehaviour
        {
            categories ??= new();

            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>().SetCanStack().SetPassive();

            info.rulebookName = rulebookName;
            info.rulebookDescription = rulebookDescription;
            info.powerLevel = powerLevel;

            Texture2D icon = TextureLoader.LoadTextureFromFile(iconTexture);
            
            if (pixelIconTexture != null)
                info.SetPixelAbilityIcon(TextureLoader.LoadTextureFromFile(pixelIconTexture));

            // StatusMetaCategories correspond to values of AbilityMetaCategory
            foreach (StatusMetaCategory category in categories)
                info.AddMetaCategories((AbilityMetaCategory)category);

            FullAbility iconAbility = Add(pluginGuid, info, typeof(AbilityBehaviour), icon);
            FullSpecialTriggeredAbility effectAbility = Add(pluginGuid, rulebookName, typeof(T));
            AllStatusEffects.Add(typeof(T), iconAbility.Info);
            AllIconColours.Add(iconAbility.Id, iconColour);
            return new(effectAbility.Id, iconAbility.Id);
        }
        public static List<StatusEffectBehaviour> GetStatusEffects(this PlayableCard card)
        {
            List<StatusEffectBehaviour> statusEffects = new();
            StatusEffectBehaviour[] specialAbilities= card.GetComponents<StatusEffectBehaviour>();
            foreach (StatusEffectBehaviour ab in specialAbilities)
            {
                // if the special ability is a status effect
                if (AllStatusEffects.ContainsKey(ab.GetType()))
                    statusEffects.Add(ab);
            }

            return statusEffects;
        }
        public static List<StatusEffectBehaviour> GetStatusEffects(this PlayableCard card, bool positiveEffect)
        {
            List<StatusEffectBehaviour> statusEffects = card.GetStatusEffects();
            statusEffects.RemoveAll(x => AllStatusEffects[x.GetType()].PositiveEffect != positiveEffect);
            return statusEffects;
        }

        public enum StatusMetaCategory
        {
            Part1StatusEffect = 0,
            Part3StatusEffect = 2,
            GrimoraStatusEffect = 5,
            MagnificusStatusEffect = 6
        }
    }
}