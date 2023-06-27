using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ExpressHellTrain_T0986()
        {
            const string expressHellTrain = "expressHellTrain";

            CardInfo expressHellTrainCard = NewCard(
                expressHellTrain,
                "Express Train to Hell",
                "When the time comes, the train will sound its mighty horn.",
                attack: 0, health: 4, bones: 4)
                .SetPortraits(expressHellTrain)
                .AddAbilities(TheTrain.ability)
                .AddTribes(TribeMechanical)
                .SetTerrain()
                .SetEvolveInfo("[name]Express Train to Turbo Hell");

            CreateCard(expressHellTrainCard, CardHelper.ChoiceType.Rare, RiskLevel.Waw);
        }
    }
}