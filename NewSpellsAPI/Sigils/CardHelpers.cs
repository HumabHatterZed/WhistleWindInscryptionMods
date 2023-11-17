using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using System.Collections.Generic;

namespace Infiniscryption.Spells.Sigils
{
    public static class CardHelpers
    {
        public static CardInfo SetGlobalSpell(this CardInfo card)
        {
            card.hideAttackAndHealth = true;
            card.SetStatIcon(GlobalSpellAbility.Icon);
            if (card.HasCardMetaCategory(CardMetaCategory.Rare))
            {
                card.AddAppearances(SpellBehavior.RareSpellBackgroundAppearance.ID);
                card.appearanceBehaviour.Remove(CardAppearanceBehaviour.Appearance.RareCardBackground);
            }
            else
            {
                card.AddAppearances(SpellBehavior.SpellBackgroundAppearance.ID);
            }

            return card;
        }
        public static CardInfo SetInstaGlobalSpell(this CardInfo card)
        {
            card.hideAttackAndHealth = true;
            card.SetStatIcon(InstaGlobalSpellAbility.Icon);
            if (card.HasCardMetaCategory(CardMetaCategory.Rare))
            {
                card.AddAppearances(SpellBehavior.RareSpellBackgroundAppearance.ID);
                card.appearanceBehaviour.Remove(CardAppearanceBehaviour.Appearance.RareCardBackground);
            }
            else
            {
                card.AddAppearances(SpellBehavior.SpellBackgroundAppearance.ID);
            }

            return card;
        }
        public static CardInfo SetTargetedSpell(this CardInfo card)
        {
            card.hideAttackAndHealth = true;
            card.SetStatIcon(TargetedSpellAbility.Icon);
            if (card.HasCardMetaCategory(CardMetaCategory.Rare))
            {
                card.AddAppearances(SpellBehavior.RareSpellBackgroundAppearance.ID);
                card.appearanceBehaviour.Remove(CardAppearanceBehaviour.Appearance.RareCardBackground);
            }
            else
            {
                card.AddAppearances(SpellBehavior.SpellBackgroundAppearance.ID);
            }
            return card;
        }
        public static CardInfo SetTargetedSpellStats(this CardInfo card)
        {
            card.SetTargetedSpell();
            card.hideAttackAndHealth = false;
            return card;
        }
        public static CardInfo SetGlobalSpellStats(this CardInfo card)
        {
            card.SetGlobalSpell();
            card.hideAttackAndHealth = false;
            return card;
        }
        public static CardInfo SetInstaGlobalSpellStats(this CardInfo card)
        {
            card.SetInstaGlobalSpell();
            card.hideAttackAndHealth = false;
            return card;
        }
        public static CardInfo SetNeverBoostStats(this CardInfo card)
        {
            card.AddTraits(NeverBoostStats);
            return card;
        }
        public static Trait NeverBoostStats = GuidManager.GetEnumValue<Trait>(InfiniscryptionSpellsPlugin.OriginalPluginGuid, "NeverBoostStats");

        public static List<Ability> GetDistinctShownAbilities(CardInfo info, List<CardModificationInfo> mods, List<Ability> hiddenAbilities)
        {
            List<Ability> abilities = info.Abilities;
            abilities.AddRange(AbilitiesUtil.GetAbilitiesFromMods(mods));
            abilities = AbilitiesUtil.RemoveNonDistinctNonStacking(abilities);
            abilities.RemoveAll((Ability x) => mods.Exists((CardModificationInfo m) => m.negateAbilities.Contains(x)));
            if (hiddenAbilities != null)
                abilities.RemoveAll(hiddenAbilities.Contains);

            return abilities;
        }

        public static AbilityInfo SetCanMerge(this AbilityInfo info, bool canMerge = true)
        {
            info.SetExtendedProperty("Spells:CanMerge", canMerge);
            return info;
        }
        public static bool CanMerge(this AbilityInfo info)
        {
            return info.GetExtendedPropertyAsBool("Spells:CanMerge") ?? true;
        }
        public static bool CanMerge(this Ability ability)
        {
            return ability.GetExtendedPropertyAsBool("Spells:CanMerge") ?? true;
        }
    }
}