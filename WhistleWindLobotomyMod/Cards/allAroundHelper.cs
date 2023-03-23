using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

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
            CreateCard(
                "wstl_allAroundHelper", "All-Around Helper",
                "A murderous machine originally built to do chores. It reminds me of someone I know.",
                atk: 1, hp: 3,
                blood: 0, bones: 0, energy: 4,
                Artwork.allAroundHelper, Artwork.allAroundHelper_emission, pixelTexture: Artwork.allAroundHelper_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new() { TribeMechanical }, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He,
                evolveName: "{0} 2.0");
        }
    }
}