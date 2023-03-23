using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_SwanBrothers_F0270()
        {
            List<Tribe> tribes = new();

            if (TribalAPI.Enabled)
                tribes.Add(TribalAPI.AddTribal("anthropoid"));

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
                Artwork.dreamOfABlackSwanBrother1, pixelTexture: Artwork.dreamOfABlackSwanBrother1_pixel,
                abilities: new() { Ability.DoubleStrike },
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_dreamOfABlackSwanBrother2", "Second Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.dreamOfABlackSwanBrother2, pixelTexture: Artwork.dreamOfABlackSwanBrother2_pixel,
                abilities: new() { Piercing.ability },
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_dreamOfABlackSwanBrother3", "Third Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.dreamOfABlackSwanBrother3, pixelTexture: Artwork.dreamOfABlackSwanBrother3_pixel,
                abilities: new() { Reflector.ability },
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_dreamOfABlackSwanBrother4", "Fourth Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.dreamOfABlackSwanBrother4, pixelTexture: Artwork.dreamOfABlackSwanBrother4_pixel,
                abilities: new() { Ability.Deathtouch }, metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_dreamOfABlackSwanBrother5", "Fifth Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.dreamOfABlackSwanBrother5, pixelTexture: Artwork.dreamOfABlackSwanBrother5_pixel,
                abilities: new() { Burning.ability },
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_dreamOfABlackSwanBrother6", "Sixth Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.dreamOfABlackSwanBrother6, pixelTexture: Artwork.dreamOfABlackSwanBrother6_pixel,
                abilities: new() { ThickSkin.ability },
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
        }
    }
}