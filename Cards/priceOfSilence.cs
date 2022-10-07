using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

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
            List<Trait> traits = new()
            {
                Trait.Terrain
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.TerrainBackground
            };
            CardHelper.CreateCard(
                "wstl_priceOfSilence", "Price of Silence",
                "The unflinching hand of time cuts down man and beast alike.",
                0, 1, 1, 0,
                Resources.priceOfSilence, Resources.priceOfSilence_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits, appearances: appearances,
                statIcon: Time.icon, isChoice: true, riskLevel: 3);
        }
    }
}