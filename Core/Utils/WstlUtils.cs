using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public static class WstlUtils
    {
        // Originally taken from GrimoraMod and SigilADay_julienperge
        #region Cards
        public static void Add(
            string name,
            string displayName,
            string description,
            int baseAttack,
            int baseHealth,
            int bloodCost,
            int bonesCost,
            byte[] defaultTexture,
            byte[] emissionTexture = null,
            byte[] altTexture = null,
            byte[] emissionAltTexture = null,
            byte[] titleTexture = null,
            List<Ability> abilities = null,
            List<SpecialTriggeredAbility> specialAbilities = null,
            List<CardMetaCategory> metaCategories = null,
            List<Tribe> tribes = null,
            List<Trait> traits = null,
            bool isTerrain = false,
            bool isChoice = false,
            bool isRare = false,
            List<CardAppearanceBehaviour.Appearance> appearances = null,
            List<Texture> decals = null,
            string iceCubeName = null,
            string evolveName = null,
            int numTurns = 1,
            string tailName = null,
            byte[] tailTexture = null,
            int riskLevel = 0,
            bool onePerDeck = false
            )
        {
            abilities ??= new();
            specialAbilities ??= new();
            metaCategories ??= new();
            tribes ??= new();
            traits ??= new();
            appearances ??= new();
            decals ??= new();

            Texture2D texture = ImageUtils.LoadTextureFromResource(defaultTexture);
            Texture2D emissionTex = null;
            Texture2D altTex = null;
            Texture2D emissionAltTex = null;
            Texture titleTex = null;
            Texture2D tailTex = null;

            if (emissionTexture != null) {
                emissionTex = ImageUtils.LoadTextureFromResource(emissionTexture);
            }
            if (altTexture != null) {
                altTex = ImageUtils.LoadTextureFromResource(altTexture);
            }
            if (emissionAltTexture != null)
            {
                emissionAltTex = ImageUtils.LoadTextureFromResource(emissionAltTexture);
            }
            if (titleTexture != null) {
                titleTex = ImageUtils.LoadTextureFromResource(titleTexture);
            }
            if (tailTexture != null) {
                tailTex = ImageUtils.LoadTextureFromResource(titleTexture);
            }

            string risk = riskLevel switch
            {
                1 => "TETH",
                2 => "HE",
                3 => "WAW",
                4 => "ALEPH",
                _ => "ZAYIN",
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
            cardInfo.SetExtendedProperty("RiskLevel", risk);

            if (altTex != null)
            {
                cardInfo.SetAltPortrait(altTex);
            }
            //if (emissionAltTex != null)
            //{
            //    cardInfo.SetEmissiveAltPortrait(emissionAltTex);
            //}
            if (isTerrain)
            {
                cardInfo.SetTerrain();
            }
            if (isChoice)
            {
                cardInfo.SetDefaultPart1Card();
            }
            if (isRare)
            {
                cardInfo.SetRare();
            }
            if (titleTexture != null)
            {
                cardInfo.titleGraphic = titleTex;
            }
            if (iceCubeName != null)
            {
                cardInfo.SetEvolve(iceCubeName, numTurns);
            }
            if (evolveName != null)
            {
                cardInfo.SetEvolve(evolveName, numTurns);
            }
            if (tailName != null)
            {
                cardInfo.SetTail(tailName, tailTex);
            }
            cardInfo.onePerDeck = onePerDeck;

            CardManager.Add(WstlPlugin.modPrefix, cardInfo);
        }
        #endregion

        #region Abilities
        public static DialogueEvent.LineSet SetAbilityInfoDialogue(string dialogue)
        {
            return new DialogueEvent.LineSet(new List<DialogueEvent.Line>()
                {
                    new()
                    {
                        text = dialogue
                    }
                }
            );
        }

        public static AbilityManager.FullAbility CreateAbility<T>(
            byte[] texture,
            string rulebookName,
            string rulebookDescription,
            string dialogue,
            int powerLevel = 0,
            bool addModular = false,
            bool isPassive = false,
            bool canStack = false,
            bool opponent = false,
            bool flipY = false,
            byte[] customY = null,
            bool overrideModular = false)
            where T : AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            Texture2D tex = ImageUtils.LoadTextureFromResource(texture);
            Texture2D flippedTex = null;
            if (customY != null)
            {
                flippedTex = ImageUtils.LoadTextureFromResource(customY);
            }

            List<AbilityMetaCategory> list = new()
            {
                AbilityMetaCategory.Part1Rulebook
            };
            if ((addModular || ConfigUtils.Instance.AllModular) && !overrideModular)
            {
                list.Add(AbilityMetaCategory.Part1Modular);
            }

            info.rulebookName = rulebookName;
            info.rulebookDescription = rulebookDescription;
            info.powerLevel = powerLevel;
            info.passive = isPassive;
            info.canStack = canStack;
            info.opponentUsable = opponent;
            info.flipYIfOpponent = flipY;
            info.metaCategories = list;
            info.abilityLearnedDialogue = SetAbilityInfoDialogue(dialogue);
            if (flippedTex != null)
            {
                info.SetCustomFlippedTexture(flippedTex);
            }

            return AbilityManager.Add(WstlPlugin.modPrefix, info, typeof(T), tex);
        }

        #endregion

        #region SpecialAbilities
        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreateSpecialAbility<T>(string rulebookName, string rulebookDesc)
            where T : SpecialCardBehaviour
        {
            return SpecialTriggeredAbilityManager.Add(WstlPlugin.modPrefix, rulebookName, typeof(T));
        }
        #endregion
    }
}