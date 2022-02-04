using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
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
                2, 4, 0, 0,
                Resources.cloudedMonk,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                new List<Tribe>());
        }
    }
}