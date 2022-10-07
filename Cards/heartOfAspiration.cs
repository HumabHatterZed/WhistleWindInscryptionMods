using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_HeartOfAspiration_O0977()
        {
            List<Ability> abilities = new()
            {
                Ability.BuffNeighbours
            };

            CardHelper.CreateCard(
                "wstl_heartOfAspiration", "The Heart of Aspiration",
                "A heart without an owner. It emboldens those nearby.",
                1, 2, 1, 0,
                Artwork.heartOfAspiration, Artwork.heartOfAspiration_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}