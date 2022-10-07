using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Yang_O07103()
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
                Resources.yang, Resources.yang_emission,
                altTexture: Resources.yangAlt, emissionAltTexture: Resources.yangAlt_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances,
                onePerDeck: true, isChoice: true, riskLevel: 4);
        }
    }
}