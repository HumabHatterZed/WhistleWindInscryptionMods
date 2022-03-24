using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void CENSOREDMinion_O0389()
        {
            WstlUtils.Add(
                "wstl_censoredMinion", "CENSORED",
                "I think it's best you don't know what it looks like.",
                1, 1, 0, 0,
                Resources.censoredMinion, Resources.censoredMinion_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}