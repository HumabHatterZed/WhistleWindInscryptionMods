using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_YinYangHead_O07103()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                CustomEvolveHelper.specialAbility
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };
            LobotomyCardHelper.CreateCard(
                "wstl_yinYangHead", "",
                "Now you become [c:bR]the sky[c:], and I the land.",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 0,
                Artwork.yinYangHead, Artwork.yinYangHead_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, hideStats: true);
        }
    }
}