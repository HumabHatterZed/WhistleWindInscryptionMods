using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

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

            AbnormalCardHelper.CreateCard(
                "wstl_theNakedWorm", "Naked Worm",
                "It can enter your body through any aperture.",
                1, 1, 0, 0,
                Artwork.theNakedWorm, Artwork.theNakedWorm_emission,
                abilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}