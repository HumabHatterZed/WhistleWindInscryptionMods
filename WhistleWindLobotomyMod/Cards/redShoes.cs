using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_RedShoes_O0408()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp,
                Ability.GuardDog
            };
            CardHelper.CreateCard(
                "wstl_redShoes", "Red Shoes",
                "How pretty. Maybe they'll fit.",
                atk: 0, hp: 3,
                blood: 1, bones: 0, energy: 0,
                Artwork.redShoes, Artwork.redShoes_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}