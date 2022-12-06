using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using System;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using static WhistleWindLobotomyMod.LobotomyPlugin;
using WhistleWind.Core.Helpers;

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
        public enum RiskLevel
        {
            None,
            Zayin,
            Teth,
            He,
            Waw,
            Aleph
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

        public static CardMetaCategory SEPHIRAH_CARD = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "SEPHIRAH_CARD");
        public static CardMetaCategory CANNOT_GIVE_SIGILS = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CANNOT_GIVE_SIGILS");
        public static CardMetaCategory CANNOT_GAIN_SIGILS = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CANNOT_GAIN_SIGILS");
        public static CardMetaCategory CANNOT_BUFF_STATS = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CANNOT_BUFF_STATS");
        public static CardMetaCategory CANNOT_COPY_CARD = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CANNOT_COPY_CARD");

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
            GameObject face = null
            )
        {
            string risk = riskLevel switch
            {
                RiskLevel.Zayin => "Zayin",
                RiskLevel.Teth => "Teth",
                RiskLevel.He => "He",
                RiskLevel.Waw => "Waw",
                RiskLevel.Aleph => "Aleph",
                _ => null
            };

            bool eventCard = modTypes.HasFlag(ModCardType.EventCard);

            bool disableDonators = ConfigManager.Instance.NoDonators && modTypes.HasFlag(ModCardType.Donator);
            bool disableRuina = ConfigManager.Instance.NoRuina && modTypes.HasFlag(ModCardType.Ruina);

            if (eventCard || disableDonators || disableRuina)
            {
                if (!metaTypes.HasFlag(CardHelper.CardMetaType.NonChoice))
                    metaTypes++;
            }

            // Create initial card info
            CardInfo cardInfo = CardHelper.CreateCardInfo
                (name, displayName, description,
                atk, hp, blood, bones, energy,
                portrait, emission, pixelTexture, altTexture, emissionAltTexture, titleTexture,
                abilities, specialAbilities, metaCategories, tribes, traits, appearances, decals, statIcon,
                choiceType, metaTypes, iceCubeName, evolveName, numTurns, onePerDeck, hideStats);

            if (face != null)
                cardInfo.animatedPortrait = face;

            // Add KillsSurvivors trait to cards with Deathtouch or Punisher
            if (cardInfo.HasAnyOfAbilities(Punisher.ability, Ability.Deathtouch))
                cardInfo.AddTraits(Trait.KillsSurvivors);

            if (risk != null)
                cardInfo.SetExtendedProperty("wstl:RiskLevel", risk);

            // since event cards can't be obtained normally, set 
            if (eventCard)
            {
                if (choiceType == CardHelper.CardChoiceType.Rare)
                    cardInfo.AddAppearances(RareEventBackground.appearance);
                else
                    cardInfo.AddAppearances(EventBackground.appearance);
            }

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

            CardManager.Add(pluginPrefix, cardInfo);
        }

        private static CardInfo SetNodeRestrictions(this CardInfo card, bool give, bool gain, bool buff, bool copy)
        {
            if (give)
                card.AddMetaCategories(CANNOT_GIVE_SIGILS);
            if (gain)
                card.AddMetaCategories(CANNOT_GAIN_SIGILS);
            if (buff)
                card.AddMetaCategories(CANNOT_BUFF_STATS);
            if (copy)
                card.AddMetaCategories(CANNOT_COPY_CARD);
            return card;
        }
    }
}
