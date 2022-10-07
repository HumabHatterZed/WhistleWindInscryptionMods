using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_TheFirebird_O02101()
        {
            List<Ability> abilities = new()
            {
                Burning.ability,
                Ability.Flying
            };
            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };

            CardHelper.CreateCard(
                "wstl_theFirebird", "The Firebird",
                "A bird that longs for the thrill of being hunted.",
                2, 3, 2, 0,
                Artwork.theFirebird, Artwork.theFirebird_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}