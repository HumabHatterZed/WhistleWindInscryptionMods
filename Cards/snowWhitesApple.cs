using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SnowWhitesApple_F0442()
        {
            List<Ability> abilities = new List<Ability>
            {
                Roots.ability
            };

            WstlUtils.Add(
                "wstl_snowWhitesApple", "Snow White's Apple",
                "A poisoned apple brought to life, on a fruitless search for its own happily ever after.",
                3, 1, 0, 3,
                Resources.snowWhitesApple,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.snowWhitesApple_emission);
        }
    }
}