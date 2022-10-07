using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_NothingThere_O0620()
        {
            List<Ability> abilities = new()
            {
                Ability.Reach
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                Mimicry.specialAbility
            };
            List<Trait> traits = new()
            {
                Trait.DeathcardCreationNonOption
            };

            CardHelper.CreateCard(
                "wstl_nothingThere", "Yumi",
                "I don't remember this challenger...",
                1, 1, 2, 0,
                Artwork.nothingThere, Artwork.nothingThere_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                onePerDeck: true,
                choiceType: CardHelper.ChoiceType.Rare, riskLevel: CardHelper.RiskLevel.Aleph);
        }
    }
}