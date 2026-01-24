using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 3/5 Nettle Clothes
        /// </summary>
        public const string dreamOfABlackSwan = "wstl_dreamOfABlackSwan";
        private static void DreamOfABlackSwan_F0270() {
            string textureName = "dreamOfABlackSwan";
            CardManager.New(LobotomyPlugin.pluginPrefix, dreamOfABlackSwan, "Dream of a Black Swan",
                attack: 4, health: 4, "Sister of six brothers. Tirelessly she worked to protect them, all for naught.")
                .SetBloodCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Nettles.ability)
                .AddTribes(Tribe.Bird)
                .SetDefaultEvolutionName("Dream of an Elder Swan")
                .Build(CardHelper.CardType.Rare, RiskLevel.Waw, true);
        }
    }
}