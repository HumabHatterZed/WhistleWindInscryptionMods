using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string reddenedBuddy = "wstlWonder_reddenedBuddy";
        private static void ReddenedBuddy() {
            string textureName = "redBuddy";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, reddenedBuddy, "Reddened Buddy",
                attack: 1, health: 6, "A pitiable pet, ever loyal to its cruel master.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetAltPortraits(LobotomyPlugin.ModAssembly, "redBuddy_alt")
                .AddAbilities(StressResponse.ID)
                .AddTribes(DiskCardGame.Tribe.Canine)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}