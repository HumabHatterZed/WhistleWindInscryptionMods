using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string pygmalion = "wstlWonder_pygmalion";
        private static void Pygmalion() {
            string textureName = "pygmalion";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, pygmalion, "Pygmalion",
                attack: 3, health: 3)
                .SetBloodCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(FingerTapping.ability)
                .AddTribes(AbnormalPlugin.TribeBotanic)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}