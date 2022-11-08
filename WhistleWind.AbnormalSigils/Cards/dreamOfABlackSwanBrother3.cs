using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_ThirdBrother_F0270()
        {
            List<Ability> abilities = new()
            {
                Reflector.ability
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.TerrainLayout
            };

            AbnormalCardHelper.CreateCard(
                "wstl_dreamOfABlackSwanBrother3", "Third Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                0, 3, 1, 0,
                Artwork.dreamOfABlackSwanBrother3, Artwork.dreamOfABlackSwanBrother3_emission,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances);
        }
    }
}