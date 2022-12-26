using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void MagicalGirlSpade_O0173()
        {
            List<Ability> abilities = new()
            {
                Protector.ability
            };

            CardHelper.CreateCard(
                "wstl_magicalGirlSpade", "Magical Girl",
                "A loyal knight fighting to protect those close to her.",
                2, 4, 2, 0,
                Resources.magicalGirlSpade, Resources.magicalGirlSpade_emission, gbcTexture: Resources.magicalGirlSpade_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isRare: true, onePerDeck: true, riskLevel: 4);
        }
    }
}