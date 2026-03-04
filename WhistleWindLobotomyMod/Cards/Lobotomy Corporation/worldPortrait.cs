using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string worldPortrait = "wstl_worldPortrait";
        private static void WorldPortrait_O0991() {
            string textureName = "worldPortrait";
            CardManager.New(LobotomyPlugin.pluginPrefix, worldPortrait, "Portrait of Another World",
                attack: 0, health: 4, "This portrait captures a moment, one we're destined to lose.")
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Reflector.ID)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}