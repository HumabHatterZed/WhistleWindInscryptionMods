using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using UnityEngine;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;

namespace WhistleWind.AbnormalSigils.Core.Helpers
{
    public static class AbnormalCardHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        // Cards
        public static void CreateCard(
            string name, string displayName,
            string description,
            int attack, int health,
            int bloodCost, int bonesCost,
            byte[] portrait, byte[] emission = null, byte[] pixelTexture = null,
            List<Ability> abilities = null,
            List<CardMetaCategory> metaCategories = null,
            List<Tribe> tribes = null,
            List<Trait> traits = null,
            List<CardAppearanceBehaviour.Appearance> appearances = null,
            SpecialStatIcon statIcon = SpecialStatIcon.None,
            List<MetaType> metaTypes = null,
            string evolveName = null,
            int numTurns = 1
            )
        {
            // Create empty lists if any of them are null
            abilities ??= new();
            metaCategories ??= new();
            tribes ??= new();
            traits ??= new();
            appearances ??= new();
            metaTypes ??= new();

            // Load textures
            Texture2D portraitTex = TextureLoader.LoadTextureFromBytes(portrait);
            Texture2D emissionTex = emission != null ? TextureLoader.LoadTextureFromBytes(emission) : null;
            Texture2D pixelTex = pixelTexture != null ? TextureLoader.LoadTextureFromBytes(pixelTexture) : null;

            // Create initial card info
            CardInfo cardInfo = ScriptableObject.CreateInstance<CardInfo>();

            cardInfo.name = name;
            cardInfo.SetBasic(displayName, attack, health, description);
            cardInfo.SetBloodCost(bloodCost).SetBonesCost(bonesCost);

            if (emissionTex != null)
                cardInfo.SetPortrait(portraitTex, emissionTex);
            else
                cardInfo.SetPortrait(portraitTex);

            if (pixelTexture != null)
                cardInfo.SetPixelPortrait(pixelTex);

            // Abilities
            cardInfo.abilities = abilities;

            if (statIcon != SpecialStatIcon.None)
                cardInfo.SetStatIcon(statIcon);

            // Internal values
            cardInfo.metaCategories = metaCategories;
            cardInfo.tribes = tribes;
            cardInfo.traits = traits;

            // Misc
            cardInfo.temple = CardTemple.Nature;
            cardInfo.cardComplexity = CardComplexity.Simple;
            cardInfo.appearanceBehaviour = appearances;

            if (evolveName != null)
                cardInfo.SetEvolve(evolveName, numTurns);

            if (metaTypes.Contains(MetaType.Terrain))
            {
                cardInfo.SetTerrain();
                if (metaTypes.Contains(MetaType.NoTerrainLayout))
                    cardInfo.appearanceBehaviour.Remove(CardAppearanceBehaviour.Appearance.TerrainLayout);
            }

            CardManager.Add(pluginPrefix, cardInfo);
        }
        public static CardAppearanceBehaviourManager.FullCardAppearanceBehaviour CreateAppearance<T>(string name)
            where T : CardAppearanceBehaviour => CardAppearanceBehaviourManager.Add(pluginGuid, name, typeof(T));

        public enum MetaType
        {
            None,               // Default, nothing special
            Terrain,            // Terrain trait
            NoTerrainLayout     // No terrain layout
        }
    }
}
