using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TheNakedWorm_O0274()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            CardHelper.CreateCard(
                "wstl_theNakedWorm", "Naked Worm",
                "It can enter your body through any aperture.",
                1, 1, 0, 0,
                Resources.theNakedWorm, Resources.theNakedWorm_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}