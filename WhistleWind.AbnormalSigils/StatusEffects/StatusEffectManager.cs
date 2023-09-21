using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using static InscryptionAPI.Card.AbilityManager;
using static InscryptionAPI.Card.SpecialTriggeredAbilityManager;

namespace WhistleWind.AbnormalSigils.StatusEffects
{
    public static class StatusEffectManager
    {
        public class FullStatusEffect
        {
            public string ModGUID;
            public string RulebookName;
            public Type BehaviourType;
            public SpecialTriggeredAbility BehaviourId;
            public Ability IconId;
            public AbilityInfo IconAbilityInfo;
            public FullStatusEffect()
            {

            }
        }
        public static readonly List<FullStatusEffect> AllStatusEffects = new();
        public static readonly Dictionary<Ability, Color> AllIconColours = new();

        public static FullStatusEffect NewStatusEffect<T>(
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

            info.SetExtendedProperty("wstl:StatusEffect", true);
            Texture2D icon = TextureLoader.LoadTextureFromFile(iconTexture);

            if (pixelIconTexture != null)
                info.SetPixelAbilityIcon(TextureLoader.LoadTextureFromFile(pixelIconTexture));

            // StatusMetaCategories correspond to values of AbilityMetaCategory
            foreach (StatusMetaCategory category in categories)
                info.AddMetaCategories((AbilityMetaCategory)category);

            FullAbility statusEffectIcon = Add(pluginGuid, info, typeof(AbilityBehaviour), icon);
            FullSpecialTriggeredAbility statusEffectBehaviour = Add(pluginGuid, rulebookName, typeof(T));

            FullStatusEffect fullEffect = new()
            {
                ModGUID = statusEffectBehaviour.ModGUID,
                RulebookName = statusEffectBehaviour.AbilityName,
                BehaviourType = statusEffectBehaviour.AbilityBehaviour,
                BehaviourId = statusEffectBehaviour.Id,
                IconId = statusEffectIcon.Id,
                IconAbilityInfo = statusEffectIcon.Info
            };

            AllStatusEffects.Add(fullEffect);
            AllIconColours.Add(statusEffectIcon.Id, iconColour);
            return fullEffect;
        }

        public static CardModificationInfo StatusMod(string singletonName, bool positiveEffect, bool inheritable = false)
        {
            CardModificationInfo retval = new()
            {
                singletonId = singletonName,
                nonCopyable = !inheritable
            };
            retval.SetExtendedProperty("wstl:StatusEffectMod", true).SetExtendedProperty("wstl:PositiveEffect", positiveEffect);
            return retval;
        }

        public static T AddStatusEffectToCard<T>(this PlayableCard playableCard, int extraStacks = 0, bool addDecal = false) where T : StatusEffectBehaviour
        {
            playableCard.AddPermanentBehaviour<T>();
            T component = playableCard.GetStatusEffect<T>();
            component.UpdateStatusEffectCount(extraStacks, addDecal);
            return component;
        }

        public static void UpdateStatusEffectCount<T>(this PlayableCard card, int numToAdd, bool updateDecals) where T : StatusEffectBehaviour
        {
            T component = card.GetStatusEffect<T>();
            component.UpdateStatusEffectCount(numToAdd, updateDecals);
        }

        public static int GetStatusEffectStacks<T>(this PlayableCard playableCard) where T : StatusEffectBehaviour
        {
            Ability iconAbility = GetStatusEffectIconAbility<T>();
            return playableCard.GetAbilityStacks(iconAbility);
        }
        public static bool IsStatusMod(this CardModificationInfo mod) => mod.GetExtendedPropertyAsBool("wstl:StatusEffectMod") ?? false;
        public static bool IsStatusMod(this CardModificationInfo mod, bool positiveEffect) => mod.IsStatusMod() && mod.GetExtendedPropertyAsBool("wstl:PositiveEffect") == positiveEffect;

        public static Ability GetStatusEffectIconAbility(SpecialTriggeredAbility statusBehaviourAbility)
        {
            return AllStatusEffects.Find(x => x.BehaviourId == statusBehaviourAbility).IconId;
        }
        public static Ability GetStatusEffectIconAbility<T>() where T : StatusEffectBehaviour
        {
            return AllStatusEffects.Find(x => x.BehaviourType == typeof(T)).IconId;
        }
        public static AbilityInfo GetStatusEffectIconAbilityInfo(SpecialTriggeredAbility statusBehaviourAbility)
        {
            return AllStatusEffects.Find(x => x.BehaviourId == statusBehaviourAbility).IconAbilityInfo;
        }
        public static AbilityInfo GetStatusEffectIconAbilityInfo<T>() where T : StatusEffectBehaviour
        {
            return AllStatusEffects.Find(x => x.BehaviourType == typeof(T)).IconAbilityInfo;
        }

        public static bool HasStatusEffect<T>(this PlayableCard card) where T : StatusEffectBehaviour
        {
            foreach (var effect in card.GetStatusEffects())
            {
                if (effect is T)
                    return true;
            }
            return false;
        }
        public static List<StatusEffectBehaviour> GetStatusEffects(this PlayableCard card)
        {
            StatusEffectBehaviour[] statusEffects = card.GetComponents<StatusEffectBehaviour>();

            if (statusEffects.Length > 0)
                return statusEffects.ToList();

            return new();
        }
        public static T GetStatusEffect<T>(this PlayableCard card) where T : StatusEffectBehaviour
        {
            return card.GetComponent<T>();
        }
        public static List<StatusEffectBehaviour> GetStatusEffects(this PlayableCard card, bool positiveEffect)
        {
            List<StatusEffectBehaviour> statusEffects = card.GetStatusEffects();
            statusEffects.RemoveAll(x => AllStatusEffects.Find(y => y.BehaviourType == x.GetType()).IconAbilityInfo.PositiveEffect != positiveEffect);

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