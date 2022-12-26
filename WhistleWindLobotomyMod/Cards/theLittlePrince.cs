using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TheLittlePrince_O0466()
        {
            List<Ability> abilities = new()
            {
                Spores.ability
            };

            CardHelper.CreateCard(
                "wstl_theLittlePrince", "The Little Prince",
                "A giant mushroom chunk. A mist of spores surrounds it.",
                1, 4, 2, 0,
                Resources.theLittlePrince, Resources.theLittlePrince_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 4);
        }
    }
}