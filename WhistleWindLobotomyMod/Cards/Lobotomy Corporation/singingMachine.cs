using InscryptionAPI.Card;
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

            CardManager.New(pluginPrefix, singingMachine, "Singing Machine",
                attack: 0, health: 4, "A wind-up music machine. The song it plays is to die for.")
                .SetBloodCost(1)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(ModAssembly, singingMachine)
                .AddAbilities(TeamLeader.ability, Aggravating.ability)
                .AddTribes(TribeMechanical)
                .AddTraits(Orchestral)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}