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
        private void Card_SingingMachine_O0530()
        {
            const string singingMachine = "singingMachine";

            NewCard(singingMachine, "Singing Machine", "A wind-up music machine. The song it plays is to die for.",
                attack: 0, health: 4, blood: 1)
                .SetPortraits(singingMachine)
                .AddAbilities(TeamLeader.ability, Aggravating.ability)
                .AddTribes(TribeMechanical)
                .AddTraits(Orchestral)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}