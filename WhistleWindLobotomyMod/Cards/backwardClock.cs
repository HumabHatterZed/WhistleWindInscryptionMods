using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
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
            CardHelper.CreateCard(
                "wstl_backwardClock", "Backward Clock",
                "A clock to rewind your wasted time. Will you pay the toll?",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 6,
                Artwork.backwardClock, Artwork.backwardClock_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                onePerDeck: true,
                cardType: CardHelper.CardType.Rare, riskLevel: CardHelper.RiskLevel.Waw,
                 metaTypes: CardHelper.MetaType.Donator | CardHelper.MetaType.Restricted);
        }
    }
}