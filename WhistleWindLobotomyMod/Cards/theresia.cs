using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Theresia_T0909()
        {
            List<Ability> abilities = new()
            {
                Healer.ability
            };
            CardHelper.CreateCard(
                "wstl_theresia", "Theresia",
                "An old music box. It plays a familiar melody.",
                0, 2, 1, 0,
                Resources.theresia, Resources.theresia_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 2);
        }
    }
}