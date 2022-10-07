using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_DimensionalRefraction_O0388()
        {
            List<Ability> abilities = new()
            {
                Ability.RandomAbility
            };

            CardHelper.CreateCard(
                "wstl_dimensionalRefraction", "Dimensional Refraction Variant",
                "A strange phenomenon. Or rather, the creature is the phenomena in and of itself.",
                4, 4, 3, 0,
                Artwork.dimensionalRefraction, Artwork.dimensionalRefraction_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}