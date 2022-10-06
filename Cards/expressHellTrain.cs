using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

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
            List<CardMetaCategory> metaCategories = new()
            {
                CardHelper.CANNOT_GIVE_SIGILS,
                CardHelper.CANNOT_GAIN_SIGILS,
                CardHelper.CANNOT_BUFF_STATS,
                CardHelper.CANNOT_COPY_CARD
            };
            List<Trait> traits = new()
            {
                Trait.DeathcardCreationNonOption
            };

            CardHelper.CreateCard(
                "wstl_expressHellTrain", "Express Train to Hell",
                "When the time comes, the train will sound its mighty horn.",
                0, 1, 0, 2,
                Resources.expressHellTrain, Resources.expressHellTrain_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: metaCategories, tribes: new(), traits: traits,
                isTerrain: true, isRare: true, onePerDeck: true, riskLevel: 4);
        }
    }
}