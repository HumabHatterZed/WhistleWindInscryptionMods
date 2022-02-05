using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BurrowingHeaven_O0472()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.Sharp,
                Ability.WhackAMole
            };

            WstlUtils.Add(
                "wstl_burrowingHeaven", "Red Shoes",
                "Don't look away. Contain it in your sight.",
                2, 0, 1, 0,
                Resources.burrowingHeaven,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.redShoes_emission);
        }
    }
}