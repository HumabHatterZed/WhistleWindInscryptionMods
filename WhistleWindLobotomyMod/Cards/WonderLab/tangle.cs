using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string tangle = "wstl_tangle";
        private static void Tangle() {
            string textureName = "tangle";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, tangle, "Tangle",
                attack: 2, health: 3)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(BindingStrike.ability, Driver.ability)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}