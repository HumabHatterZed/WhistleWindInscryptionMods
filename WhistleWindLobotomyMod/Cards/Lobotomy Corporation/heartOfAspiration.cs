using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 1/2 Leader
        /// </summary>
        public const string heartOfAspiration = "wstl_heartOfAspiration";
        private static void HeartOfAspiration_O0977() {
            string textureName = "heartOfAspiration";
            CardManager.New(LobotomyPlugin.pluginPrefix, heartOfAspiration, "The Heart of Aspiration",
                attack: 1, health: 2, "A heart without an owner. It emboldens those nearby.")
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.BuffNeighbours)
                .SetDefaultEvolutionName("The Elder Heart of Aspiration")
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}