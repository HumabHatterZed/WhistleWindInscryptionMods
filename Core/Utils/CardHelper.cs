using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using DiskCardGame;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public static class CardHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        private const string modPrefix = "wstl";
        // Cards
        public static void CreateCard(
            string name,
            string displayName,
            string description,
            int baseAttack,
            int baseHealth,
            int bloodCost,
            int bonesCost,
            byte[] defaultTexture,
            byte[] emissionTexture = null,
            byte[] gbcTexture = null,
            byte[] altTexture = null,
            byte[] emissionAltTexture = null,
            byte[] titleTexture = null,
            List<Ability> abilities = null,
            List<SpecialTriggeredAbility> specialAbilities = null,
            List<CardMetaCategory> metaCategories = null,
            List<Tribe> tribes = null,
            List<Trait> traits = null,
            List<CardAppearanceBehaviour.Appearance> appearances = null,
            List<Texture> decals = null,
            string iceCubeName = null,
            string evolveName = null,
            int numTurns = 1,
            string tailName = null,
            byte[] tailTexture = null,
            int riskLevel = 0,
            bool isTerrain = false,
            bool isChoice = false,
            bool isRare = false,
            bool onePerDeck = false,
            bool hideStats = false,
            bool isDonator = false
            )
        {
            abilities ??= new();
            specialAbilities ??= new();
            metaCategories ??= new();
            tribes ??= new();
            traits ??= new();
            appearances ??= new();
            decals ??= new();

            Texture2D texture = WstlTextureHelper.LoadTextureFromResource(defaultTexture);
            Texture2D emissionTex = emissionTexture != null ? WstlTextureHelper.LoadTextureFromResource(emissionTexture) : null;
            Texture2D altTex = altTexture != null ? WstlTextureHelper.LoadTextureFromResource(altTexture) : null;
            Texture2D emissionAltTex = emissionAltTexture != null ? WstlTextureHelper.LoadTextureFromResource(emissionAltTexture) : null;
            Texture titleTex = titleTexture != null ? WstlTextureHelper.LoadTextureFromResource(titleTexture) : null;
            Texture2D gbcTex = gbcTexture != null ? WstlTextureHelper.LoadTextureFromResource(gbcTexture) : null;
            Texture2D tailTex = tailTexture != null ? WstlTextureHelper.LoadTextureFromResource(titleTexture) : null;

            string risk = riskLevel switch
            {
                1 => "Zayin",
                2 => "Teth",
                3 => "He",
                4 => "Waw",
                5 => "Aleph",
                _ => null
            };
            CardInfo cardInfo = ScriptableObject.CreateInstance<CardInfo>();
            cardInfo.SetPortrait(texture, emissionTex);
            cardInfo.name = name;
            cardInfo.displayedName = displayName;
            cardInfo.description = description;
            cardInfo.baseAttack = baseAttack;
            cardInfo.baseHealth = baseHealth;
            cardInfo.cost = bloodCost;
            cardInfo.bonesCost = bonesCost;
            cardInfo.abilities = abilities;
            cardInfo.specialAbilities = specialAbilities;
            cardInfo.metaCategories = metaCategories;
            cardInfo.tribes = tribes;
            cardInfo.traits = traits;
            cardInfo.appearanceBehaviour = appearances;
            cardInfo.decals = decals;
            cardInfo.cardComplexity = CardComplexity.Simple;
            cardInfo.temple = CardTemple.Nature;
            cardInfo.onePerDeck = onePerDeck;
            cardInfo.SetExtendedProperty("wstl:RiskLevel", risk);
            cardInfo.hideAttackAndHealth = hideStats;
            cardInfo.onePerDeck = onePerDeck;
            if (gbcTexture != null) { cardInfo.SetPixelPortrait(gbcTex); }
            if (altTex != null) { cardInfo.SetAltPortrait(altTex); }
            if (emissionAltTex != null) { cardInfo.SetEmissiveAltPortrait(emissionAltTex); }
            if (titleTexture != null) { cardInfo.titleGraphic = titleTex; }
            if (isTerrain) { cardInfo.SetTerrain(); }
            if (isChoice)
            {
                if (!isDonator || (isDonator && !ConfigUtils.Instance.NoDonators))
                {
                    cardInfo.SetDefaultPart1Card();
                    WstlPlugin.AllWstlModCards.Add(cardInfo);
                }
            }
            if (isRare)
            {
                if (isDonator && ConfigUtils.Instance.NoDonators)
                {
                    cardInfo.appearanceBehaviour = new() { CardAppearanceBehaviour.Appearance.RareCardBackground };
                }
                else
                {
                    cardInfo.SetRare();
                }
            }
            if (iceCubeName != null) { cardInfo.SetIceCube(iceCubeName); }
            if (evolveName != null) { cardInfo.SetEvolve(evolveName, numTurns); }
            if (tailName != null && tailTex != null) { cardInfo.SetTail(tailName, tailTex); }

            WstlPlugin.AllWstlModCards.Add(cardInfo);
            CardManager.Add(modPrefix, cardInfo);
        }
    }
}
