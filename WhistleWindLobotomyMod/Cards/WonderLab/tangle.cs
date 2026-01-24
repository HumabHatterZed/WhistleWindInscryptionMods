using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string tangle = "wstlWonder_tangle";
        private static void Tangle() {
            string textureName = "tangle";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, tangle, "Tangle",
                attack: 2, health: 3, "A head from which sprouts endless waves of golden hair.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(BindingStrike.ability, Driver.ability)
                .AddTribes(AbnormalPlugin.TribeAnthropoid)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}