using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void BlueStar2_O0393()
        {
            List<Ability> abilities = new()
            {
                Assimilator.ability,
                Ability.AllStrike,
                Idol.ability,
            };

            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.RareCardBackground
            };

            CardHelper.CreateCard(
                "wstl_blueStar2", "Blue Star",
                "When this is over, let's meet again as stars.",
                1, 4, 4, 0,
                Resources.blueStar, Resources.blueStar_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, onePerDeck: true, riskLevel: 5);
        }
    }
}