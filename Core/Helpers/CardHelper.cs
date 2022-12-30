using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class CardHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        [Flags]
        public enum CardMetaType
        {
            None = 0,
            NonChoice = 1,          // Remove as choice option
            Terrain = 2,            // Terrain trait
            NoTerrainLayout = 4     // No terrain layout
        }
        public enum CardChoiceType
        {
            None,
            Basic,  // Default background, common choice
            Rare    // Rare background, boss chest
        }

        public static void CreateCard(
            string pluginPrefix, string name, string displayName,
            string description, int atk, int hp,
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
            CardChoiceType cardType = CardChoiceType.None,
            CardMetaType metaTypes = 0,
            string iceCubeName = null,
            string evolveName = null,
            int numTurns = 1,
            bool onePerDeck = false,
            bool hideStats = false
        )
        {
            CardInfo cardInfo = CreateCardInfo(
                name, displayName, description,
                atk, hp, blood, bones, energy,
                portrait, emission, pixelTexture,
                altTexture, emissionAltTexture, titleTexture,
                abilities, specialAbilities, metaCategories,
                tribes, traits, appearances, decals, statIcon,
                cardType, metaTypes,
                iceCubeName, evolveName, numTurns, onePerDeck, hideStats
                );

            CardManager.Add(pluginPrefix, cardInfo);
        }
        // Cards
        public static CardInfo CreateCardInfo(
            string name, string displayName,
            string description, int atk, int hp,
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
            CardChoiceType cardType = CardChoiceType.None,
            CardMetaType metaTypes = 0,
            string iceCubeName = null,
            string evolveName = null,
            int numTurns = 1,
            bool onePerDeck = false,
            bool hideStats = false
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

            bool nonChoice = metaTypes.HasFlag(CardMetaType.NonChoice);

            CardInfo cardInfo = ScriptableObject.CreateInstance<CardInfo>();

            cardInfo.name = name;
            cardInfo.SetBasic(displayName, atk, hp, description);
            cardInfo.SetBloodCost(blood).SetBonesCost(bones).SetEnergyCost(energy);

            if (portraitTex != null)
                cardInfo.SetPortrait(portraitTex);
            if (emissionTex != null)
                cardInfo.SetEmissivePortrait(emissionTex);
            if (pixelTexture != null)
                cardInfo.SetPixelPortrait(pixelTex);
            if (altTex != null)
                cardInfo.SetAltPortrait(altTex);
            if (altEmissionTex != null)
                cardInfo.SetEmissiveAltPortrait(altEmissionTex);
            if (titleTexture != null)
                cardInfo.titleGraphic = titleTex;

            // Abilities
            cardInfo.abilities = abilities;
            cardInfo.specialAbilities = specialAbilities;

            if (statIcon != SpecialStatIcon.None)
                cardInfo.SetStatIcon(statIcon);

            // Internal values
            cardInfo.metaCategories = metaCategories;
            cardInfo.tribes = tribes;
            cardInfo.traits = traits;

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

            switch (cardType)
            {
                case CardChoiceType.Basic:
                    if (!nonChoice)
                        cardInfo.SetDefaultPart1Card();
                    break;
                case CardChoiceType.Rare:
                    cardInfo.SetRare();
                    if (nonChoice)
                        cardInfo.metaCategories.Remove(CardMetaCategory.Rare);
                    break;
            }

            if (metaTypes.HasFlag(CardMetaType.Terrain))
            {
                cardInfo.SetTerrain();
                if (metaTypes.HasFlag(CardMetaType.NoTerrainLayout))
                    cardInfo.appearanceBehaviour.Remove(CardAppearanceBehaviour.Appearance.TerrainLayout);
                if (nonChoice || cardType == CardChoiceType.Rare)
                    cardInfo.appearanceBehaviour.Remove(CardAppearanceBehaviour.Appearance.TerrainBackground);
            }
            return cardInfo;
        }
        public static CardAppearanceBehaviourManager.FullCardAppearanceBehaviour CreateAppearance<T>(string pluginGuid, string name) where T : CardAppearanceBehaviour
        {
            return CardAppearanceBehaviourManager.Add(pluginGuid, name, typeof(T));
        }
    }
}
