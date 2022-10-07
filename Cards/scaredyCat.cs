using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ScaredyCat_F02115()
        {
            List<Ability> abilities = new()
            {
                Cowardly.ability
            };

            CardHelper.CreateCard(
                "wstl_scaredyCat", "Scaredy Cat",
                "A pitiful little cat.",
                0, 1, 1, 0,
                Artwork.scaredyCat, Artwork.scaredyCat_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "wstl_scaredyCatStrong");
        }
    }
}