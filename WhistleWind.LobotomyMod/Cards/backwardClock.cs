using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BackwardClock_D09104()
        {
            List<Ability> abilities = new()
            {
                TimeMachine.ability
            };
            List<Trait> traits = new()
            {
                Trait.DeathcardCreationNonOption
            };
            LobotomyCardHelper.CreateCard(
                "wstl_backwardClock", "Backward Clock",
                "A clock to rewind your wasted time. Will you pay the toll?",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 2,
                Artwork.backwardClock, Artwork.backwardClock_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: LobotomyCardHelper.RiskLevel.Waw,
                modTypes: LobotomyCardHelper.ModCardType.Donator | LobotomyCardHelper.ModCardType.Restricted);
        }
    }
}