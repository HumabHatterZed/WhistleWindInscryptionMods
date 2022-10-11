using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ExpressHellTrain_T0986()
        {
            List<Ability> abilities = new()
            {
                TheTrain.ability
            };
            List<Trait> traits = new()
            {
                Trait.DeathcardCreationNonOption
            };
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.RestrictAll
            };

            CardHelper.CreateCard(
                "wstl_expressHellTrain", "Express Train to Hell",
                "When the time comes, the train will sound its mighty horn.",
                0, 1, 0, 2,
                Artwork.expressHellTrain, Artwork.expressHellTrain_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                onePerDeck: true,
                choiceType: CardHelper.ChoiceType.Rare, riskLevel: CardHelper.RiskLevel.Waw,
                terrainType: CardHelper.TerrainType.Terrain, metaTypes: metaTypes);
        }
    }
}