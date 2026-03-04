using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 2/3 Flying Scorching
        /// </summary>
        public const string theFirebird = "wstl_theFirebird";
        private static void TheFirebird_O02101() {
            string textureName = "theFirebird";
            CardManager.New(LobotomyPlugin.pluginPrefix, theFirebird, "The Firebird",
                attack: 2, health: 3, "A bird that longs for the thrill of being hunted.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Scorching.ID, Ability.Flying)
                .AddTribes(Tribe.Bird)
                .SetDefaultEvolutionName("The Grand Firebird")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}