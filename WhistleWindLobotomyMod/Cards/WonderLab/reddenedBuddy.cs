using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string reddenedBuddy = "wstl_reddenedBuddy";
        private static void ReddenedBuddy() {
            return;
            string textureName = "reddenedBuddy";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, reddenedBuddy, "Reddened Buddy",
                attack: 0, health: 0)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities()
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}