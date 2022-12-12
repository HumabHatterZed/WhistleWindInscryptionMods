using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_FirstBrother_F0270()
        {
            List<Ability> abilities1 = new() { Ability.DoubleStrike };
            List<Ability> abilities2 = new() { Piercing.ability };
            List<Ability> abilities3 = new() { Reflector.ability };
            List<Ability> abilities4 = new() { Ability.Deathtouch };
            List<Ability> abilities5 = new() { Burning.ability };
            List<Ability> abilities6 = new() { ThickSkin.ability };

            List<Tribe> tribes = new();
            if (TribalAPI.Enabled)
                TribalAPI.AddTribalTribe(tribes, "humanoid");
            
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.TerrainLayout
            };
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_dreamOfABlackSwanBrother1", "First Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.dreamOfABlackSwanBrother1,
                abilities: abilities1,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_dreamOfABlackSwanBrother2", "Second Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.dreamOfABlackSwanBrother2,
                abilities: abilities2,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_dreamOfABlackSwanBrother3", "Third Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.dreamOfABlackSwanBrother3,
                abilities: abilities3,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_dreamOfABlackSwanBrother4", "Fourth Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.dreamOfABlackSwanBrother4,
                abilities: abilities4, metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_dreamOfABlackSwanBrother5", "Fifth Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.dreamOfABlackSwanBrother5,
                abilities: abilities5,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_dreamOfABlackSwanBrother6", "Sixth Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.dreamOfABlackSwanBrother6,
                abilities: abilities6,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
        }
    }
}