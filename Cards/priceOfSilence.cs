using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_PriceOfSilence_O0565()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                Time.specialAbility
            };

            CardHelper.CreateCard(
                "wstl_priceOfSilence", "Price of Silence",
                "The unflinching hand of time cuts down man and beast alike.",
                0, 1, 1, 0,
                Artwork.priceOfSilence, Artwork.priceOfSilence_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(), appearances: new(),
                statIcon: Time.icon, choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.He,
                terrainType: CardHelper.TerrainType.TerrainAttack);
        }
    }
}