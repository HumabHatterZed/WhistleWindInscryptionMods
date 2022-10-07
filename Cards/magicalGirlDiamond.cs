using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_MagicalGirlDiamond_O0164()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                CustomFledgling.specialAbility
            };

            CardHelper.CreateCard(
                "wstl_magicalGirlDiamond", "Magical Girl",
                "A girl encased in hardened amber. Happiness trapped by greed.",
                0, 2, 1, 0,
                Artwork.magicalGirlDiamond, Artwork.magicalGirlDiamond_emission, pixelTexture: Artwork.magicalGirlDiamond_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                onePerDeck: true,
                choiceType: CardHelper.ChoiceType.Common, evolveName: "wstl_kingOfGreed", riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}