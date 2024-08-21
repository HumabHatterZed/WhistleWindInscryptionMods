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
        public static CardInfo NewP03()
        public static CardInfo NewCard(
            string cardName, string displayName = null,
            string description = null,
            int attack = 0, int health = 0,
            int blood = 0, int bones = 0, int energy = 0,
            CardTemple temple = CardTemple.Nature)
        {
            return CardHelper.New(cardName, displayName, description, attack, health, blood, bones, energy, temple: temple);
        }

        public static CardInfo Build(
            this CardInfo cardInfo,
            ChoiceType choiceType = ChoiceType.None,
            RiskLevel riskLevel = RiskLevel.None,
            ModCardType cardType = ModCardType.None,
            bool nonChoice = false, bool gbc = false
            )
        {
            // if this is an event card
            bool eventCard = cardType.HasFlag(ModCardType.EventCard);
            bool riskDisabled = riskLevel != RiskLevel.None && DisabledRiskLevels.HasFlag(riskLevel);
            bool donatorDisabled = LobotomyConfigManager.Instance.NoDonators && cardType.HasFlag(ModCardType.Donator);
            bool ruinaDisabled = LobotomyConfigManager.Instance.NoRuina && cardType.HasFlag(ModCardType.Ruina);
            bool wonderLabDisabled = false && cardType.HasFlag(ModCardType.WonderLab);
            
            bool canBeObtained = choiceType != ChoiceType.None && !nonChoice && !AllCardsDisabled && !riskDisabled && !eventCard && !donatorDisabled && !ruinaDisabled && !wonderLabDisabled;

            // Add KillsSurvivors trait to cards with Deathtouch or Punisher
            if (cardInfo.HasAnyOfAbilities(Punisher.ability, Ability.Deathtouch))
                cardInfo.AddTraits(Trait.KillsSurvivors);

            // add the event card background
            if (eventCard)
            {
                cardInfo.AddAppearances(choiceType == ChoiceType.Rare ? RareEventBackground.appearance : EventBackground.appearance);
                cardInfo.RemoveAppearances(CardAppearanceBehaviour.Appearance.TerrainBackground);
            }
            else
            {
                cardInfo.SetChoiceType(choiceType, !canBeObtained);
            }

            if (canBeObtained)
            {
                // set risk level
                if (riskLevel != RiskLevel.None)
                    cardInfo.SetExtendedProperty("wstl:RiskLevel", riskLevel.ToString());

                if (gbc)
                {
                    if (LobotomyConfigManager.Instance.GBCPacks)
                        cardInfo.AddMetaCategories(CardMetaCategory.GBCPack, CardMetaCategory.GBCPlayable);

                    PixelLobotomyCards.Add(cardInfo);
                }
                else if (cardInfo.HasAnyOfCardMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.Rare))
                    Act1LobotomyCards.Add(cardInfo);
            }

            CardManager.Add(pluginPrefix, cardInfo);
            AllLobotomyCards.Add(cardInfo);
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
                card.AddTraits(AbnormalPlugin.CannotGiveSigils);
            if (cannotGainSigils)
                card.AddTraits(AbnormalPlugin.CannotGainSigils);
            if (cannotBuffStats)
                card.AddTraits(AbnormalPlugin.CannotBoostStats);
            if (cannotCopyCard)
                card.AddTraits(AbnormalPlugin.CannotCopyCard);
            return card;
        }

        public static readonly List<CardInfo> Act1LobotomyCards = new();
        public static readonly List<CardInfo> GBCLobotomyCards = new();
        public static readonly List<CardInfo> Act3LobotomyCards = new();
        public static readonly List<CardInfo> GrimoraLobotomyCards = new();
        public static readonly List<CardInfo> PixelLobotomyCards = new();

        public static readonly List<CardInfo> AllLobotomyCards = new();

        public static List<CardInfo> ObtainableLobotomyCards => new(SaveManager.SaveFile.IsPart1 ? Act1LobotomyCards : PixelLobotomyCards);

        public static Trait Apostle = GuidManager.GetEnumValue<Trait>(pluginGuid, "Apostle");
        public static Trait Sephirah = GuidManager.GetEnumValue<Trait>(pluginGuid, "Sephirah");
        public static Trait BlackForest = GuidManager.GetEnumValue<Trait>(pluginGuid, "BlackForest");
        public static Trait EmeraldCity = GuidManager.GetEnumValue<Trait>(pluginGuid, "EmeraldCity");
        public static Trait MagicalGirl = GuidManager.GetEnumValue<Trait>(pluginGuid, "MagicalGirl");
        public static Trait Executioner = GuidManager.GetEnumValue<Trait>(pluginGuid, "Executioner");

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
            Ruina = 4,          // From Ruina expansion
            WonderLab = 8       // From WonderLab expansion
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
