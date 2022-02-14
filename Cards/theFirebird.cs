using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
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
                Resources.theFirebird,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.theFirebird_emission);
        }
    }
}