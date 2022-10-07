using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using DiskCardGame;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Infiniscryption.Spells.Sigils;
using Infiniscryption.Spells.Patchers;

namespace WhistleWindLobotomyMod
{
    public static class CardHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static CardMetaCategory CANNOT_GIVE_SIGILS = GuidManager.GetEnumValue<CardMetaCategory>(WstlPlugin.pluginGuid, "CANNOT_GIVE_SIGILS");
        public static CardMetaCategory CANNOT_GAIN_SIGILS = GuidManager.GetEnumValue<CardMetaCategory>(WstlPlugin.pluginGuid, "CANNOT_GAIN_SIGILS");
        public static CardMetaCategory CANNOT_BUFF_STATS = GuidManager.GetEnumValue<CardMetaCategory>(WstlPlugin.pluginGuid, "CANNOT_BUFF_STATS");
        public static CardMetaCategory CANNOT_COPY_CARD = GuidManager.GetEnumValue<CardMetaCategory>(WstlPlugin.pluginGuid, "CANNOT_COPY_CARD");

        public enum ChoiceType
        {
            None,   // Is not obtainable
            Common, // Is common
            Rare    // Is rare
        }
        
        public enum MetaType
        {
            None,       // No special meta
            Event       // Remove from pool, restrict nodes
            NonChoice,  // Remove from pool
        }
        public enum TerrainType
        {
            None,
            Terrain,
            TerrainRare,
            TerrainAttack
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
        
        // Cards
        public static void CreateCard(
            string name, string displayName,
            string description,
            int baseAttack, int baseHealth,
            int bloodCost, int bonesCost,
            byte[] defaultTexture, byte[] emissionTexture,
            byte[] gbcTexture = null,
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
            ChoiceType cardType = ChoiceType.None,
            MetaType metaType = MetaType.None,
            TerrainType terrainType = TerrainType.None,
            RiskLevel riskLevel = RiskLevel.None,
            SpellType spellType = SpellType.None,
            bool isDonator = false,
            string iceCubeName = null,
            string evolveName = null,
            int numTurns = 1,
            string tailName = null,
            byte[] tailTexture = null,
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
            Texture2D texture = WstlTextureHelper.LoadTextureFromResource(defaultTexture);
            Texture2D emissionTex = emissionTexture != null ? WstlTextureHelper.LoadTextureFromResource(emissionTexture) : null;
            Texture2D altTex = altTexture != null ? WstlTextureHelper.LoadTextureFromResource(altTexture) : null;
            Texture2D emissionAltTex = emissionAltTexture != null ? WstlTextureHelper.LoadTextureFromResource(emissionAltTexture) : null;
            Texture2D gbcTex = gbcTexture != null ? WstlTextureHelper.LoadTextureFromResource(gbcTexture) : null;
            Texture2D tailTex = tailTexture != null ? WstlTextureHelper.LoadTextureFromResource(titleTexture) : null;
            Texture titleTex = titleTexture != null ? WstlTextureHelper.LoadTextureFromResource(titleTexture) : null;

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
            cardInfo.SetBasic(displayName, baseAttack, baseHealth, description);
            cardInfo.SetBloodCost(bloodCost);
            cardInfo.SetBonesCost(bonesCost);
            cardInfo.SetPortrait(texture, emissionTex);

            if (gbcTexture != null)
                cardInfo.SetPixelPortrait(gbcTex);

            if (altTex != null)
                cardInfo.SetAltPortrait(altTex);

            if (emissionAltTex != null)
                cardInfo.SetEmissiveAltPortrait(emissionAltTex);

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

            if (tailName != null && tailTex != null)
                cardInfo.SetTail(tailName, tailTex);

            bool disableDonator = isDonator && ConfigManager.Instance.NoDonators;
            bool noneChoice = metaType == MetaType.NonChoice || metaType == MetaType.Event;
            // Sets the card type (meta categories, appearances, etc.)
            if (cardType != CardType.None)
            {
                if (cardType == CardType.Rare)
                {
                    cardInfo.SetRare();
                    if (disableDonator || noneChoice)
                        cardInfo.metaCategories.Remove(CardMetaCategory.Rare);
                }
                else
                {
                    if (!disableDonator)
                        cardInfo.SetDefaultPart1Card();
                }   
            }
            
            // Set terrain
            if (terrainType != TerrainType.None)
            {
                cardInfo.SetTerrain();
                if (terrainType == TerrainType.TerrainRare)
                    cardInfo.appearanceBehaviour.Remove(CardAppearanceBehaviour.Appearance.TerrainCardBackground);

                if (terrainType == TerrainType.TerrainAttack)
                    cardInfo.appearanceBehaviour.Remove(CardAppearanceBehaviour.Appearance.TerrainLayout);
            }

            // Add KillsSurvivors trait to cards with Deathtouch or Punisher
            if (abilities.Exists((Ability ab) => ab == Punisher.ability || ab == Ability.Deathtouch) && !traits.Contains(Trait.KillsSurvivors))
                cardInfo.AddTraits(Trait.KillsSurvivors);

            if (spellType != SpellType.None)
            {
                if (spellType == SpellType.Global)
                {
                        cardInfo.SetGlobalSpell();
                }
                else
                {
                    cardInfo.SetTargetedSpell();
                    switch (spellType)
                    {
                            case SpellType.TargetedStats
                    }
                }
            }
                switch (spellType)
                {
                    case SpellType.Global:
                        cardInfo.SetGlobalSpell();
                        cardInfo.SetNodeRestrictions(true, true, true, true);
                        break;
                    case SpellType.Targeted:
                        cardInfo.SetTargetedSpell();
                        cardInfo.SetNodeRestrictions(true, true, true, true);
                        break;
                    case SpellType.TargetedStats:
                        cardInfo.SetTargetedShowStats();
                        cardInfo.SetNodeRestrictions(true, true, false, true);
                        break;
                    case SpellType.TargetedSigils:
                        cardInfo.SetTargetedSpell();
                        cardInfo.SetNodeRestrictions(true, false, true, true);
                        break;
                    case SpellType.TargetedStatsSigils:
                        cardInfo.SetTargetedShowStats();
                        cardInfo.SetNodeRestrictions(true, false, false, false);
                        break;
                };

            CardManager.Add("wstl", cardInfo);
        }
        public static CardAppearanceBehaviourManager.FullCardAppearanceBehaviour CreateAppearance<T>(string name) where T : CardAppearanceBehaviour
        {
            return CardAppearanceBehaviourManager.Add(WstlPlugin.pluginGuid, name, typeof(T));
        }
        public static CardInfo SetTargetedShowStats(this CardInfo card)
        {
            card.AddSpecialAbilities(TargetedSpellAbility.ID);
            card.specialStatIcon = TargetedSpellAbility.Icon;
            if (card.metaCategories.Contains(CardMetaCategory.Rare))
            {
                card.AddAppearances(SpellBehavior.RareSpellBackgroundAppearance.ID);
                card.appearanceBehaviour.Remove(CardAppearanceBehaviour.Appearance.RareCardBackground);
            }
            else
            {
                card.AddAppearances(SpellBehavior.SpellBackgroundAppearance.ID);
            }
            card.hideAttackAndHealth = false;
            return card;
        }

        public static CardInfo SetNodeRestrictions(this CardInfo card, bool give, bool gain, bool buff, bool copy)
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
    }
}
