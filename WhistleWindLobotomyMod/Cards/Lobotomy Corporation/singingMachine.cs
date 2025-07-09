using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 0/4 Team leader, Aggravating
        /// </summary>
        public const string singingMachine = "wstl_singingMachine";
        private static void SingingMachine_O0530() {
            string name = "Singing Machine";
            string desc = "A wind-up music machine. The song it plays is to die for.";
            string textureName = "singingMachine";
            CardManager.New(LobotomyPlugin.pluginPrefix, singingMachine, name,
                attack: 0, health: 4, desc)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(TeamLeader.ability, Aggravating.ability)
                .AddTribes(TribeMechanical)
                .AddTraits(Orchestral)
                .Build(CardHelper.CardType.Common, RiskLevel.He);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 4, desc)
                .SetBloodCost(1)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(TeamLeader.ability, Aggravating.ability)
                .AddTribes(TribeMechanical)
                .AddTraits(Orchestral)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}