using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_SnowQueenIceBlock_F0137()
        {
            AbnormalCardHelper.CreateCard(
                "wstl_snowQueenIceBlock", "Block of Ice",
                "The palace was cold and lonely.",
                0, 1, 0, 0,
                Artwork.snowQueenIceBlock, Artwork.snowQueenIceBlock_emission,
                abilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}