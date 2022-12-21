using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using System;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core.Helpers
{
    public static class LobotomyCardHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        [Flags]
        public enum ModCardType
        {
            None = 0,           // Default, nothing special
            Donator = 1,        // Donator class, can be removed from card choice
            EventCard = 2,      // Event background, only obtained through special methods
            Restricted = 4,     // Can't be used at any modification nodes
            Ruina = 8           // From Ruina expansion
        }

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
        public enum SpellType
        {
            None,
            Global,
            Targeted,
            TargetedStats,
            TargetedSigils,
            TargetedStatsSigils
        }

        public static CardMetaCategory SephirahCard = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "SephirahCard");
        public static CardMetaCategory CannotGiveSigils = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CannotGiveSigils");
        public static CardMetaCategory CannotGainSigils = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CannotGainSigils");
        public static CardMetaCategory CannotBoostStats = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CannotBoostStats");
        public static CardMetaCategory CannotCopyCard = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CannotCopyCard");

        // Cards
        public static void CreateCard(
            string name, string displayName,
            string description,
            int atk, int hp,
            int blood, int bones, int energy,
            byte[] portrait, byte[] emission = null, byte[] pixelTexture = null,
            byte[] altTexture = null, byte[] emissionAltTexture = null,
            byte[] titleTexture = null,
            List<Ability> abilities = null,
            List<SpecialTriggeredAbility> specialAbilities = null,
            List<CardMetaCategory> metaCategories = null,
            List<Tribe> tribes = null,
            List<Trait> traits = null,
            List<CardAppearanceBehaviour.Appearance> appearances = null,
            List<Texture> decals = null,
            SpecialStatIcon statIcon = SpecialStatIcon.None,
            CardHelper.CardChoiceType choiceType = CardHelper.CardChoiceType.None,
            CardHelper.CardMetaType metaTypes = 0,
            ModCardType modTypes = 0,
            RiskLevel riskLevel = RiskLevel.None,
            SpellType spellType = SpellType.None,
            string iceCubeName = null,
            string evolveName = null,
            int numTurns = 1,
            bool onePerDeck = false,
            bool hideStats = false,
            GameObject face = null,
            Tribe customTribe = Tribe.None
            )
        {
            // if this is an event card
            bool eventCard = modTypes.HasFlag(ModCardType.EventCard);
            bool riskDisabled = riskLevel != RiskLevel.None && DisabledRiskLevels.HasFlag(riskLevel);
            bool donatorDisabled = DonatorCardsDisabled && modTypes.HasFlag(ModCardType.Donator);
            bool ruinaDisabled = RuinaCardsDisabled && modTypes.HasFlag(ModCardType.Ruina);

            // mark disabled and event cards has non-choices (don't show up in card choices)
            if (AllCardsDisabled || riskDisabled || eventCard || donatorDisabled || ruinaDisabled)
            {
                if (!metaTypes.HasFlag(CardHelper.CardMetaType.NonChoice))
                    metaTypes++;
            }

            // Create initial card info
            CardInfo cardInfo = CardHelper.CreateCardInfo(
                name, displayName, description,
                atk, hp, blood, bones, energy,
                portrait, emission, pixelTexture, altTexture, emissionAltTexture, titleTexture,
                abilities, specialAbilities, metaCategories, tribes, traits, appearances, decals, statIcon,
                choiceType, metaTypes, iceCubeName, evolveName, numTurns, onePerDeck, hideStats
                );

            // for animated cards
            if (face != null)
                cardInfo.animatedPortrait = face;

            // Add KillsSurvivors trait to cards with Deathtouch or Punisher
            if (cardInfo.HasAnyOfAbilities(Punisher.ability, Ability.Deathtouch))
                cardInfo.AddTraits(Trait.KillsSurvivors);

            if (customTribe != Tribe.None)
                cardInfo.AddTribes(customTribe);

            // set risk level
            if (!AllCardsDisabled && !riskDisabled)
                cardInfo.SetExtendedProperty("wstl:RiskLevel", riskLevel.ToString());

            // add the event card background
            if (eventCard)
            {
                if (choiceType == CardHelper.CardChoiceType.Rare)
                    cardInfo.AddAppearances(RareEventBackground.appearance);
                else
                    cardInfo.AddAppearances(EventBackground.appearance);
            }

            // set spells
            if (spellType == SpellType.Global)
            {
                cardInfo.SetGlobalSpell();
                cardInfo.SetNodeRestrictions(true, true, true, true);
            }
            else if (spellType != SpellType.None)
            {
                cardInfo.SetTargetedSpell();
                switch (spellType)
                {
                    case SpellType.Targeted:
                        cardInfo.SetNodeRestrictions(true, true, true, true);
                        break;
                    case SpellType.TargetedStats:
                        cardInfo.hideAttackAndHealth = false;
                        cardInfo.SetNodeRestrictions(true, true, false, true);
                        break;
                    case SpellType.TargetedSigils:
                        cardInfo.SetNodeRestrictions(true, false, true, true);
                        break;
                    case SpellType.TargetedStatsSigils:
                        cardInfo.hideAttackAndHealth = false;
                        cardInfo.SetNodeRestrictions(true, false, false, false);
                        break;
                }
            }

            // cannot be used at any node
            if (modTypes.HasFlag(ModCardType.Restricted))
                cardInfo.SetNodeRestrictions(true, true, true, true);

            LobotomyCards.Add(cardInfo);
            if (cardInfo.metaCategories.Exists(mc => mc == CardMetaCategory.ChoiceNode || mc == CardMetaCategory.Rare || mc == SephirahCard))
                ObtainableLobotomyCards.Add(cardInfo);

            CardManager.Add(pluginPrefix, cardInfo);
        }

        private static CardInfo SetNodeRestrictions(this CardInfo card, bool give, bool gain, bool buff, bool copy)
        {
            if (give)
                card.AddMetaCategories(CannotGiveSigils);
            if (gain)
                card.AddMetaCategories(CannotGainSigils);
            if (buff)
                card.AddMetaCategories(CannotBoostStats);
            if (copy)
                card.AddMetaCategories(CannotCopyCard);
            return card;
        }
    }
}
