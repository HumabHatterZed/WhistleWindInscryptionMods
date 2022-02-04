using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void TheFirebird_O02101()
        {
            List<Ability> abilities = new List<Ability>
            {
                Burning.ability,
                Ability.Flying
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Bird
            };

            WstlUtils.Add(
                "wstl_theFirebird", "The Firebird",
                "A bird that longs for the thrill of being hunted.",
                3, 1, 2, 0,
                Resources.theFirebird,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.theFirebird_emission);
        }
    }
}