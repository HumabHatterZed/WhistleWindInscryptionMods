using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string trainingDummy = "wstl_trainingDummy";
        private static void TrainingDummy_00000()
        {
            string name = "Standard Training-Dummy Rabbit";
            string desc = "A beast in the shape of a training dummy. The safest of these abnormal creatures.";
            string textureName = "trainingDummy";
            CardManager.New(LobotomyPlugin.pluginPrefix, trainingDummy, name,
                attack: 0, health: 2, desc)
                .SetEnergyCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddTribes(TribeMechanical)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 2, desc)
                .SetEnergyCost(1)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddTribes(TribeMechanical)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}