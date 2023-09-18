using InscryptionAPI.Card;
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

            NewCard(expressHellTrain, "Express Train to Hell", "When the time comes, the train will sound its mighty horn.",
                attack: 0, health: 4, bones: 4)
                .SetPortraits(expressHellTrain)
                .AddAbilities(TheTrain.ability)
                .AddTribes(TribeMechanical)
                .SetTerrain()
                .SetDefaultEvolutionName("Express Train to Turbo Hell")
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.Waw);
        }
    }
}