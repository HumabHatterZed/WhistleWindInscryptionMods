using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void KingOfGreed_O0164()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.StrafePush
            };

            WstlUtils.Add(
                "wstl_kingOfGreed", "The King of Greed",
                "An aged storyteller. She can tell you any tale, even those that can't exist.",
                5, 4, 0, 0,
                Resources.kingOfGreed,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>());
        }
    }
}