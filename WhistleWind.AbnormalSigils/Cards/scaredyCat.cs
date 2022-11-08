using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_ScaredyCat_F02115()
        {
            List<Ability> abilities = new()
            {
                Cowardly.ability
            };

            AbnormalCardHelper.CreateCard(
                "wstl_scaredyCat", "Scaredy Cat",
                "A pitiful little cat.",
                0, 1, 1, 0,
                Artwork.scaredyCat, Artwork.scaredyCat_emission,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "wstl_scaredyCatStrong");
        }
    }
}