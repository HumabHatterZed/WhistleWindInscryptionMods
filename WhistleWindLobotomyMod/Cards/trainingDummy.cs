using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TrainingDummy_00000()
        {
            List<Tribe> tribes = new() { TribeMechanical };

            CreateCard(
                "wstl_trainingDummy", "Standard Training-Dummy Rabbit",
                "A beast in the shape of a training dummy. The safest of these abnormal creatures.",
                atk: 0, hp: 2,
                blood: 0, bones: 0, energy: 1,
                Artwork.trainingDummy, Artwork.trainingDummy_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Zayin);
        }
    }
}