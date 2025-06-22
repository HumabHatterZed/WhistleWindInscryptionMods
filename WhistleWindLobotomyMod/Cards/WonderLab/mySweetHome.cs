using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string mySweetHome = "wstl_mySweetHome";
        private static void MySweetHome() {
            return;
            string textureName = "mySweetHome";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHome, "My Sweet Home",
                attack: 0, health: 2)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.MadeOfStone, Ability.Reach)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}