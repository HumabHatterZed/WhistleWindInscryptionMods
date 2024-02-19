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

            NewCard(trainingDummy, "Standard Training-Dummy Rabbit", "A beast in the shape of a training dummy. The safest of these abnormal creatures.",
                attack: 0, health: 2, energy: 1, temple: CardTemple.Tech)
                .SetPortraits(trainingDummy)
                .AddTribes(TribeMechanical)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}