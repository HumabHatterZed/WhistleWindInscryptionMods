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
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                SpecialAbilityFledgling.specialAbility
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };
            CardHelper.CreateCard(
                "wstl_yinYangHead", "",
                "Now you become [c:bR]the sky[c:], and I the land.",
                0, 101, 0, 0,
                Resources.yinYangHead, Resources.yinYangHead_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, hideStats: true);
        }
    }
}