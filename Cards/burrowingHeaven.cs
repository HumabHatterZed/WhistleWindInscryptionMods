using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BurrowingHeaven_O0472()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp,
                Ability.WhackAMole
            };

            WstlUtils.Add(
                "wstl_burrowingHeaven", "The Burrowing Heaven",
                "Don't look away. Contain it in your sight.",
                0, 2, 0, 3,
                Resources.burrowingHeaven,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.burrowingHeaven_emission);
        }
    }
}