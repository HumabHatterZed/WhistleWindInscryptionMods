using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string whiteLake = "wstlWonder_whiteLake";
        private static void WhiteLake() {
            string textureName = "whiteLake";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, whiteLake, "White Lake",
                attack: 0, health: 2, "A beautiful avian with a distate for those with great fortitude.")
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Damsel.ability)
                .AddTribes(AbnormalPlugin.TribeFae)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}