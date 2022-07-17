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
            string name, string displayName, string description,
            int baseAttack, int baseHealth, int bloodCost, int bonesCost,
            byte[] defaultTexture, byte[] emissionTexture = null,
            byte[] altTexture = null, byte[] emissionAltTexture = null, byte[] titleTexture = null,
            List<Ability> abilities = null, List<SpecialTriggeredAbility> specialAbilities = null,
            List<CardMetaCategory> metaCategories = null, List<Tribe> tribes = null, List<Trait> traits = null,
            bool isTerrain = false, bool isChoice = false, bool isRare = false,
            List<CardAppearanceBehaviour.Appearance> appearances = null, List<Texture> decals = null,
            string iceCubeName = null, string evolveName = null, int numTurns = 1,
            string tailName = null, byte[] tailTexture = null,
            bool onePerDeck = false, bool hideStats = false,
            int riskLevel = 0, bool isDonator = false
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
            Texture2D emissionTex = null;
            Texture2D altTex = null;
            Texture2D emissionAltTex = null;
            Texture titleTex = null;
            Texture2D tailTex = null;

            if (emissionTexture != null) { emissionTex = WstlTextureHelper.LoadTextureFromResource(emissionTexture); }
            if (altTexture != null) { altTex = WstlTextureHelper.LoadTextureFromResource(altTexture); }
            if (emissionAltTexture != null) { emissionAltTex = WstlTextureHelper.LoadTextureFromResource(emissionAltTexture); }
            if (titleTexture != null) { titleTex = WstlTextureHelper.LoadTextureFromResource(titleTexture); }
            if (tailTexture != null) { tailTex = WstlTextureHelper.LoadTextureFromResource(titleTexture); }
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
            if (altTex != null) { cardInfo.SetAltPortrait(altTex); }
            //if (emissionAltTex != null) { cardInfo.SetEmissiveAltPortrait(emissionAltTex); }
            if (isTerrain) { cardInfo.SetTerrain(); }
            if (isChoice)
            {
                if (!isDonator || (isDonator && !ConfigUtils.Instance.NoDonators))
                {
                    cardInfo.SetDefaultPart1Card();
                }
            }
            if (isRare)
            {
                if (!isDonator || (isDonator && !ConfigUtils.Instance.NoDonators))
                {
                    cardInfo.SetRare();
                }
                else if (isDonator && ConfigUtils.Instance.NoDonators)
                {
                    cardInfo.appearanceBehaviour = new() { CardAppearanceBehaviour.Appearance.RareCardBackground };
                }
            }
            if (titleTexture != null) { cardInfo.titleGraphic = titleTex; }
            if (iceCubeName != null) { cardInfo.SetIceCube(iceCubeName); }
            if (evolveName != null) { cardInfo.SetEvolve(evolveName, numTurns); }
            if (tailName != null && tailTex != null) { cardInfo.SetTail(tailName, tailTex); }

            CardManager.Add(modPrefix, cardInfo);
        }
    }
}
