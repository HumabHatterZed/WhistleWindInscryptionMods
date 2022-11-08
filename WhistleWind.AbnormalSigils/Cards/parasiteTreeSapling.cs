using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_ParasiteTreeSapling_D04108()
        {
            AbnormalCardHelper.CreateCard(
                "wstl_parasiteTreeSapling", "Sapling",
                "They proliferate and become whole. Can you feel it?",
                0, 2, 0, 0,
                Artwork.parasiteTreeSapling, Artwork.parasiteTreeSapling_emission,
                abilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}