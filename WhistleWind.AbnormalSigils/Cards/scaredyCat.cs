using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

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

            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_scaredyCatStrong", "Scaredy Cat",
                "A pitiful little cat.",
                atk: 2, hp: 6,
                blood: 3, bones: 0, energy: 0,
                Artwork.scaredyCatStrong, Artwork.scaredyCatStrong_emission,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "wstl_scaredyCat");

            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_scaredyCat", "Scaredy Cat",
                "A pitiful little cat.",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.scaredyCat, Artwork.scaredyCat_emission,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "wstl_scaredyCatStrong");
        }
    }
}