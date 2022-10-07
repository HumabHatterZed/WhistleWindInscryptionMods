using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_TheNakedWorm_O0274()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };

            CardHelper.CreateCard(
                "wstl_theNakedWorm", "Naked Worm",
                "It can enter your body through any aperture.",
                1, 1, 0, 0,
                Artwork.theNakedWorm, Artwork.theNakedWorm_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}