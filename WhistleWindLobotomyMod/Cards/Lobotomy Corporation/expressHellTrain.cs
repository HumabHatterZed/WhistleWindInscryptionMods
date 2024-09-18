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

            CardManager.New(pluginPrefix, expressHellTrain, "Express Train to Hell",
                attack: 0, health: 1, "When the time comes, the train will sound its mighty horn.")
                .SetEnergyCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, expressHellTrain)
                .AddAbilities(TheTrain.ability)
                .AddTribes(TribeMechanical)
                .SetTerrain()
                .SetDefaultEvolutionName("Express Train to Turbo Hell")
                .Build(CardHelper.CardType.Rare, RiskLevel.Waw, true);
        }
    }
}