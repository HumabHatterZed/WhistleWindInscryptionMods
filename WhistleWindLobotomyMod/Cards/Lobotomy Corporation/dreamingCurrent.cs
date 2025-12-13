using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string dreamingCurrent = "wstl_dreamingCurrent";
        private static void DreamingCurrent_T0271() {
            string textureName = "dreamingCurrent";
            CardManager.New(LobotomyPlugin.pluginPrefix, dreamingCurrent, "The Dreaming Current",
                attack: 4, health: 3, "A sickly child fed candy that let it see the ocean.")
                .SetBloodCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Barreler.ability, NimbleFoot.ability)
                .SetDefaultEvolutionName("The Elder Dreaming Current")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}