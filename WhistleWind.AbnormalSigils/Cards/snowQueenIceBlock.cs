using DiskCardGame;
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
                "",
                atk: 0, hp: 2,
                blood: 0, bones: 0, energy: 0,
                Artwork.snowQueenIceBlock, pixelTexture: Artwork.snowQueenIceBlock_pixel,
                abilities: new() { Ability.Reach },
                metaCategories: new(), tribes: new(), traits: new(),
                metaTypes: CardHelper.CardMetaType.Terrain);
        }
    }
}