using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string queenBee = "wstl_queenBee";
        private static void QueenBee_T0450() {
            string textureName = "queenBee";
            CardManager.New(LobotomyPlugin.pluginPrefix, queenBee, "Queen Bee",
                attack: 0, health: 4, "A monstrous amalgam of a hive and a bee.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(QueenNest.ability)
                .AddTribes(Tribe.Insect)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}