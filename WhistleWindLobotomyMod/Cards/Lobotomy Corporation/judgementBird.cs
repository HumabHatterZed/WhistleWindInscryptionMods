using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 1/1 Sniper, Executioner
        /// </summary>
        public const string judgementBird = "wstl_judgementBird";
        private static void JudgementBird_O0262() {
            string textureName = "judgementBird";
            CardManager.New(LobotomyPlugin.pluginPrefix, judgementBird, "Judgement Bird",
                attack: 1, health: 1, "A long-necked bird that swiftly judges sinners, guilty or no.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Sniper)
                .AddTribes(Tribe.Bird)
                .AddTraits(BlackForest, Executioner)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Rare, RiskLevel.Waw, true);
        }
    }
}