using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void DontTouchMe_O0547()
        {
            List<Ability> abilities = new()
            {
                Punisher.ability,
                Ability.GuardDog
            };

            CardHelper.CreateCard(
                "wstl_dontTouchMe", "Don't Touch Me",
                "Don't touch it.",
                0, 1, 0, 2,
                Resources.dontTouchMe, Resources.dontTouchMe_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 1);
        }
    }
}