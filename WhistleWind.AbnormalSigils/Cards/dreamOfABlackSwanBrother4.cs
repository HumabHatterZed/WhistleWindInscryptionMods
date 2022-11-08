using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_FourthBrother_F0270()
        {
            List<Ability> abilities = new()
            {
                Ability.Deathtouch
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.TerrainLayout
            };

            AbnormalCardHelper.CreateCard(
                "wstl_dreamOfABlackSwanBrother4", "Fourth Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                0, 2, 1, 0,
                Artwork.dreamOfABlackSwanBrother4, Artwork.dreamOfABlackSwanBrother4_emission,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances);
        }
    }
}