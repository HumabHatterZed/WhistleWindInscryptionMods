using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MagicalGirlDiamond_O0164()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                CustomEvolveHelper.specialAbility
            };
            CardHelper.CreateCard(
                "wstl_magicalGirlDiamond", "Magical Girl",
                "A girl encased in hardened amber. Happiness trapped by greed.",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.magicalGirlDiamond, Artwork.magicalGirlDiamond_emission, pixelTexture: Artwork.magicalGirlDiamond_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                onePerDeck: true,
                cardType: CardHelper.CardType.Basic, evolveName: "wstl_kingOfGreed", riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}