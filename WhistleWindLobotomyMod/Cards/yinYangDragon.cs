using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Helpers.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_YinYangDragon_O07103()
        {
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };

            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                CustomEvolveHelper.specialAbility
            };
            CreateCard(
                "wstl_yinYangHead", "",
                "Now you become [c:bR]the sky[c:], and I the land.",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 0,
                Artwork.yinYangHead, Artwork.yinYangHead_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, hideStats: true);

            CreateCard(
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