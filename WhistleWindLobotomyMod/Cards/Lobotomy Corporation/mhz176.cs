using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 2/1 Annoying
        /// </summary>
        public const string mhz176 = "wstl_mhz176";
        private static void MHz176_T0727() {
            string name = "1.76 MHz";
            string desc = "This is a record. A record of a day we must never forget.";
            string textureName = "mhz176";
            CardManager.New(LobotomyPlugin.pluginPrefix, mhz176, name,
                attack: 2, health: 1, desc)
                .SetEnergyCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.BuffEnemy)
                .SetTerrain(false)
                .AddTribes(AbnormalPlugin.TribeMechanical)
                .SetDefaultEvolutionName("1.76 GHz")
                .Build(CardHelper.CardType.Common, RiskLevel.Teth);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 2, health: 1, desc)
                .SetEnergyCost(3)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.BuffEnemy)
                .SetTerrain(false)
                .AddTribes(AbnormalPlugin.TribeMechanical)
                .SetDefaultEvolutionName("1.76 GHz")
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}