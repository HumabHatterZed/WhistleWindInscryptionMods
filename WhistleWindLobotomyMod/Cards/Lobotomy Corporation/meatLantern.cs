using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 1/3 Reflector, Reach
        /// </summary>
        public const string meatLantern = "wstl_meatLantern";
        private static void MeatLantern_O0484() {
            string textureName = "meatLantern";
            CardManager.New(LobotomyPlugin.pluginPrefix, meatLantern, "Meat Lantern",
                attack: 1, health: 3, "A beautiful flower attached to a mysterious creature.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Alluring.ability, Ability.Sentry)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}