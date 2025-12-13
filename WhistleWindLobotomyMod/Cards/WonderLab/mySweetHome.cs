using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string mySweetHome = "wstlWonder_mySweetHome";
        private static void MySweetHome() {
            string textureName = "mySweetHome";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHome, "My Sweet Home",
                attack: 0, health: 0)
                .SetBonesCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Alluring.ability, Ability.SteelTrap)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}