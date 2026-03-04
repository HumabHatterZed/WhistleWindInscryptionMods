using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 0/1 Punisher
        /// </summary>
        public const string dontTouchMe = "wstl_dontTouchMe";
        private static void DontTouchMe_O0547() {
            string name = "Don't Touch Me";
            string name2 = "Please Don't Touch Me";
            string desc = "Don't touch it.";
            string textureName = "dontTouchMe";
            CardManager.New(LobotomyPlugin.pluginPrefix, dontTouchMe, name,
                attack: 0, health: 1, desc)
                .SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Punisher.ID)
                .AddTribes(TribeMechanical)
                .SetTerrain()
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 1, desc)
                .SetEnergyCost(2)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Punisher.ID)
                .AddTribes(TribeMechanical)
                .SetTerrain()
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}