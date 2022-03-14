using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void KingOfGreed_O0164()
        {
            List<Ability> abilities = new()
            {
                Ability.StrafePush
            };

            WstlUtils.Add(
                "wstl_kingOfGreed", "The King of Greed",
                "An aged storyteller. She can tell you any tale, even those that can't exist.",
                4, 5, 2, 0,
                Resources.kingOfGreed,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), emissionTexture: Resources.kingOfGreed_emission,
                onePerDeck: true);
        }
    }
}