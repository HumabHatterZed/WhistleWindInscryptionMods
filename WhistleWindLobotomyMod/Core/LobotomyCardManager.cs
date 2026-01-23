using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using InscryptionAPI.Saves;
using System;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using static WhistleWind.Core.Helpers.CardHelper;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core {
    public static class LobotomyCardManager // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static CardInfo Build(
            this CardInfo cardInfo,
            CardType cardType = CardType.None,
            RiskLevel riskLevel = RiskLevel.None,
            bool availableInGBC = false,
            bool overrideCardChoice = false
            ) {
            switch (cardInfo.GetModPrefix()) {
                case pluginPrefix:
                    BaseModCards.Add(cardInfo);
                    AllLobotomyCards.Add(cardInfo);
                    break;
                case wonderlabPrefix:
                    WonderLabCards.Add(cardInfo);
                    AllLobotomyCards.Add(cardInfo);
                    break;
                case limbusPrefix:
                    LimbusCards.Add(cardInfo);
                    AllLobotomyCards.Add(cardInfo);
                    break;
                case pixelPrefix:
                    AllLobotomyPixelCards.Add(cardInfo);
                    break;
            }

            cardInfo.SetExtendedProperty("wstl:RiskLevel", riskLevel.ToString());
            if (cardInfo.HasAnyOfAbilities(Punisher.ability, Ability.Deathtouch)) {
                cardInfo.AddTraits(Trait.KillsSurvivors);
            }

            if (cardType != CardType.None && CardCanBeObtained(cardInfo)) {
                cardInfo.SetCardType(cardType, !overrideCardChoice);
                if (!overrideCardChoice) {
                    if (availableInGBC) {
                        if (LobotomyConfigManager.GBCPacks) {
                            cardInfo.AddMetaCategories(CardMetaCategory.GBCPack, CardMetaCategory.GBCPlayable);
                        }
                        ObtainableAct2Cards.Add(cardInfo);
                    }

                    switch (cardInfo.temple) {
                        case CardTemple.Nature:
                            ObtainableAct1Cards.Add(cardInfo);
                            break;
                        case CardTemple.Undead:
                            ObtainableActGCards.Add(cardInfo);
                            break;
                        case CardTemple.Tech:
                            ObtainableAct3Cards.Add(cardInfo);
                            break;
                        case CardTemple.Wizard:
                            ObtainableActMCards.Add(cardInfo);
                            break;
                    }
                }
            }

            return cardInfo;
        }

        public static bool CardIsDisabled(CardInfo info) {
            if (info.HasCardMetaCategory(EventCard) && LobotomyConfigManager.NoEvents)
                return true;

            if (info.HasCardMetaCategory(DonatorCard) && LobotomyConfigManager.NoDonators)
                return true;

            if (info.HasCardMetaCategory(RuinaCard) && LobotomyConfigManager.NoRuina)
                return true;

            RiskLevel riskLevel = info.GetRiskLevel();
            if (riskLevel != RiskLevel.None && DisabledRiskLevels.HasFlag(riskLevel))
                return true;

            return false;
        }
        private static bool CardCanBeObtained(CardInfo info) {
            if (info.HasCardMetaCategory(EventCard))
                return false;

            return !AllCardsDisabled && !CardIsDisabled(info);
        }

        public static CardInfo SetOrdealCard(this CardInfo info, OrdealType type) {
            switch (type) {
                case OrdealType.Green:
                    if (info.HasTrait(Trait.Terrain)) {
                        info.AddAppearances(OrdealBackgroundGreenTerrain.appearance);
                    }
                    else {
                        info.AddAppearances(OrdealBackgroundGreen.appearance);
                    }
                    break;
                case OrdealType.Crimson:
                    info.AddAppearances(OrdealBackgroundCrimson.appearance);
                    break;
                case OrdealType.Violet:
                    if (info.HasTrait(Trait.Terrain)) {
                        info.AddAppearances(OrdealBackgroundVioletTerrain.appearance);
                    }
                    else {
                        info.AddAppearances(OrdealBackgroundViolet.appearance);
                    }
                    break;
                case OrdealType.Amber:
                    info.AddAppearances(OrdealBackgroundAmber.appearance);
                    break;
                case OrdealType.Indigo:
                    if (info.HasTrait(Trait.Terrain)) {
                        info.AddAppearances(OrdealBackgroundIndigoTerrain.appearance);
                    }
                    else {
                        info.AddAppearances(OrdealBackgroundIndigo.appearance);
                    }
                    break;
                case OrdealType.White:
                    info.AddAppearances(OrdealBackgroundWhite.appearance);
                    break;
            }
            info.RemoveAppearances(CardAppearanceBehaviour.Appearance.TerrainBackground);
            info.AddTraits(Ordeal);
            return info;
        }
        public static CardInfo SetEventCard(this CardInfo info, bool isRare) {
            info.AddAppearances(isRare ? RareEventBackground.appearance : EventBackground.appearance);
            info.RemoveAppearances(CardAppearanceBehaviour.Appearance.TerrainBackground);
            info.AddMetaCategories(EventCard);
            return info;
        }

        public static CardInfo SetNodeRestrictions(this CardInfo card, bool cannotGiveSigils, bool cannotGainSigils, bool cannotBuffStats, bool cannotCopyCard) {
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

        public static CardInfo SetSpellType(this CardInfo cardInfo, SpellType spellType) {
            string spellName = spellType.ToString();
            bool isGlobal = spellName.StartsWith("Global");
            bool isStatSpell = spellName.Contains("Stats");
            if (isGlobal)
                cardInfo.SetGlobalSpell();
            else
                cardInfo.SetTargetedSpell();

            cardInfo.hideAttackAndHealth = !isStatSpell;
            cardInfo.SetNodeRestrictions(
                cannotGiveSigils: isGlobal,
                cannotGainSigils: !spellName.Contains("Sigils"),
                cannotBuffStats: !isStatSpell,
                cannotCopyCard: isGlobal);
            return cardInfo;
        }

        public static RiskLevel GetRiskLevel(this CardInfo info) {
            return info.GetExtendedProperty("wstl:RiskLevel") switch {
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
        public static readonly List<CardInfo> AllLobotomyPixelCards = new();

        private static readonly List<CardInfo> ObtainableAct1Cards = new();
        private static readonly List<CardInfo> ObtainableAct2Cards = new();
        private static readonly List<CardInfo> ObtainableAct3Cards = new();
        private static readonly List<CardInfo> ObtainableActGCards = new();
        private static readonly List<CardInfo> ObtainableActMCards = new();

        private static readonly List<CardInfo> _obtainableCards = new();
        public static List<CardInfo> ObtainableLobotomyCards {
            get {
                if (_obtainableCards.Count == 0)
                    _obtainableCards.Add(CardLoader.GetCardByName(Cards.trainingDummy));

                if (AllCardsDisabled) {
                    return _obtainableCards;
                }

                CardTemple? currentAct = SaveManager.SaveFile.IsPart2 ? CardTemple.NUM_TEMPLES : SaveManager.SaveFile.GetSceneAsCardTemple();

                return currentAct switch {
                    CardTemple.Nature => ObtainableAct1Cards,
                    CardTemple.NUM_TEMPLES => ObtainableAct2Cards,
                    CardTemple.Tech => ObtainableAct3Cards,
                    CardTemple.Undead => ObtainableActGCards,
                    CardTemple.Wizard => ObtainableActMCards,
                    _ => _obtainableCards
                };
            }
        }

        public static Trait Ordeal = GuidManager.GetEnumValue<Trait>(LobotomyPlugin.pluginGuid, "Ordeal");
        public static Trait Apostle = GuidManager.GetEnumValue<Trait>(LobotomyPlugin.pluginGuid, "Apostle");
        public static Trait Sephirah = GuidManager.GetEnumValue<Trait>(LobotomyPlugin.pluginGuid, "Sephirah");
        public static Trait Executioner = GuidManager.GetEnumValue<Trait>(LobotomyPlugin.pluginGuid, "Executioner");
        public static Trait BlackForest = GuidManager.GetEnumValue<Trait>(LobotomyPlugin.pluginGuid, "BlackForest");
        public static Trait EmeraldCity = GuidManager.GetEnumValue<Trait>(LobotomyPlugin.pluginGuid, "EmeraldCity");
        public static Trait MagicalGirl = GuidManager.GetEnumValue<Trait>(LobotomyPlugin.pluginGuid, "MagicalGirl");
        public static Trait PriorityMovement = GuidManager.GetEnumValue<Trait>(LobotomyPlugin.pluginGuid, "PriorityMovement");

        public static CardMetaCategory RuinaCard = GuidManager.GetEnumValue<CardMetaCategory>(LobotomyPlugin.pluginGuid, "RuinaCard");
        public static CardMetaCategory EventCard = GuidManager.GetEnumValue<CardMetaCategory>(LobotomyPlugin.pluginGuid, "EventCard");
        public static CardMetaCategory DonatorCard = GuidManager.GetEnumValue<CardMetaCategory>(LobotomyPlugin.pluginGuid, "DonatorCard");

        [Flags]
        public enum RiskLevel {
            None = 0,
            Zayin = 1,
            Teth = 2,
            He = 4,
            Waw = 8,
            Aleph = 16,
            All = 32
        }

        public enum SpellType {
            None,
            Global,
            GlobalStats,
            GlobalSigils,
            Targeted,
            TargetedStats,
            TargetedSigils,
            TargetedStatsSigils
        }
    }
}
