using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public static class CardHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        // Cards
        public static void CreateCard(
            string name, string displayName,
            string description,
            int baseAttack, int baseHealth, int bloodCost, int bonesCost,
            byte[] defaultTexture, byte[] emissionTexture = null,
            byte[] gbcTexture = null,
            byte[] altTexture = null, byte[] emissionAltTexture = null,
            byte[] titleTexture = null,
            List<Ability> abilities = null,
            List<SpecialTriggeredAbility> specialAbilities = null,
            List<CardMetaCategory> metaCategories = null,
            List<Tribe> tribes = null,
            List<Trait> traits = null,
            SpecialStatIcon statIcon = SpecialStatIcon.None,
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
            cardInfo.SetPortrait(texture);
            if (emissionAltTex != null)
                cardInfo.SetEmissivePortrait(emissionTex);

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
            if (statIcon != SpecialStatIcon.None) { cardInfo.SetStatIcon(statIcon); }
            if (isTerrain) { cardInfo.SetTerrain(); }
            if (isChoice)
            {
                if (isDonator && ConfigManager.Instance.NoDonators)
                {

                }
                else
                {
                    cardInfo.SetDefaultPart1Card();
                }
            }
            if (isRare)
            {
                if (isDonator && ConfigManager.Instance.NoDonators)
                {
                    cardInfo.AddAppearances(CardAppearanceBehaviour.Appearance.RareCardBackground);
                }
                else
                {
                    cardInfo.SetRare();
                }
            }
            if (iceCubeName != null) { cardInfo.SetIceCube(iceCubeName); }
            if (evolveName != null) { cardInfo.SetEvolve(evolveName, numTurns); }
            if (tailName != null && tailTex != null) { cardInfo.SetTail(tailName, tailTex); }

            CardManager.Add("wstl", cardInfo);
            WstlPlugin.AllLobotomyCards.Add(cardInfo);
            if (cardInfo.metaCategories.Exists(x => x == CardMetaCategory.ChoiceNode || x == CardMetaCategory.TraderOffer || x == CardMetaCategory.Rare))
                WstlPlugin.ObtainableLobotomyCards.Add(cardInfo);
        }

        public static CardAppearanceBehaviourManager.FullCardAppearanceBehaviour CreateAppearance<T>(string name) where T : CardAppearanceBehaviour
        {
            return CardAppearanceBehaviourManager.Add(WstlPlugin.pluginGuid, name, typeof(T));
        }
    }
}
