using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using static InscryptionAPI.Card.AbilityManager;
using static InscryptionAPI.Card.SpecialTriggeredAbilityManager;
using static InscryptionAPI.Slots.SlotModificationManager;

namespace WhistleWind.AbnormalSigils.StatusEffects
{
    public static class StatusEffectManager
    {
        public class FullStatusEffect
        {
            public string ModGUID;
            
            public string RulebookName;

            public Type Behaviour;

            public SpecialTriggeredAbility Id;

            public Texture Icon;
            public AbilityInfo IconInfo;

            public bool SigilRulebookEntry;
            public List<StatusMetaCategory> statusMetaCategories = new();

            public FullStatusEffect(string modGuid, string rulebookName, Type type, SpecialTriggeredAbility id, Texture icon, AbilityInfo iconInfo)
            {
                this.ModGUID = modGuid;
                this.RulebookName = rulebookName;
                this.Behaviour = type;
                this.Id = id;
                this.Icon = icon;
                this.IconInfo = iconInfo;
            }

            public FullStatusEffect Clone()
            {
                return new(this.ModGUID, this.RulebookName, this.Behaviour, this.Id, this.Icon, this.IconInfo);
            }
        }

        internal static readonly ObservableCollection<FullStatusEffect> NewStatusEffects = new();
        public static List<FullStatusEffect> AllStatusEffects { get; private set; }

        public static FullStatusEffect EffectByIcon(this IEnumerable<FullStatusEffect> effects, Ability id)
        {
            return effects.FirstOrDefault(x => x.IconInfo.ability == id);
        }

        public static FullStatusEffect EffectByID(this IEnumerable<FullStatusEffect> effects, SpecialTriggeredAbility id)
        {
            return effects.FirstOrDefault(x => x.Id == id);
        }

        //public static readonly Dictionary<Ability, Color> AllIconColours = new();

        public static Texture2D StatusEffectPatch => _statusEffectPatch;

        private static Texture2D _statusEffectPatch = null;
        public static void SyncStatusEffects()
        {
            _statusEffectPatch ??= TextureLoader.LoadTextureFromFile("status_effect_patch.png", AbnormalPlugin.Assembly);
            AllStatusEffects = NewStatusEffects.Select(x => x.Clone()).ToList();
        }

        public static FullStatusEffect New<T>(
            string pluginGuid,
            string rulebookName, string rulebookDescription,
            int powerLevel, Color iconColour,
            Texture2D iconTexture, Texture2D pixelIconTexture = null) where T : StatusEffectBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>()
                .SetPassive()
                .SetCanStack()
                .SetStatusEffect(iconColour);
            info.rulebookName = rulebookName;
            info.rulebookDescription = rulebookDescription;
            info.powerLevel = powerLevel;

            if (pixelIconTexture != null)
                info.SetPixelAbilityIcon(pixelIconTexture);

            FullAbility iconInfo = Add(pluginGuid, info, typeof(AbilityBehaviour), iconTexture);
            FullSpecialTriggeredAbility statusBehav = Add(pluginGuid, rulebookName, typeof(T));

            FullStatusEffect fullEffect = new(pluginGuid, rulebookName, statusBehav.AbilityBehaviour, statusBehav.Id, iconInfo.Texture, iconInfo.Info);

            NewStatusEffects.Add(fullEffect);
            //AllIconColours.Add(iconInfo.Id, iconColour);
            return fullEffect;
        }

        public const string STATUS_EFFECT_PROPERTY = "wstl:StatusEffect";

        public static FullStatusEffect AddMetaCategories(this FullStatusEffect effect, params StatusMetaCategory[] categories)
        {
            effect.IconInfo.AddMetaCategories(categories.Convert(x => (AbilityMetaCategory)x).ToArray());
            return effect;
        }
        public static FullStatusEffect SetSigilEntry(this FullStatusEffect statusEffect, bool setSigilEntry = true)
        {
            statusEffect.SigilRulebookEntry = setSigilEntry;
            return statusEffect;
        }

        public static AbilityInfo SetStatusEffect(this AbilityInfo info, Color colourOverride)
        {
            return info.SetHasColorOverride(true, colourOverride).SetExtendedProperty(STATUS_EFFECT_PROPERTY, true);
        }
        public static bool IsStatusEffect(this AbilityInfo info)
        {
            return info.GetExtendedPropertyAsBool(STATUS_EFFECT_PROPERTY) == true;
        }
        public static bool IsStatusEffect(this AbilityInfo info, bool positiveEffect)
        {
            return info.IsStatusEffect() && info.PositiveEffect == positiveEffect;
        }

        public static CardModificationInfo SetStatusEffect(this CardModificationInfo mod, bool markAsStatus = true)
        {
            return mod.SetExtendedProperty(STATUS_EFFECT_PROPERTY, true);
        }
        public static bool IsStatusEffect(this CardModificationInfo mod)
        {
            return mod.GetExtendedPropertyAsBool(STATUS_EFFECT_PROPERTY) == true;
        }
        public static bool IsStatusMod(this CardModificationInfo mod, bool positiveEffect)
        {
            return mod.IsStatusEffect() && AbilitiesUtil.GetInfo(mod.abilities[0]).PositiveEffect == positiveEffect;
        }

        public static CardModificationInfo StatusMod(SpecialTriggeredAbility statusIcon, string singletonName, bool inheritable = false)
        {
            CardModificationInfo retval = new()
            {
                singletonId = singletonName,
                nonCopyable = !inheritable
            };
            retval.specialAbilities.Add(statusIcon);
            retval.SetStatusEffect();
            return retval;
        }

