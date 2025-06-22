using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string yin = "wstl_yin";
        private static void Yin_O05102() {
            string textureName = "yin";
            CardManager.New(LobotomyPlugin.pluginPrefix, yin, "Yin",
                attack: 2, health: 3, "A black pendant in search of its missing half.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetAltPortraits(LobotomyPlugin.ModAssembly, "yinAlt")
                .AddAbilities(Ability.Strafe, Ability.Submerge)
                .AddAppearances(AlternateBattlePortrait.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}