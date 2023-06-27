﻿using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class CardHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static CardInfo NewCard(
            bool addToAPI, string modPrefix,
            string cardName, string displayName = null, string description = null,
            int attack = 0, int health = 0,
            int blood = 0, int bones = 0, int energy = 0, List<GemType> gems = null)
        {
            if (addToAPI)
                return CardManager.New(modPrefix, cardName, displayName, attack, health, description).SetCost(blood, bones, energy, gems);
            else
            {
                CardInfo cardInfo = ScriptableObject.CreateInstance<CardInfo>()
                    .SetName(cardName, modPrefix)
                    .SetBasic(displayName, attack, health, description)
                    .SetCost(blood, bones, energy);
                return cardInfo;
            }
        }

        public static CardInfo SetPortraits(this CardInfo cardInfo,
            string portraitName, string emissionName = null, string pixelPortraitName = null,
            string altPortraitName = null, string altEmissionName = null, string titleName = null)
        {
            Texture2D portraitTex = TextureLoader.LoadTextureFromFile(portraitName);
            // if a custom emission name isn't provided, default to the filename [portraitName]_emission
            Texture2D emissionTex = TextureLoader.LoadTextureFromFile(emissionName ?? $"{portraitName}_emission");
            // if a custom pixel name isn't provided, default to the filename [portraitName]_pixel
            Texture2D pixelTex = TextureLoader.LoadTextureFromFile(pixelPortraitName ?? $"{portraitName}_pixel");
            Texture2D altTex = null, altEmissionTex = null;
            Texture2D titleTex = TextureLoader.LoadTextureFromFile(titleName);
            if (!string.IsNullOrEmpty(altPortraitName))
            {
                altTex = TextureLoader.LoadTextureFromFile(altPortraitName);
                altEmissionTex = TextureLoader.LoadTextureFromFile(altEmissionName ?? $"{altPortraitName}_emission");
            }
            if (portraitTex != null)
                cardInfo.SetPortrait(portraitTex);
            if (emissionTex != null)
                cardInfo.SetEmissivePortrait(emissionTex);
            if (pixelTex != null)
                cardInfo.SetPixelPortrait(pixelTex);
            if (altTex != null)
                cardInfo.SetAltPortrait(altTex);
            if (altEmissionTex != null)
                cardInfo.SetEmissiveAltPortrait(altEmissionTex);
            if (titleTex != null)
                cardInfo.titleGraphic = titleTex;

            return cardInfo;
        }

        public static CardInfo SetChoiceType(this CardInfo cardInfo, ChoiceType cardChoice, bool nonChoice = false)
        {
            if (cardChoice == ChoiceType.Common && !nonChoice)
                cardInfo.SetDefaultPart1Card();
            else if (cardChoice == ChoiceType.Rare)
            {
                cardInfo
                    .SetRare()
                    .SetCardTemple(CardTemple.Nature)
                    .RemoveAppearances(CardAppearanceBehaviour.Appearance.TerrainBackground);
                if (nonChoice)
                    cardInfo.RemoveCardMetaCategories(CardMetaCategory.Rare);
            }

            return cardInfo;
        }

        public static CardInfo SetEvolveInfo(this CardInfo cardInfo, string evolveName, int turnsToEvolve = 1)
        {
            if (evolveName.Contains("{0}"))
                cardInfo.defaultEvolutionName = string.Format(Localization.Translate(evolveName), cardInfo.DisplayedNameLocalized);
            else if (evolveName.Contains("[name]"))
                cardInfo.defaultEvolutionName = Localization.Translate(evolveName.Replace("[name]", ""));
            else
                cardInfo.SetEvolve(evolveName, turnsToEvolve);

            return cardInfo;
        }

        public static List<CardInfo> RemoveOwnedSingletons() => CardLoader.RemoveDeckSingletonsIfInDeck(CardManager.AllCardsCopy);
        public static CardAppearanceBehaviourManager.FullCardAppearanceBehaviour CreateAppearance<T>(string pluginGuid, string name)
            where T : CardAppearanceBehaviour
        {
            return CardAppearanceBehaviourManager.Add(pluginGuid, name, typeof(T));
        }

        public enum ChoiceType
        {
            None,
            Common,
            Rare
        }
    }
}
