using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void TheNakedWorm_O0274()
        {
            WstlUtils.Add(
                "wstl_theNakedWorm", "Naked Worm",
                "It can enter your body through any aperture.",
                1, 1, 0, 0,
                Resources.theNakedWorm,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                new List<Tribe>());
        }
    }
}