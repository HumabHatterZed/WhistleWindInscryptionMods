using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_Yin_O05102()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe,
                Ability.Submerge
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                BlackFish.appearance
            };

            CardHelper.CreateCard(
                "wstl_yin", "Yin",
                "A black pendant in search of its missing half.",
                2, 3, 2, 0,
                Artwork.yin, Artwork.yin_emission,
                altTexture: Artwork.yinAlt, emissionAltTexture: Artwork.yinAlt_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, onePerDeck: true,
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}