        public static IEnumerator AddStatusEffect<T>(this PlayableCard card, int amount, bool updateDecals = false, Func<int, int> modifyTurnGained = null)
            where T : StatusEffectBehaviour
        {
            
            T component = card.GetComponent<T>();
            bool firstStack = component == null;
            if (firstStack)
            {
                component = card.gameObject.AddComponent<T>();
                card.TriggerHandler.permanentlyAttachedBehaviours.Add(component);
                component.TurnGained = modifyTurnGained?.Invoke(TurnManager.Instance.TurnNumber) ?? TurnManager.Instance.TurnNumber;
            }

            component.ModifyPotency(amount, updateDecals);

            yield return CustomTriggerFinder.TriggerAll<IOnStatusEffectAdded>(firstStack,
                x => x.RespondsToStatusEffectAdded(card, amount, component, firstStack),
                x => x.OnStatusEffectAdded(card, amount, component, firstStack));

            if (!DialogueEventsData.EventIsPlayed("LearnStatusEffects"))
                yield return new WaitForSeconds(0.2f);

            yield return DialogueHelper.PlayDialogueEvent("LearnStatusEffects", card: card);

            if (card.GetDisplayedStatusEffects(false).Count > 5)
                yield return DialogueHelper.PlayDialogueEvent("LearnStatusEffectsOverflow", card: card);
        }

        public static IEnumerator AddStatusEffectToFaceDown<T>(this PlayableCard card, int amount = 1, bool updateDecals = false, Func<int, int> modifyTurnGained = null)
            where T : StatusEffectBehaviour
        {
            bool faceDown = card.FaceDown;
            yield return card.FlipFaceUp(faceDown);
            yield return card.AddStatusEffect<T>(amount, updateDecals, modifyTurnGained);
            yield return card.FlipFaceDown(faceDown);
        }

        public static int GetStatusEffectPotency<T>(this PlayableCard playableCard) where T : StatusEffectBehaviour
        {
            return playableCard.GetStatusEffect<T>()?.EffectPotency ?? 0;
        }

        public static bool HasStatusEffect<T>(this PlayableCard card, bool atLeastOneStack = false) where T : StatusEffectBehaviour
        {
            foreach (StatusEffectBehaviour effect in card.GetStatusEffects())
            {
                if (effect is T)
                {
                    if (atLeastOneStack)
                        return effect.EffectPotency > 0;

                    return true;
                }
            }
            return false;
        }

        public static bool HasStatusEffect(this PlayableCard card, Ability iconAbility, bool atLeastOneStack = false)
        {
            foreach (var effect in card.GetStatusEffects())
            {
                if (effect.IconAbility == iconAbility)
                {
                    if (atLeastOneStack)
                        return effect.EffectPotency > 0;
                    return true;
                }
            }
            return false;
        }

        public static T GetStatusEffect<T>(this PlayableCard card) where T : StatusEffectBehaviour
        {
            return card.GetComponent<T>();
        }

        public static StatusEffectBehaviour GetStatusEffect(this PlayableCard card, Ability iconAbility)
        {
            Type type = AllStatusEffects.EffectByIcon(iconAbility).Behaviour;
            if (type == null)
                return null;

            return card.GetComponent(type) as StatusEffectBehaviour;
        }

        public static StatusEffectBehaviour GetStatusEffect(this PlayableCard card, SpecialTriggeredAbility id)
        {
            Type type = AllStatusEffects.EffectByID(id).Behaviour;
            if (type == null)
                return null;

            return card.GetComponent(type) as StatusEffectBehaviour;
        }

        public static List<StatusEffectBehaviour> GetStatusEffects(this PlayableCard card)
        {
            return card.GetComponents<StatusEffectBehaviour>().ToList();
        }
        public static List<StatusEffectBehaviour> GetStatusEffects(this PlayableCard card, bool positiveEffect)
        {
            List<StatusEffectBehaviour> statusEffects = card.GetStatusEffects();
            statusEffects.RemoveAll(x => AbilitiesUtil.GetInfo(x.IconAbility).PositiveEffect != positiveEffect);
            return statusEffects;
        }

        public static List<Ability> GetDisplayedStatusEffects(this PlayableCard card, bool retainStacks)
        {
            List<CardModificationInfo> mods = card.AllCardModificationInfos().Where(x => x.IsStatusEffect()).ToList();
            List<Ability> abilities = mods.SelectMany(x => x.abilities).ToList();
            return retainStacks ? abilities : abilities.Distinct().ToList();
        }

        public static bool RemoveStatusEffect(this PlayableCard card, SpecialTriggeredAbility id)
        {
            StatusEffectBehaviour status = card.GetStatusEffect(id);
            if (status != null)
            {
                status.DestroyStatusEffect();
                return true;
            }
            return false;
        }
        public static bool RemoveStatusEffects(this PlayableCard card)
        {
            List<StatusEffectBehaviour> statuses = card.GetStatusEffects();
            if (statuses.Count > 0)
            {
                for (int i = 0; i < statuses.Count; i++)
                    statuses[i].DestroyStatusEffect(false);

                card.RenderCard();
                return true;
            }
            return false;
        }
        public static bool RemoveStatusEffects(this PlayableCard card, bool positiveEffects)
        {
            List<StatusEffectBehaviour> statuses = card.GetStatusEffects(positiveEffects);
            if (statuses.Count > 0)
            {
                for (int i = 0; i < statuses.Count; i++)
                    statuses[i].DestroyStatusEffect(false);

                card.RenderCard();
                return true;
            }
            return false;
        }
    }

    public enum StatusMetaCategory
    {
        Part1StatusEffect = 0,
        Part3StatusEffect = 2,
        GrimoraStatusEffect = 5,
        MagnificusStatusEffect = 6
    }
}