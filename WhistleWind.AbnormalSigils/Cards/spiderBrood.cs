using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_SpiderBrood_O0243()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedRed.appearance
            };

            AbnormalCardHelper.CreateCard(
                "wstl_spiderBrood", "Spider Brood",
                "Big and mean.",
                1, 2, 1, 0,
                Artwork.spiderBrood, Artwork.spiderBrood_emission,
                abilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
        }
    }
}