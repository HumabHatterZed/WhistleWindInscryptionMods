using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_BlueStar_O0393()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };

            CardHelper.CreateCard(
                "wstl_blueStar", "Blue Star",
                "When this is over, let's meet again as stars.",
                0, 2, 4, 0,
                Artwork.blueStar, Artwork.blueStar_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Rare, riskLevel: CardHelper.RiskLevel.Aleph,
                evolveName: "wstl_blueStar2");
        }
    }
}