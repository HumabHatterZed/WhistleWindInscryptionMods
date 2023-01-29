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
                abilities.RemoveAll((Ability x) => hiddenAbilities.Contains(x));

            return abilities;
        }
    }
}