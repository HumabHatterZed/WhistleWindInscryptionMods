using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                3, 2, 0, 8,
                Artwork.happyTeddyBear, Artwork.happyTeddyBear_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}