using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_YinYangBody_O07103()
        {
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };
            CardHelper.CreateCard(
                "wstl_yinYangBody", "",
                "Now you become [c:bR]the sky[c:], and I the land.",
                0, 101, 0, 0,
                Resources.yinYangBody, Resources.yinYangBody_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, hideStats: true);
        }
    }
}