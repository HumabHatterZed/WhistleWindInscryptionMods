using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                0, 3, 1, 0,
                Artwork.redShoes, Artwork.redShoes_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}