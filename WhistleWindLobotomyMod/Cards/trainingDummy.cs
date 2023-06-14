using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
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

            CardInfo trainingDummyCard = NewCard(
                trainingDummy,
                "Standard Training-Dummy Rabbit",
                "A beast in the shape of a training dummy. The safest of these abnormal creatures.",
                attack: 0, health: 2, energy: 1)
                .SetPortraits(trainingDummy)
                .AddTribes(TribeMechanical);

            CreateCard(trainingDummyCard, CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}