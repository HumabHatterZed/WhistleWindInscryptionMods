using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ScaredyCatStrong_F02115()
        {
            List<Ability> abilities = new()
            {
                Cowardly.ability
            };

            CardHelper.CreateCard(
                "wstl_scaredyCatStrong", "Scaredy Cat",
                "A pitiful little cat.",
                2, 6, 3, 0,
                Artwork.scaredyCatStrong, Artwork.scaredyCatStrong_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "wstl_scaredyCat");
        }
    }
}