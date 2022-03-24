using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                Resources.dimensionalRefraction, Resources.dimensionalRefraction_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}