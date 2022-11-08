using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MagicalGirlClover_O01111()
        {
            List<Ability> abilities = new()
            {
                Burning.ability
            };
            CardHelper.CreateCard(
                "wstl_magicalGirlClover", "Magical Girl",
                "Blind protector of another world.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.magicalGirlClover, Artwork.magicalGirlClover_emission, pixelTexture: Artwork.magicalGirlClover_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                onePerDeck: true,
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Waw,
                metaTypes: CardHelper.MetaType.Ruina);
        }
    }
}