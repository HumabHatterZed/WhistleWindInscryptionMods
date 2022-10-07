using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_Yang_O07103()
        {
            List<Ability> abilities = new()
            {
                Regenerator.ability
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                BlackFish.appearance
            };

            CardHelper.CreateCard(
                "wstl_yang", "Yang",
                "A white pendant that heals those nearby.",
                0, 3, 1, 0,
                Artwork.yang, Artwork.yang_emission,
                altTexture: Artwork.yangAlt, emissionAltTexture: Artwork.yangAlt_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, onePerDeck: true,
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}