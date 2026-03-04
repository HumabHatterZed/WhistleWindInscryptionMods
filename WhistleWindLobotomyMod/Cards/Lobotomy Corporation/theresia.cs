using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 0/2 Healer
        /// </summary>
        public const string theresia = "wstl_theresia";
        private static void Theresia_T0909() {
            string name = "Theresia";
            string desc = "An old music box. It plays a familiar melody.";
            string textureName = "theresia";
            CardManager.New(LobotomyPlugin.pluginPrefix, theresia, name,
                attack: 0, health: 2, desc)
                .SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Healer.ID)
                .AddTribes(TribeMechanical)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 1, desc)
                .SetEnergyCost(2)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Healer.ID)
                .AddTribes(TribeMechanical)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}