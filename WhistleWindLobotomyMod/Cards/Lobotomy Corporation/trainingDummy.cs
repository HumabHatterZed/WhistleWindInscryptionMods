using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TrainingDummy_00000()
        {
            const string trainingDummy = "trainingDummy";

            CardManager.New(pluginPrefix, trainingDummy, "Standard Training-Dummy Rabbit",
                attack: 0, health: 2, "A beast in the shape of a training dummy. The safest of these abnormal creatures.")
                .SetEnergyCost(1)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(ModAssembly, trainingDummy)
                .AddTribes(TribeMechanical)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}