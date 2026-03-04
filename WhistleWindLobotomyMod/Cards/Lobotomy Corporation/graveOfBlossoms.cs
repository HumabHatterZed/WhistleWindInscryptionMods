using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string graveOfBlossoms = "wstl_graveOfBlossoms";
        private static void GraveOfBlossoms_O04100() {
            string textureName = "graveOfBlossoms";
            CardManager.New(LobotomyPlugin.pluginPrefix, graveOfBlossoms, "Grave of Cherry Blossoms",
                attack: 0, health: 3, "A blooming cherry tree. The more blood it has, the more beautiful it becomes.")
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Bloodletter.ID)
                .AddTribes(TribeBotanic)
                .SetDefaultEvolutionName("Mass Grave of Cherry Blossoms")
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}