using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void CENSOREDMinion_O0389()
        {
            WstlUtils.Add(
                "wstl_censoredMinion", "CENSORED",
                "I think it's best you don't know what it looks like.",
                1, 1, 0, 0,
                Resources.censoredMinion,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(),
                emissionTexture: Resources.censoredMinion_emission,
                titleTexture: Resources.censored_title);
        }
    }
}