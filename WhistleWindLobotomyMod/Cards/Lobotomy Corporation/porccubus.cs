using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string porccubus = "wstl_porccubus";
        private static void Porccubus_O0298() {
            string textureName = "porccubus";
            CardManager.New(LobotomyPlugin.pluginPrefix, porccubus, "Porccubus",
                attack: 1, health: 1, "A prick from one of its quills creates a deadly euphoria.")
                .SetBonesCost(5)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Deathtouch)
                .AddTribes(TribeBotanic)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}