using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_SnowWhitesVine_F0442()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp
            };

            AbnormalCardHelper.CreateCard(
                "wstl_snowWhitesVine", "Thorny Vines",
                "A vine.",
                0, 1, 0, 0,
                Artwork.snowWhitesVine, Artwork.snowWhitesVine_emission,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}