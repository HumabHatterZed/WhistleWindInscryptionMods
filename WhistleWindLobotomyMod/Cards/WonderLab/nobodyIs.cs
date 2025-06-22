using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string nobodyIs = "wstl_nobodyIs";
        private static void Nobodyis() {
            return;
            string textureName = "nobodyIs";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, nobodyIs, "Nobody Is",
                attack: 0, health: 0)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities()
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}