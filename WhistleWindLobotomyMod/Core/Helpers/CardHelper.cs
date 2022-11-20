using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using System;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core.Helpers;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core.Helpers
{
    public static class CardHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
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
            CardType cardType = CardType.None,
            MetaType metaTypes = 0,
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
            // Create empty lists if any of them are null
            abilities ??= new();
            specialAbilities ??= new();
            metaCategories ??= new();
            tribes ??= new();
            traits ??= new();
            appearances ??= new();
            decals ??= new();

            // Load textures
            Texture2D portraitTex = portrait != null ? TextureLoader.LoadTextureFromBytes(portrait) : null;
            Texture2D emissionTex = emission != null ? TextureLoader.LoadTextureFromBytes(emission) : null;
            Texture2D altTex = altTexture != null ? TextureLoader.LoadTextureFromBytes(altTexture) : null;
            Texture2D altEmissionTex = emissionAltTexture != null ? TextureLoader.LoadTextureFromBytes(emissionAltTexture) : null;
            Texture2D pixelTex = pixelTexture != null ? TextureLoader.LoadTextureFromBytes(pixelTexture) : null;
            Texture titleTex = titleTexture != null ? TextureLoader.LoadTextureFromBytes(titleTexture) : null;

            string risk = riskLevel switch
            {
                RiskLevel.Zayin => "Zayin",
                RiskLevel.Teth => "Teth",
                RiskLevel.He => "He",
                RiskLevel.Waw => "Waw",
                RiskLevel.Aleph => "Aleph",
                _ => null
            };

            // Create initial card info
            CardInfo cardInfo = ScriptableObject.CreateInstance<CardInfo>();

            cardInfo.name = name;
            cardInfo.SetBasic(displayName, atk, hp, description);
            cardInfo.SetBloodCost(blood).SetBonesCost(bones).SetEnergyCost(energy);

            if (emissionTex != null)
                cardInfo.SetPortrait(portraitTex, emissionTex);
            else if (portraitTex != null)
                cardInfo.SetPortrait(portraitTex);

            if (pixelTexture != null)
                cardInfo.SetPixelPortrait(pixelTex);

            if (altTex != null)
                cardInfo.SetAltPortrait(altTex);

            if (altEmissionTex != null)
                cardInfo.SetEmissiveAltPortrait(altEmissionTex);

            if (titleTexture != null)
                cardInfo.titleGraphic = titleTex;

            if (face != null)
                cardInfo.animatedPortrait = face;
            // Abilities
            cardInfo.abilities = abilities;
            cardInfo.specialAbilities = specialAbilities;

            if (statIcon != SpecialStatIcon.None)
                cardInfo.SetStatIcon(statIcon);

            // Internal values
            cardInfo.metaCategories = metaCategories;
            cardInfo.tribes = tribes;
            cardInfo.traits = traits;

            // Add KillsSurvivors trait to cards with Deathtouch or Punisher
            if (abilities.Exists((ab) => ab == Punisher.ability || ab == Ability.Deathtouch))
                cardInfo.AddTraits(Trait.KillsSurvivors);

            if (cardType != CardType.None)
                cardInfo.SetExtendedProperty("wstl:RiskLevel", risk);

            // Misc
            cardInfo.decals = decals;
            cardInfo.temple = CardTemple.Nature;
            cardInfo.cardComplexity = CardComplexity.Simple;
            cardInfo.appearanceBehaviour = appearances;
            cardInfo.onePerDeck = onePerDeck;
            cardInfo.hideAttackAndHealth = hideStats;

            // Sets the info for Ice Cube and Evolve, if present

            if (iceCubeName != null)
                cardInfo.SetIceCube(iceCubeName);

            if (evolveName != null)
                cardInfo.SetEvolve(evolveName, numTurns);

            bool disableDonators = ConfigManager.Instance.NoDonators && metaTypes.HasFlag(MetaType.Donator);

            bool eventCard = metaTypes.HasFlag(MetaType.EventCard);

            bool nonChoice = metaTypes.HasFlag(MetaType.NonChoice);

            // since event cards can't be obtained normally, set 
            if (eventCard)
            {
                if (cardType == CardType.Rare)
                    cardInfo.AddAppearances(RareEventBackground.appearance);
                else
                    cardInfo.AddAppearances(EventBackground.appearance);
            }
            else
            {
                switch (cardType)
                {
                    case CardType.Basic:
                        if (!disableDonators && !nonChoice)
                            cardInfo.SetDefaultPart1Card();
                        break;
                    case CardType.Rare:
                        cardInfo.SetRare();
                        if (disableDonators || nonChoice)
                            cardInfo.metaCategories.Remove(CardMetaCategory.Rare);
                        break;
                }
            }

            if (metaTypes.HasFlag(MetaType.Terrain))
            {
                cardInfo.SetTerrain();
                if (metaTypes.HasFlag(MetaType.NoTerrainLayout))
                    cardInfo.appearanceBehaviour.Remove(CardAppearanceBehaviour.Appearance.TerrainLayout);
                if (eventCard || cardType == CardType.Rare)
                    cardInfo.appearanceBehaviour.Remove(CardAppearanceBehaviour.Appearance.TerrainBackground);
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
            if (metaTypes.HasFlag(MetaType.Restricted))
                cardInfo.SetNodeRestrictions(true, true, true, true);

            CardManager.Add(pluginPrefix, cardInfo);
        }
        public static CardAppearanceBehaviourManager.FullCardAppearanceBehaviour CreateAppearance<T>(string name) where T : CardAppearanceBehaviour
        {
            return CardAppearanceBehaviourManager.Add(pluginGuid, name, typeof(T));
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

        public static CardMetaCategory CANNOT_GIVE_SIGILS = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CANNOT_GIVE_SIGILS");
        public static CardMetaCategory CANNOT_GAIN_SIGILS = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CANNOT_GAIN_SIGILS");
        public static CardMetaCategory CANNOT_BUFF_STATS = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CANNOT_BUFF_STATS");
        public static CardMetaCategory CANNOT_COPY_CARD = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CANNOT_COPY_CARD");
        public static CardMetaCategory SEPHIRAH_CARD = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "SEPHIRAH_CARD");

        public enum CardType
        {
            None,
            Basic,              // Default background, common choice
            Rare,               // Rare background, boss chest
        }
        [Flags]
        public enum MetaType
        {
            None = 0,               // Default, nothing special
            NonChoice = 1,          // Remove as choice option
            Terrain = 2,            // Terrain trait
            NoTerrainLayout = 4,    // No terrain layout
            Donator = 8,            // Donator class, can be removed from card choice
            EventCard = 16,         // Event background, only obtained through special methods
            Restricted = 32,        // Can't be used at any modification nodes
            Ruina = 64              // From Ruina expansion
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
    }
}
