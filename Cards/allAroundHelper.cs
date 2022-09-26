using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void AllAroundHelper_T0541()
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
                Resources.allAroundHelper, Resources.allAroundHelper_emission, gbcTexture: Resources.allAroundHelper_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 3);
        }
    }
}