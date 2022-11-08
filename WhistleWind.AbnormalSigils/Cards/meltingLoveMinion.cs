using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_MeltingLoveMinion_D03109()
        {
            List<Ability> abilities = new()
            {
                Slime.ability
            };

            AbnormalCardHelper.CreateCard(
                "wstl_meltingLoveMinion", "Slime",
                "Don't let your beasts get too close now.",
                0, 0, 0, 0,
                Artwork.meltingLoveMinion, Artwork.meltingLoveMinion_emission,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}
