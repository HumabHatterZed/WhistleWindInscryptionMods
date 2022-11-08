using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_SecondBrother_F0270()
        {
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.TerrainLayout
            };
            AbnormalCardHelper.CreateCard(
                "wstl_dreamOfABlackSwanBrother2", "Second Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                1, 1, 1, 0,
                Artwork.dreamOfABlackSwanBrother2, Artwork.dreamOfABlackSwanBrother2_emission,
                abilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances);
        }
    }
}