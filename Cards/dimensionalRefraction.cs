using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void DimensionalRefraction_O0388()
        {
            List<Ability> abilities = new()
            {
                Ability.RandomAbility
            };

            WstlUtils.Add(
                "wstl_dimensionalRefraction", "Dimensional Refraction Variant",
                "A strange phenomenon. Or rather, the creature is the phenomena in and of itself.",
                4, 4, 3, 0,
                Resources.dimensionalRefraction,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.dimensionalRefraction_emission);
        }
    }
}