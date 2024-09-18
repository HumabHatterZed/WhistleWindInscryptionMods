using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using InscryptionAPI.Slots;
using System;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.Core.Helpers.CardHelper;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core
{
    public static class LobotomyCardManager // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static CardInfo Build(
            this CardInfo cardInfo,
            CardType cardType = CardType.None,
            RiskLevel riskLevel = RiskLevel.None,
            bool availableInGBC = false,
            bool overrideCardChoice = false
            )
        {
            cardInfo.SetExtendedProperty("wstl:RiskLevel", riskLevel.ToString());
            if (cardInfo.HasAnyOfAbilities(Punisher.ability, Ability.Deathtouch))
                cardInfo.AddTraits(Trait.KillsSurvivors);

            if (CardCanBeObtained(cardInfo))
            {
                cardInfo.SetCardType(cardType, !overrideCardChoice);
                if (!overrideCardChoice && availableInGBC && LobotomyConfigManager.Instance.GBCPacks)
                {
                    cardInfo.AddMetaCategories(CardMetaCategory.GBCPack, CardMetaCategory.GBCPlayable);
                }
            }

            AllLobotomyCards.Add(cardInfo);
            switch (cardInfo.GetModPrefix())
            {
                case pluginPrefix:
                    BaseModCards.Add(cardInfo);
                    break;
                case wonderlabPrefix:
                    WonderLabCards.Add(cardInfo);
                    break;
                case limbusPrefix:
                    LimbusCards.Add(cardInfo);
                    break;
            }
            return cardInfo;
        }

        public static bool CardIsDisabled(CardInfo info)
        {
            if (info.HasCardMetaCategory(EventCard) && LobotomyConfigManager.Instance.NoEvents)
                return true;

            if (info.HasCardMetaCategory(DonatorCard) && LobotomyConfigManager.Instance.NoDonators)
                return true;

            if (info.HasCardMetaCategory(RuinaCard) && LobotomyConfigManager.Instance.NoRuina)
                return true;

            RiskLevel riskLevel = info.GetRiskLevel();
            if (riskLevel != RiskLevel.None && DisabledRiskLevels.HasFlag(riskLevel))
                return true;

            return false;
        }
        private static bool CardCanBeObtained(CardInfo info)
        {
            if (info.HasCardMetaCategory(EventCard))
                return false;

            return !AllCardsDisabled && !CardIsDisabled(info);
        }

        public static CardInfo SetEventCard(this CardInfo info, bool isRare)
        {
            info.AddAppearances(isRare ? RareEventBackground.appearance : EventBackground.appearance);
            info.RemoveAppearances(CardAppearanceBehaviour.Appearance.TerrainBackground);
            info.AddMetaCategories(EventCard);
            return info;
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

        public static CardInfo SetSpellType(this CardInfo cardInfo, SpellType spellType)
        {
            string spellName = spellType.ToString();
            bool isGlobal = spellName.StartsWith("Global");
            bool isStatSpell = spellName.EndsWith("Stats");
            if (isGlobal)
                cardInfo.SetGlobalSpell();
            else
                cardInfo.SetTargetedSpell();

            cardInfo.hideAttackAndHealth = !isStatSpell;
            cardInfo.SetNodeRestrictions(isGlobal, !spellName.EndsWith("Sigils"), !isStatSpell, isGlobal);
            return cardInfo;
        }

        public static RiskLevel GetRiskLevel(this CardInfo info)
        {
            return info.GetExtendedProperty("wstl:RiskLevel") switch
            {
                "Aleph" => RiskLevel.Aleph,
                "Waw" => RiskLevel.Waw,
                "He" => RiskLevel.He,
                "Teth" => RiskLevel.Teth,
                "Zayin" => RiskLevel.Zayin,
                _ => RiskLevel.None
            };
        }

        public static readonly List<CardInfo> BaseModCards = new();
        public static readonly List<CardInfo> WonderLabCards = new();
        public static readonly List<CardInfo> LimbusCards = new();

        public static readonly List<CardInfo> AllLobotomyCards = new();
        public static List<CardInfo> ObtainableLobotomyCards { get; internal set; } = new();

        public static Trait Ordeal = GuidManager.GetEnumValue<Trait>(pluginGuid, "Ordeal");
        public static Trait Apostle = GuidManager.GetEnumValue<Trait>(pluginGuid, "Apostle");
        public static Trait Sephirah = GuidManager.GetEnumValue<Trait>(pluginGuid, "Sephirah");
        public static Trait Executioner = GuidManager.GetEnumValue<Trait>(pluginGuid, "Executioner");
        public static Trait BlackForest = GuidManager.GetEnumValue<Trait>(pluginGuid, "BlackForest");
        public static Trait EmeraldCity = GuidManager.GetEnumValue<Trait>(pluginGuid, "EmeraldCity");
        public static Trait MagicalGirl = GuidManager.GetEnumValue<Trait>(pluginGuid, "MagicalGirl");

        public static CardMetaCategory RuinaCard = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "RuinaCard");
        public static CardMetaCategory EventCard = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "EventCard");
        public static CardMetaCategory DonatorCard = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "DonatorCard");

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
            GlobalStats,
            GlobalSigils,
            Targeted,
            TargetedStats,
            TargetedSigils
        }
    }
}
