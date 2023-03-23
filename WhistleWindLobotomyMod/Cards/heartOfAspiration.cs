using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_HeartOfAspiration_O0977()
        {
            List<Ability> abilities = new() { Ability.BuffNeighbours };

            CreateCard(
                "wstl_heartOfAspiration", "The Heart of Aspiration",
                "A heart without an owner. It emboldens those nearby.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.heartOfAspiration, Artwork.heartOfAspiration_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Teth,
                evolveName: "[name]The Elder Heart of Aspiration");
        }
    }
}