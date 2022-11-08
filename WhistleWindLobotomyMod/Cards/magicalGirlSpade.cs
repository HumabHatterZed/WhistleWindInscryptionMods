using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MagicalGirlSpade_O0173()
        {
            List<Ability> abilities = new()
            {
                Protector.ability
            };
            List<SpecialTriggeredAbility> specialAbilties = new()
            {
                PinkTears.specialAbility
            };
            CardHelper.CreateCard(
                "wstl_magicalGirlSpade", "Magical Girl",
                "A loyal knight fighting to protect those close to her.",
                atk: 1, hp: 4,
                blood: 1, bones: 0, energy: 0,
                Artwork.magicalGirlSpade, Artwork.magicalGirlSpade_emission, pixelTexture: Artwork.magicalGirlSpade_pixel,
                abilities: abilities, specialAbilities: specialAbilties,
                metaCategories: new(), tribes: new(), traits: new(),
                onePerDeck: true,
                cardType: CardHelper.CardType.Rare, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}