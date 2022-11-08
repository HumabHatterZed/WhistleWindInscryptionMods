using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_HappyTeddyBear_T0406()
        {
            List<Ability> abilities = new()
            {
                Ability.DebuffEnemy
            };
            CardHelper.CreateCard(
                "wstl_happyTeddyBear", "Happy Teddy Bear",
                "Its memories began with a warm hug.",
                atk: 2, hp: 3,
                blood: 0, bones: 8, energy: 0,
                Artwork.happyTeddyBear, Artwork.happyTeddyBear_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}