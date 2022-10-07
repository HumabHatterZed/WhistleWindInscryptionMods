using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_YinYangHead_O07103()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                CustomFledgling.specialAbility
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };
            CardHelper.CreateCard(
                "wstl_yinYangHead", "",
                "Now you become [c:bR]the sky[c:], and I the land.",
                0, 0, 0, 0,
                Artwork.yinYangHead, Artwork.yinYangHead_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, hideStats: true);
        }
    }
}