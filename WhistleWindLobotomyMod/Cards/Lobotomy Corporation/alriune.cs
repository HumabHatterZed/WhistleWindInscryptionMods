using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string alriune = "wstl_alriune";
        private static void Alriune_T0453() {
            string name = "Alriune";
            string textureName = "alriune";
            string description = "A doll yearning to be a human. A human yearning to be a doll.";
            CardManager.New(LobotomyPlugin.pluginPrefix, alriune, name,
                attack: 4, health: 5, description)
                .SetBloodCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Strafe)
                .AddTribes(TribeBotanic, Tribe.Hooved)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}