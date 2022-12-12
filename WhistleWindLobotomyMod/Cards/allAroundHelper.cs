using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_AllAroundHelper_T0541()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe,
                Ability.SplitStrike
            };
            LobotomyCardHelper.CreateCard(
                "wstl_allAroundHelper", "All-Around Helper",
                "A murderous machine originally built to do chores. It reminds me of someone I know.",
                atk: 1, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.allAroundHelper, Artwork.allAroundHelper_emission, pixelTexture: Artwork.allAroundHelper_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.He,
                customTribe: TribeMachine);
        }
    }
}