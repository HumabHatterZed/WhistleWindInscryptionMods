using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            public bool AddNormalRulebookEntry;
            public List<StatusMetaCategory> statusMetaCategories = new();
            public FullStatusEffect()
            {

            }
        }

        internal static List<FullStatusEffect> allStatusEffects = new();
        public static List<FullStatusEffect> AllStatusEffects => new(allStatusEffects);

        public static readonly Dictionary<Ability, Color> AllIconColours = new();

        public static FullStatusEffect NewStatusEffect<T>(
            string pluginGuid,
            string rulebookName,
            string rulebookDescription,
            string iconTexture,
            Assembly modAssembly = null,
            string pixelIconTexture = null,
            int powerLevel = 0,
            List<StatusMetaCategory> categories = null,
            Color iconColour = default
            ) where T : StatusEffectBehaviour
        {
            Texture2D icon = TextureLoader.LoadTextureFromFile(iconTexture, modAssembly);
            Texture2D pixelIcon = TextureLoader.LoadTextureFromFile(pixelIconTexture, modAssembly);

            return NewStatusEffect<T>(pluginGuid, rulebookName, rulebookDescription, icon, pixelIcon, powerLevel, categories, iconColour);
        }
        public static FullStatusEffect NewStatusEffect<T>(
            string pluginGuid,
            string rulebookName,
            string rulebookDescription,
            Texture2D iconTexture,
            Texture2D pixelIconTexture = null,
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
            info.colorOverride = iconColour;

            info.SetExtendedProperty("wstl:StatusEffect", true);

            if (pixelIconTexture != null)
                info.SetPixelAbilityIcon(pixelIconTexture);

            // StatusMetaCategories correspond to values of AbilityMetaCategory
            foreach (StatusMetaCategory category in categories)
                info.AddMetaCategories((AbilityMetaCategory)category);

            FullAbility statusEffectIcon = Add(pluginGuid, info, typeof(AbilityBehaviour), iconTexture);
            FullSpecialTriggeredAbility statusEffectBehaviour = Add(pluginGuid, rulebookName, typeof(T));

            FullStatusEffect fullEffect = new()
            {
                ModGUID = statusEffectBehaviour.ModGUID,
                RulebookName = statusEffectBehaviour.AbilityName,
                BehaviourType = statusEffectBehaviour.AbilityBehaviour,
                BehaviourId = statusEffectBehaviour.Id,
                IconId = statusEffectIcon.Id,
                IconAbilityInfo = statusEffectIcon.Info,
                AddNormalRulebookEntry = false,
                statusMetaCategories = categories
            };

            allStatusEffects.Add(fullEffect);
            AllIconColours.Add(statusEffectIcon.Id, iconColour);
            return fullEffect;
        }

        public static FullStatusEffect SetAddNormalEntry(this FullStatusEffect statusEffect, bool addNormalEntry = true)
        {
            statusEffect.AddNormalRulebookEntry = addNormalEntry;
            return statusEffect;
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

        public static T AddStatusEffect<T>(this PlayableCard card, int effectSeverity,
            bool addDecals = false, Func<int, int> modifyTurnGained = null) where T : StatusEffectBehaviour
        {
            T component = card.GetStatusEffect<T>();
            if (component == null)
                card.AddPermanentBehaviour<T>();

            component = card.GetStatusEffect<T>();
            component.AddSeverity(effectSeverity, addDecals);
            component.TurnGained = modifyTurnGained?.Invoke(TurnManager.Instance.TurnNumber) ?? TurnManager.Instance.TurnNumber;
            return component;
        }
        public static IEnumerator AddStatusEffectFlipCard<T>(this PlayableCard card, int effectSeverity,
            bool addDecals = false, Func<int, int> modifyTurnGained = null) where T : StatusEffectBehaviour
        {
            bool faceDown = card.FaceDown;
            yield return card.FlipFaceUp(faceDown);
            card.AddStatusEffect<T>(effectSeverity, addDecals, modifyTurnGained);
            yield return card.FlipFaceDown(faceDown);
            if (faceDown)
                yield return new WaitForSeconds(0.4f);
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

        public static bool HasStatusEffect<T>(this PlayableCard card, bool atLeastOneStack = false) where T : StatusEffectBehaviour
        {
            foreach (var effect in card.GetStatusEffects())
            {
                if (effect is T)
                {
                    if (atLeastOneStack)
                        return (effect as T).EffectSeverity > 0;
                    return true;
                }
            }
            return false;
        }

        public static T GetStatusEffect<T>(this PlayableCard card) where T : StatusEffectBehaviour
        {
            return card.GetComponent<T>();
        }
        public static List<StatusEffectBehaviour> GetStatusEffects(this PlayableCard card)
        {
            StatusEffectBehaviour[] statusEffects = card.GetComponents<StatusEffectBehaviour>();

            if (statusEffects.Length > 0)
                return statusEffects.ToList();

            return new();
        }
        public static List<StatusEffectBehaviour> GetStatusEffects(this PlayableCard card, bool positiveEffect)
        {
            List<StatusEffectBehaviour> statusEffects = card.GetStatusEffects();
            statusEffects.RemoveAll(x => AllStatusEffects.Find(y => y.BehaviourType == x.GetType()).IconAbilityInfo.PositiveEffect != positiveEffect);

            return statusEffects;
        }

        public static List<Ability> GetDisplayedStatusEffects(this PlayableCard card, bool retainStacks)
        {
            List<Ability> abilities = card.GetAbilitiesFromAllMods();
            abilities.RemoveAll(x => !AllIconColours.ContainsKey(x));
            if (abilities.Count == 0)
                return new();

            if (retainStacks)
                return abilities;
            else
                return abilities.Distinct().ToList();
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