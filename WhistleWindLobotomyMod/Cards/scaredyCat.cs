using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ScaredyCat_F02115()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                Cowardly.specialAbility
            };
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_scaredyCatStrong", "Scaredy Cat",
                "",
                atk: 2, hp: 6,
                blood: 2, bones: 0, energy: 0,
                Artwork.scaredyCatStrong, Artwork.scaredyCatStrong_emission,
                specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "wstl_scaredyCat");

            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_scaredyCat", "Scaredy Cat",
                "A pitiful little cat.",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.scaredyCat, Artwork.scaredyCat_emission,
                specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "wstl_scaredyCatStrong");
        }
    }
}