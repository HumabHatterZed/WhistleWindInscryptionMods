using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 1/5 Guardian
        /// </summary>
        public const string happyTeddyBear = "wstl_happyTeddyBear";
        private static void HappyTeddyBear_T0406() {
            string name = "Happy Teddy Bear";
            string name2 = "Big Happy Teddy Bear";
            string desc = "A discarded stuffed bear. Its memories began with a warm hug.";
            string textureName = "happyTeddyBear";
            CardManager.New(LobotomyPlugin.pluginPrefix, happyTeddyBear, name,
                attack: 1, health: 5, desc)
                .SetBonesCost(6)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.GuardDog)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.He);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 1, health: 4, desc)
                .SetBonesCost(6)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.GuardDog)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}