using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void YinYangHead_O07103()
        {
            WstlUtils.Add(
                "wstl_yinYangHead", "",
                "Now you become [c:bR]the sky[c:], and I the land.",
                0, 1, 0, 0,
                Resources.yinYangHead, Resources.yinYangHead_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                hideStats: true);
        }
    }
}