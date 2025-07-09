using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 1/1 Double Strike
        /// </summary>
        public const string funeralOfButterflies = "wstl_funeralOfButterflies";
        private static void FuneralOfButterflies_T0168() {
            string name = "Funeral of the Dead Butterflies";
            string name2 = "2nd Funeral of the Dead Butterflies";
            string desc = "The coffin is a tribute to the fallen. A memorial to those who can't return home.";
            string textureName = "funeralOfButterflies";
            CardManager.New(LobotomyPlugin.pluginPrefix, funeralOfButterflies, name,
                attack: 1, health: 1, desc)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.DoubleStrike)
                .AddTribes(Tribe.Insect)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.He);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 1, health: 2, desc)
                .SetBonesCost(5)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.DoubleStrike)
                .AddTribes(Tribe.Insect)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}