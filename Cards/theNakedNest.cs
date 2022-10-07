using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_TheNakedNest_O0274()
        {
            List<Ability> abilities = new()
            {
                SerpentsNest.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            List<Trait> traits = new()
            {
                Trait.KillsSurvivors
            };

            CardHelper.CreateCard(
                "wstl_theNakedNest", "The Naked Nest",
                "They can enter your body through any aperture.",
                0, 2, 0, 4,
                Artwork.theNakedNest, Artwork.theNakedNest_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}