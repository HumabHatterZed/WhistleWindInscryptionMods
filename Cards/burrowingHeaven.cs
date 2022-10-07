using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_BurrowingHeaven_O0472()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp,
                Ability.WhackAMole
            };

            CardHelper.CreateCard(
                "wstl_burrowingHeaven", "The Burrowing Heaven",
                "Don't look away. Contain it in your sight.",
                0, 2, 0, 3,
                Artwork.burrowingHeaven, Artwork.burrowingHeaven_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}