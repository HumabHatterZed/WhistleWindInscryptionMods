using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_Ozma_F04116()
        {
            List<Ability> abilities = new()
            {

            };

            CardHelper.CreateCard(
                "wstl_ozma", "Ozma",
                "The former ruler of a far away land, now reduced to this.",
                2, 2, 2, 0,
                Artwork.ozma, Artwork.ozma_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}