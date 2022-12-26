using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SnowWhitesVine_F0442()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp
            };
            CardHelper.CreateCard(
                "wstl_snowWhitesVine", "Thorny Vines",
                "A vine.",
                0, 1, 0, 0,
                Resources.snowWhitesVine, Resources.snowWhitesVine_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}