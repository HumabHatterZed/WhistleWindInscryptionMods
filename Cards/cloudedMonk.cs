using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void CloudedMonk_D01110()
        {
            CardHelper.CreateCard(
                "wstl_cloudedMonk", "Clouded Monk",
                "A monk no more.",
                4, 2, 3, 0,
                Resources.cloudedMonk, Resources.cloudedMonk_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}