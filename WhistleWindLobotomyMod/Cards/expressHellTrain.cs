using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
            CardHelper.CreateCard(
                "wstl_expressHellTrain", "Express Train to Hell",
                "When the time comes, the train will sound its mighty horn.",
                atk: 0, hp: 4,
                blood: 0, bones: 4, energy: 0,
                Artwork.expressHellTrain, Artwork.expressHellTrain_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                onePerDeck: true,
                cardType: CardHelper.CardType.Rare, riskLevel: CardHelper.RiskLevel.Waw,
                 metaTypes: CardHelper.MetaType.Restricted);
        }
    }
}