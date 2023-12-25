using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using static InscryptionAPI.Card.AbilityManager;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;

namespace WhistleWind.AbnormalSigils.Core.Helpers
{
    public static class AbnormalAbilityHelper
    {
        /// <summary>
        /// Returns whether Opportunistic/OneSided can trigger during an attack.
        /// </summary>
        /// <param name="attacker">The card with Opportunistic.</param>
        /// <param name="target">The card being targeted.</param>
        public static bool SimulateOneSidedAttack(PlayableCard attacker, PlayableCard target)
        {
            if (target == null)
                return false;

            // target cannot attack on its turn
            if (target.Attack == 0 || target.HasAbility(Neutered.ability))
                return true;

            // attacker is not being targeted
            if (!target.GetOpposingSlots().Contains(attacker.Slot))
                return true;

            // if target can hit us
            if (target.LacksAbility(Ability.Flying) || attacker.HasAbility(Ability.Reach))
            {
                // if the target is Persistent it can always hit us
                if (target.HasAbility(Persistent.ability))
                    return false;

                // target cannot hit facedown/submerged cards, Repulsive cards, or Loose Tail cards with their tail intact
                return attacker.FaceDown || attacker.HasAbility(Ability.PreventAttack) || (attacker.HasAbility(Ability.TailOnHit) && !attacker.Status.lostTail);
            }
            return true; // target cannot hit us
        }
        /// <summary>
        /// Returns whether Persistent can trigger during an attack.
        /// </summary>
        /// <param name="attacker">The card with Persistent.</param>
        /// <param name="target">The card being targeted.</param>
        public static bool SimulatePersistentAttack(PlayableCard attacker, PlayableCard target)
        {
            if (target == null)
                return false;

            // if attacker can hit the target
            if (attacker.LacksAbility(Ability.Flying) || target.HasAbility(Ability.Reach))
            {
                // if the target has Loose Tail and hasn't lost it
                if (target.HasAbility(Ability.TailOnHit) && !target.Status.lostTail)
                    return true;

                // attacker can hit facedown cards and Repulsive cards
                return target.FaceDown || target.HasAbility(Ability.PreventAttack);
            }
            return false;
        }
        public static FullAbility CreateAbility<T>(
            string abilityName,
            string rulebookName, string rulebookDescription,
            string dialogue = null, string triggerText = null,
            int powerLevel = 0,
            bool modular = false, bool special = false,
            bool opponent = false, bool canStack = false,
            bool unobtainable = false,
            bool flipY = false, string flipTextureName = null)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.CheckModularity(unobtainable, special, modular, AbilityGroup.Normal);

            return AbilityHelper.CreateAbility<T>(
                info, pluginGuid,
                abilityName,
                rulebookName, rulebookDescription,
                dialogue, triggerText,
                powerLevel,
                opponent, canStack, false, false, flipY, flipTextureName);
        }

        public static FullAbility CreateActivatedAbility<T>(
            string abilityName,
            string rulebookName, string rulebookDescription,
            string dialogue = null, string triggerText = null,
            int powerLevel = 0,
            bool special = false, bool unobtainable = false)
            where T : ExtendedActivatedAbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.CheckModularity(unobtainable, special, false, AbilityGroup.Activated);

            return AbilityHelper.CreateActivatedAbility<T>(
                info, pluginGuid,
                abilityName,
                rulebookName, rulebookDescription,
                dialogue, triggerText,
                powerLevel);
        }

        private static AbilityInfo CheckModularity(this AbilityInfo info, bool unobtainable, bool special, bool makeModular, AbilityGroup defaultGroup)
        {
            if (ForceDisable.HasFlag(AbilityGroup.All) ||
                ForceDisable.HasFlags(AbilityGroup.Normal, AbilityGroup.Activated, AbilityGroup.Special))
                return info;

            if (unobtainable)
                info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);
            else if (special && !ForceDisable.HasFlag(AbilityGroup.Special))
            {
                info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                if (ForceModular.HasFlag(AbilityGroup.All) || ForceModular.HasFlag(AbilityGroup.Special))
                    info.AddMetaCategories(AbilityMetaCategory.Part1Modular);
            }
            else if (!ForceDisable.HasFlag(defaultGroup))
            {
                info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                if (makeModular || ForceModular.HasFlag(AbilityGroup.All) || ForceModular.HasFlag(defaultGroup))
                    info.AddMetaCategories(AbilityMetaCategory.Part1Modular);
            }
            return info;
        }

        private static AbilityGroup ForceModular => AbnormalConfigManager.Instance.MakeModular;
        private static AbilityGroup ForceDisable => AbnormalConfigManager.Instance.DisableModular;

        [Flags]
        public enum AbilityGroup
        {
            None = 0,
            Normal = 1,
            Activated = 2,
            Special = 4,
            All = 8
        }
    }
}