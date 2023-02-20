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
    public static class LobotomyCardManager // Base code taken from GrimoraMod and SigilADay_julienperge
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

        public static List<CardInfo> AllLobotomyCards = new();
        public static List<CardInfo> ObtainableLobotomyCards = new();

        public static CardMetaCategory SephirahCard = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "SephirahCard");

        public static Tribe TribeDivine;
        public static Tribe TribeFae;
        public static Tribe TribeHumanoid;
        public static Tribe TribeMachine;
        public static Tribe TribePlant;

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
            CardInfo cardInfo = CardHelper.CreateCard(
                pluginPrefix, name, displayName, description,
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
                if (spellType == SpellType.Targeted)
                    cardInfo.SetTargetedSpell();
                else
                    cardInfo.SetTargetedSpellStats();

                switch (spellType)
                {
                    case SpellType.Targeted:
                        cardInfo.SetNodeRestrictions(false, true, true, false);
                        break;
                    case SpellType.TargetedStats:
                        cardInfo.SetNodeRestrictions(false, true, false, false);
                        break;
                    case SpellType.TargetedSigils:
                        cardInfo.SetNodeRestrictions(false, false, true, false);
                        break;
                }
            }

            // cannot be used at any node
            if (modTypes.HasFlag(ModCardType.Restricted))
                cardInfo.SetNodeRestrictions(true, true, true, true);

            if (cardInfo.HasAnyOfCardMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.TraderOffer, CardMetaCategory.Rare))
                ObtainableLobotomyCards.Add(cardInfo);

            AllLobotomyCards.Add(cardInfo);

            Log.LogInfo($"{name:.16f} | {atk:.2f<}/{hp:.2f} | {blood}B :: x{bones} :: E{energy}");
        }

        private static CardInfo SetNodeRestrictions(this CardInfo card, bool give, bool gain, bool buff, bool copy)
        {
            if (give)
                card.AddMetaCategories(AbnormalPlugin.CannotGiveSigils);
            if (gain)
                card.AddMetaCategories(AbnormalPlugin.CannotGainSigils);
            if (buff)
                card.AddMetaCategories(AbnormalPlugin.CannotBoostStats);
            if (copy)
                card.AddMetaCategories(AbnormalPlugin.CannotCopyCard);
            return card;
        }
    }
}
