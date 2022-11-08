using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_ScaredyCatStrong_F02115()
        {
            List<Ability> abilities = new()
            {
                Cowardly.ability
            };

            AbnormalCardHelper.CreateCard(
                "wstl_scaredyCatStrong", "Scaredy Cat",
                "A pitiful little cat.",
                2, 6, 3, 0,
                Artwork.scaredyCatStrong, Artwork.scaredyCatStrong_emission,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "wstl_scaredyCat");
        }
    }
}