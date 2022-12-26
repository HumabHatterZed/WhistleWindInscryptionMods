using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void BackwardClock_D09104()
        {
            List<Ability> abilities = new()
            {
                TimeMachine.ability
            };
            List<Trait> traits = new()
            {
                Trait.DeathcardCreationNonOption
            };
            CardHelper.CreateCard(
                "wstl_backwardClock", "Backward Clock",
                "A clock to rewind your wasted time. A blatant cheat.",
                0, 1, 0, 4,
                Resources.backwardClock, Resources.backwardClock_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                isTerrain: true, isRare: true, onePerDeck: true, isDonator: true, riskLevel: 4);
        }
    }
}