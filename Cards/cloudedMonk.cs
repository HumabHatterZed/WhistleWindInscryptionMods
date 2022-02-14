using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void CloudedMonk_D01110()
        {
            WstlUtils.Add(
                "wstl_cloudedMonk", "Clouded Monk",
                "A monk no more.",
                4, 2, 3, 0,
                Resources.cloudedMonk,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                new List<Tribe>());
        }
    }
}