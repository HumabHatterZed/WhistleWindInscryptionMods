using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                "A clock to rewind your wasted time. A blatant cheat.",
                0, 1, 0, 4,
                Artwork.backwardClock, Artwork.backwardClock_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                onePerDeck: true, isDonator: true,
                choiceType: CardHelper.ChoiceType.Rare, riskLevel: CardHelper.RiskLevel.Waw,
                terrainType: CardHelper.TerrainType.TerrainRare, metaType: CardHelper.MetaType.OutOfJail);
        }
    }
}