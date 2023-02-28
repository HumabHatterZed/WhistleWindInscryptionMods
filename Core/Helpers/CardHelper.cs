using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class CardHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static CardInfo CreateCard(
            string modPrefix, string name, string displayName,
            string description, int atk, int hp,
            int blood, int bones, int energy,
            byte[] portrait = null, byte[] emission = null, byte[] pixelTexture = null,
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
            Texture2D portraitTex = TextureLoader.LoadTextureFromBytes(portrait);
            Texture2D emissionTex = TextureLoader.LoadTextureFromBytes(emission);
            Texture2D altTex = TextureLoader.LoadTextureFromBytes(altTexture);
            Texture2D altEmissionTex = TextureLoader.LoadTextureFromBytes(emissionAltTexture);
            Texture2D pixelTex = TextureLoader.LoadTextureFromBytes(pixelTexture);
            Texture titleTex = TextureLoader.LoadTextureFromBytes(titleTexture);

            bool nonChoice = metaTypes.HasFlag(CardMetaType.NonChoice);

            CardInfo cardInfo = CardManager.New(modPrefix, name, displayName, atk, hp, description);

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
            cardInfo.SetOnePerDeck(onePerDeck).SetHideStats(hideStats);

            // Sets the info for Ice Cube and Evolve, if present
            if (iceCubeName != null)
                cardInfo.SetIceCube(iceCubeName);

            if (evolveName != null)
            {
                if (evolveName.Contains("{0}"))                    
                    cardInfo.defaultEvolutionName = string.Format(Localization.Translate(evolveName), cardInfo.DisplayedNameLocalized);
                else if (evolveName.Contains("[name]"))
                    cardInfo.defaultEvolutionName = Localization.Translate(evolveName.Replace("[name]", ""));
                else
                    cardInfo.SetEvolve(evolveName, numTurns);
            }

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

            if (metaTypes.HasFlags(CardMetaType.Terrain, CardMetaType.Pelt))
            {
                if (metaTypes.HasFlag(CardMetaType.Pelt))
                    cardInfo.SetPelt(!metaTypes.HasFlag(CardMetaType.NoLice));

                if (metaTypes.HasFlag(CardMetaType.Terrain))
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

        [Flags]
        public enum CardMetaType
        {
            None = 0,
            NonChoice = 1,          // Remove as choice option
            Terrain = 2,            // Terrain trait
            NoTerrainLayout = 4,    // No terrain layout
            Pelt = 8,               // Pelt trait
            NoLice = 16             // No Lice
        }
        public enum CardChoiceType
        {
            None,
            Basic,  // Default background, common choice
            Rare    // Rare background, boss chest
        }
    }
}
