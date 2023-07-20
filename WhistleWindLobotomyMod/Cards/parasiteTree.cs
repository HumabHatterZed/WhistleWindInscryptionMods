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
        private void Card_ParasiteTree_D04108()
        {
            const string parasiteTree = "parasiteTree";

            NewCard(parasiteTree, "Parasite Tree", "A beautiful tree. It wants only to help you and your beasts.",
                attack: 0, health: 3, blood: 1)
                .SetPortraits(parasiteTree)
                .AddAbilities(Gardener.ability)
                .AddTribes(TribeBotanic)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw, ModCardType.Donator);
        }
    }
}