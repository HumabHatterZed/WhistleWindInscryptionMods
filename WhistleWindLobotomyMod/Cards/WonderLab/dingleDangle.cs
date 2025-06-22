using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string dingleDangle = "wstlWonder_dingleDangle";
        private static void DingleDangle() {
            string textureName = "dingleDangle";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, dingleDangle, "Dingle Dangle",
                attack: 1, health: 2, "A beautiful ribbon that binds despairing hearts, so they may all become hanging, dangling fruits.")
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(BindingStrike.ability)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}