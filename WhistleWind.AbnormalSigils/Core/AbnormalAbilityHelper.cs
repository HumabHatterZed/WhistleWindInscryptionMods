using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System;
using UnityEngine;
using WhistleWind.Core.Helpers;
using static InscryptionAPI.Card.AbilityManager;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;

namespace WhistleWind.AbnormalSigils.Core.Helpers {
    public static class AbnormalAbilityHelper {
        public const string ADDTORULEBOOK = "wstl_ADDTORULEBOOK";
        public const string FORCEMODULAR = "wstl_FORCEMODULAR";

        public static CardInfo SetUniqueCopycat(this CardInfo info, string id) => info.SetExtendedProperty(Copycat.UNIQUE_COPYCAT_ID, id);

        public static CardInfo SetGiftGiverId(this CardInfo info, string id) => info.SetExtendedProperty(GiftGiver.CUSTOM_CARD_PROPERTY, id);

        public static CardInfo SetBoneless(this CardInfo info) {
            info.AddTraits(Boneless);
            if (info.HasCardMetaCategory(CardMetaCategory.Rare)) {
                info.AddAppearances(RareBonelessCardBackground.appearance);
            }
            else {
                info.AddAppearances(BonelessCardBackground.appearance);
            }

            return info;
        }
        public static CardInfo SetMiniGiant(this CardInfo info) {
            return info.AddSpecialAbilities(MiniGiantCard.Id).AddAppearances(MiniGiantPortrait.appearance).AddTraits(Trait.Giant);
        }
        public static CardInfo SetMiniGiantEmission(this CardInfo info, Texture2D emissionTex) {
            return info.SetPortrait(emissionTex);
        }

        public static bool HasUniqueCopyCat(this CardInfo info) => info.GetExtendedProperty(Copycat.UNIQUE_COPYCAT_ID) != null;
        public static string GetUniqueCopyCat(this CardInfo info) => info.GetExtendedProperty(Copycat.UNIQUE_COPYCAT_ID);
        public static bool IsConductor(this PlayableCard card) {
            return card.HasTrait(Orchestral) || card.HasAnyOfAbilities(Conductor.ability, MovementOne.ability, MovementTwo.ability, MovementThree.ability, MovementFour.ability, MovementFive.ability);
        }
        /// <summary>
        /// If true, Opportunistic's effect can trigger against the target card.
        /// </summary>
        /// <param name="attacker">The card with Opportunistic.</param>
        /// <param name="target">The card being targeted.</param>
        [Obsolete("Opportunistic has been reworked, method no longer used.")]
        public static bool SimulateOneSidedAttack(PlayableCard attacker, PlayableCard target) {
            if (target == null || target.Attack > 0 || target.HasAbility(Neutered.ability))
                return false;

            if (target.GetOpposingSlots().Contains(attacker.Slot)) {
                return target.CanAttackDirectly(attacker.Slot) || target.AttackIsBlocked(attacker.Slot) || (target.HasAbility(Ability.TailOnHit) && !target.Status.lostTail);
            }

            return false;
        }
        /// <summary>
        /// Returns whether Persistent can trigger during an attack.
        /// </summary>
        /// <param name="attacker">The card with Persistent.</param>
        /// <param name="target">The card being targeted.</param>
        public static bool SimulatePersistentAttack(PlayableCard attacker, PlayableCard target) {
            // Damsel ally override Persistent behaviour
            if (target == null || attacker.Slot.GetAdjacentCards().Exists(x => x != null && x.HasAbility(Damsel.ability)))
                return false;

            // if attacker can hit the target
            if (attacker.LacksAbility(Ability.Flying) || target.HasAbility(Ability.Reach)) {
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
            bool canStack = false, bool modular = false, bool opponent = false,
            bool unobtainable = false, bool special = false) where T : AbilityBehaviour {
            bool addToRulebook = AddToRulebook(AbilityGroup.Normal, unobtainable, special);
            bool forceModular = ForceModularity(AbilityGroup.Normal, modular, unobtainable, special);
            FullAbility ab = AbilityHelper.New<T>(pluginGuid,
                abilityName, rulebookName, rulebookDescription, powerLevel, false, dialogue, triggerText,
                canStack, false, opponent);

            ab.SetExtendedProperty(ADDTORULEBOOK, addToRulebook);
            ab.SetExtendedProperty(FORCEMODULAR, forceModular);
            return ab;
        }

        public static FullAbility CreateActivatedAbility<T>(
            string abilityName,
            string rulebookName, string rulebookDescription,
            string dialogue = null, string triggerText = null,
            int powerLevel = 0,
            bool special = false, bool unobtainable = false)
            where T : AbilityBehaviour {
            bool addToRulebook = AddToRulebook(AbilityGroup.Activated, unobtainable, special);
            bool forceModular = ForceModularity(AbilityGroup.Activated, false, unobtainable, special);
            FullAbility ab = AbilityHelper.NewActivated<T>(pluginGuid, abilityName, rulebookName, rulebookDescription, powerLevel, false, dialogue, triggerText,
                false, false);

            ab.SetExtendedProperty(ADDTORULEBOOK, addToRulebook);
            ab.SetExtendedProperty(FORCEMODULAR, forceModular);
            return ab;
        }

        private static bool AddToRulebook(AbilityGroup defaultGroup, bool rulebookOnly, bool special) {
            if (ForceDisable.HasFlag(AbilityGroup.All) || ForceDisable.HasFlags(AbilityGroup.Normal, AbilityGroup.Activated, AbilityGroup.Special))
                return false;

            if (rulebookOnly)
                return true;

            if (special && ForceDisable.HasFlag(AbilityGroup.Special))
                return false;

            if (ForceDisable.HasFlag(defaultGroup))
                return false;

            return true;
        }
        private static bool ForceModularity(AbilityGroup defaultGroup, bool defaultModular, bool rulebookOnly, bool special) {
            if (ForceModular.HasFlag(AbilityGroup.All) || ForceModular.HasFlags(AbilityGroup.Normal, AbilityGroup.Activated, AbilityGroup.Special))
                return true;

            if (rulebookOnly)
                return false;

            if (special && ForceModular.HasFlag(AbilityGroup.Special))
                return true;

            if (ForceModular.HasFlag(defaultGroup))
                return true;

            return defaultModular;
        }

        private static AbilityGroup ForceModular => AbnormalConfigManager.Instance.MakeModular;
        private static AbilityGroup ForceDisable => AbnormalConfigManager.Instance.DisableModular;

        [Flags]
        public enum AbilityGroup {
            None = 0,
            Normal = 1,
            Activated = 2,
            Special = 4,
            All = 8
        }
    }
}