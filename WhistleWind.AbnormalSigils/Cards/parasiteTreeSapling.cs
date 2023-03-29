using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_ParasiteTreeSapling_D04108()
        {
            List<Tribe> tribes = new() { TribeBotanic };

            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_parasiteTreeSapling", "Sapling",
                "",
                atk: 0, hp: 2,
                blood: 0, bones: 0, energy: 0,
                Artwork.parasiteTreeSapling, pixelTexture: Artwork.parasiteTreeSapling_pixel,
                abilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}