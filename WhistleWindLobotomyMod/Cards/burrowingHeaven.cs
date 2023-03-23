using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BurrowingHeaven_O0472()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp,
                Ability.WhackAMole
            };
            List<Tribe> tribes = new() { TribeDivine };

            CreateCard(
                "wstl_burrowingHeaven", "The Burrowing Heaven",
                "Don't look away. Contain it in your sight.",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.burrowingHeaven, Artwork.burrowingHeaven_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw,
                evolveName: "The Elder Burrowing Heaven");
        }
    }
}