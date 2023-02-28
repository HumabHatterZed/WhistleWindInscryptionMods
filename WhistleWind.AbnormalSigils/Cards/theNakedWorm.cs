using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_TheNakedWorm_O0274()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_theNakedWorm", "Naked Worm",
                "It can enter your body through any aperture.",
                atk: 1, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.theNakedWorm, pixelTexture: Artwork.theNakedWorm_pixel,
                abilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}