using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_AllAroundHelper_T0541()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe,
                Ability.SplitStrike
            };

            CardHelper.CreateCard(
                "wstl_allAroundHelper", "All-Around Helper",
                "A murderous machine originally built to do chores. It reminds me of someone I know.",
                1, 3, 2, 0,
                Artwork.allAroundHelper, Artwork.allAroundHelper_emission, pixelTexture: Artwork.allAroundHelper_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}