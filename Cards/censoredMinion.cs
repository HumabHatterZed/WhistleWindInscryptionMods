using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_CENSOREDMinion_O0389()
        {
            CardHelper.CreateCard(
                "wstl_censoredMinion", "CENSORED",
                "I think it's best you don't know what it looks like.",
                0, 0, 0, 0,
                Resources.censoredMinion, Resources.censoredMinion_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}
