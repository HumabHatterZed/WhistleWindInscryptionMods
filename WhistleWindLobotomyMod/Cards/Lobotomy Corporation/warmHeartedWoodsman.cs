using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// <c>(2/3) (<see cref="Woodcutter"/>) (2 Blood)</c>
        /// </summary>
        public const string warmHeartedWoodsman = "wstl_warmHeartedWoodsman";
        private static void WarmHeartedWoodsman_F0532() {
            string textureName = "warmHeartedWoodsman";
            CardManager.New(LobotomyPlugin.pluginPrefix, warmHeartedWoodsman, "Warm-Hearted Woodsman",
                attack: 2, health: 3, "A tin woodsman in search of a heart. Perhaps you can give him yours.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Woodcutter.ID)
                .AddTribes(TribeMechanical)
                .AddTraits(EmeraldCity)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}