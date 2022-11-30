using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_SnowQueenIceBlock_F0137()
        {
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_snowQueenIceBlock", "Block of Ice",
                "The palace was cold and lonely.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.snowQueenIceBlock,
                abilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}