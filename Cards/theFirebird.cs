using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TheFirebird_O02101()
        {
            List<Ability> abilities = new()
            {
                Burning.ability,
                Ability.Flying
            };

            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };

            WstlUtils.Add(
                "wstl_theFirebird", "The Firebird",
                "A bird that longs for the thrill of being hunted.",
                1, 3, 2, 0,
                Resources.theFirebird, Resources.theFirebird_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                isChoice: true);
        }
    }
}