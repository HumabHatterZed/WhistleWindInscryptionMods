using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
            LobotomyCardHelper.CreateCard(
                "wstl_nothingThere", "Yumi",
                "I don't remember this challenger...",
                atk: 1, hp: 1,
                blood: 2, bones: 0, energy: 0,
                Artwork.nothingThere, Artwork.nothingThere_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: LobotomyCardHelper.RiskLevel.Aleph);
        }
    }
}