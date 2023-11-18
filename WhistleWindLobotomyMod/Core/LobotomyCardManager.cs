using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using System;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.Core.Helpers.CardHelper;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core
{
    public static class LobotomyCardManager // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static CardInfo NewCard(
            string cardName, string displayName = null,
            string description = null,
            int attack = 0, int health = 0,
            int blood = 0, int bones = 0, int energy = 0,
            CardTemple temple = CardTemple.Nature)
        {
            return CardHelper.NewCard(false, pluginPrefix, cardName, displayName, description, attack, health, blood, bones, energy, temple: temple);
        }

        public static CardInfo Build(
            this CardInfo cardInfo,
            ChoiceType choiceType = ChoiceType.None,
            RiskLevel riskLevel = RiskLevel.None,
            ModCardType cardType = ModCardType.None,
            bool nonChoice = false
            )
        {
            // if this is an event card
            bool eventCard = cardType.HasFlag(ModCardType.EventCard);
            bool riskDisabled = riskLevel != RiskLevel.None && DisabledRiskLevels.HasFlag(riskLevel);
            bool donatorDisabled = LobotomyConfigManager.Instance.NoDonators && cardType.HasFlag(ModCardType.Donator);
            bool ruinaDisabled = LobotomyConfigManager.Instance.NoRuina && cardType.HasFlag(ModCardType.Ruina);

            bool canBeObtained = choiceType != ChoiceType.None && !nonChoice && !AllCardsDisabled && !riskDisabled && !eventCard && !donatorDisabled && !ruinaDisabled;

            cardInfo.SetChoiceType(choiceType, !canBeObtained);

            // Add KillsSurvivors trait to cards with Deathtouch or Punisher
            if (cardInfo.HasAnyOfAbilities(Punisher.ability, Ability.Deathtouch))
                cardInfo.AddTraits(Trait.KillsSurvivors);

            // set risk level
            if (riskLevel != RiskLevel.None && canBeObtained)
                cardInfo.SetExtendedProperty("wstl:RiskLevel", riskLevel.ToString());

            // add the event card background
            if (eventCard)
            {
                if (choiceType == ChoiceType.Rare)
                    cardInfo.AddAppearances(RareEventBackground.appearance);
                else
                    cardInfo.AddAppearances(EventBackground.appearance);

                cardInfo.RemoveAppearances(CardAppearanceBehaviour.Appearance.TerrainBackground);
            }

            if (LobotomyConfigManager.Instance.GBCPacks && cardInfo.pixelPortrait != null)
            {
                CardTemple temple = cardInfo.temple;
                if (cardInfo.IsOfTribe(AbnormalPlugin.TribeFae))
                    temple = CardTemple.Wizard;
                else if (cardInfo.IsOfTribe(AbnormalPlugin.TribeMechanical))
                    temple = CardTemple.Tech;

                // make it available in packs and appear in the collection menu
                if (canBeObtained)
                    cardInfo.SetGBCPlayable(temple);
            }

            if (canBeObtained && cardInfo.HasAnyOfCardMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.Rare))
                ObtainableLobotomyCards.Add(cardInfo);

            AllLobotomyCards.Add(cardInfo);
            CardManager.Add(pluginPrefix, cardInfo);
            return cardInfo;
        }

        public static CardInfo SetSpellType(this CardInfo cardInfo, SpellType spellType)
        {
            switch (spellType)
            {
                case SpellType.Global:
                    cardInfo.SetGlobalSpell();
                    cardInfo.SetNodeRestrictions(true, true, true, true);
                    break;
                case SpellType.GlobalStats:
                    cardInfo.SetGlobalSpellStats();
                    cardInfo.SetNodeRestrictions(true, true, false, true);
                    break;
                case SpellType.GlobalSigils:
                    cardInfo.SetGlobalSpell();
                    cardInfo.SetNodeRestrictions(true, false, true, true);
                    break;
                case SpellType.Targeted:
                    cardInfo.SetTargetedSpell();
                    cardInfo.SetNodeRestrictions(false, true, true, false);
                    break;
                case SpellType.TargetedStats:
                    cardInfo.SetTargetedSpellStats();
                    cardInfo.SetNodeRestrictions(false, true, false, false);
                    break;
                case SpellType.TargetedSigils:
                    cardInfo.SetTargetedSpell();
                    cardInfo.SetNodeRestrictions(false, false, true, false);
                    break;
            }
            return cardInfo;
        }
        public static CardInfo SetNodeRestrictions(this CardInfo card, bool cannotGiveSigils, bool cannotGainSigils, bool cannotBuffStats, bool cannotCopyCard)
        {
            if (cannotGiveSigils)
                card.AddMetaCategories(AbnormalPlugin.CannotGiveSigils);
            if (cannotGainSigils)
                card.AddMetaCategories(AbnormalPlugin.CannotGainSigils);
            if (cannotBuffStats)
                card.AddMetaCategories(AbnormalPlugin.CannotBoostStats);
            if (cannotCopyCard)
                card.AddMetaCategories(AbnormalPlugin.CannotCopyCard);
            return card;
        }

        public static readonly List<CardInfo> AllLobotomyCards = new();
        public static readonly List<CardInfo> ObtainableLobotomyCards = new();

        public static Trait TraitApostle = GuidManager.GetEnumValue<Trait>(pluginGuid, "ApostleTrait");
        public static Trait TraitSephirah = GuidManager.GetEnumValue<Trait>(pluginGuid, "SephirahTrait");
        public static Trait TraitBlackForest = GuidManager.GetEnumValue<Trait>(pluginGuid, "BlackForestTrait");
        public static Trait TraitEmeraldCity = GuidManager.GetEnumValue<Trait>(pluginGuid, "EmeraldCityTrait");
        public static Trait TraitMagicalGirl = GuidManager.GetEnumValue<Trait>(pluginGuid, "MagicalGirlTrait");
        public static Trait TraitExecutioner = GuidManager.GetEnumValue<Trait>(pluginGuid, "Executioner");

        [Flags]
        public enum RiskLevel
        {
            None = 0,
            Zayin = 1,
            Teth = 2,
            He = 4,
            Waw = 8,
            Aleph = 16,
            All = 32
        }
        [Flags]
        public enum ModCardType
        {
            None = 0,           // Default, nothing special
            Donator = 1,        // Donator class, can be removed from card choice
            EventCard = 2,      // Event background, only obtained through special methods
            Ruina = 4           // From Ruina expansion
        }

        public enum SpellType
        {
            None,
            Global,
            GlobalStats,
            GlobalSigils,
            Targeted,
            TargetedStats,
            TargetedSigils
        }
    }
}
