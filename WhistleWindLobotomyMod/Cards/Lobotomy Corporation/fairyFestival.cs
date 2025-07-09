using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 1/1 Bloodfiend
        /// </summary>
        public const string fairyFestival = "wstl_fairyFestival";
        private static void FairyFestival_F0483() {
            string textureName = "fairyFestival";
            CardManager.New(LobotomyPlugin.pluginPrefix, fairyFestival, "Fairy Festival",
                attack: 1, health: 1, "Everything will be peaceful while you're under the fairies' care.")
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Bloodfiend.ability)
                .AddTribes(TribeFae)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}