using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Helpers.CardHelper;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BlueStar2_O0393()
        {
            List<Ability> abilities = new()
            {
                Assimilator.ability,
                Ability.AllStrike
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                Forced.appearance
            };
            CardHelper.CreateCard(
                "wstl_blueStar2", "Blue Star",
                "When this is over, let's meet again as stars.",
                atk: 3, hp: 6,
                blood: 4, bones: 0, energy: 0,
                Artwork.blueStar, Artwork.blueStar_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances,
                cardType: CardHelper.CardType.Rare, metaTypes: MetaType.NonChoice);
        }
    }
}