using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string mirrorOfAdjustment = "wstl_mirrorOfAdjustment";
        private static void MirrorOfAdjustment_O0981() {
            string textureName = "mirrorOfAdjustment";
            CardManager.New(LobotomyPlugin.pluginPrefix, mirrorOfAdjustment, "The Mirror of Adjustment",
                attack: 0, health: 1, "A mirror that reflects nothing on its surface.")
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Woodcutter.ID)
                .SetStatIcon(SpecialStatIcon.Mirror)
                .SetTerrain(false)
                .SetDefaultEvolutionName("The Grand Mirror of Adjustment")
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}