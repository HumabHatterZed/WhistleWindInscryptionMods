using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SnowWhitesApple_F0442()
        {
            List<Ability> abilities = new()
            {
                Roots.ability
            };

            WstlUtils.Add(
                "wstl_snowWhitesApple", "Snow White's Apple",
                "A poisoned apple brought to life, on a fruitless search for its own happily ever after.",
                1, 3, 0, 3,
                Resources.snowWhitesApple, Resources.snowWhitesApple_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}