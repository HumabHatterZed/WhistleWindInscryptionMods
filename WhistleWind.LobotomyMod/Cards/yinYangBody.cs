using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_YinYangBody_O07103()
        {
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };
            LobotomyCardHelper.CreateCard(
                "wstl_yinYangBody", "",
                "Now you become [c:bR]the sky[c:], and I the land.",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 0,
                Artwork.yinYangBody, Artwork.yinYangBody_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, hideStats: true);
        }
    }
}