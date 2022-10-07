using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_BloodBath_T0551()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                WristCutter.specialAbility,
            };
            List<Trait> traits = new()
            {
                Trait.Goat
            };

            CardHelper.CreateCard(
                "wstl_bloodBath", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                0, 1, 1, 0,
                Artwork.bloodBath, Artwork.bloodBath_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}