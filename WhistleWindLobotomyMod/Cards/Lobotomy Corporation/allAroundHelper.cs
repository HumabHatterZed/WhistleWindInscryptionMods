using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string allAroundHelper = "wstl_allAroundHelper";
        private static void AllAroundHelper_T0541()
        {
            string name = "All-Around Helper";
            string name2 = "All-Around Helper 2.0";
            string desc = "A murderous cleaning machine. Far nicer than a certain other...well, nevermind.";
            string textureName = "allAroundHelper";

            CardManager.New(LobotomyPlugin.pluginPrefix, allAroundHelper, name,
                attack: 1, health: 3, desc)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetEnergyCost(4)
                .AddAbilities(Ability.Strafe, Ability.SplitStrike)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.He);

            // GBC
            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 1, health: 2, desc)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetEnergyCost(4)
                .SetCardTemple(CardTemple.Tech)
                .AddAbilities(Ability.Strafe, Ability.SplitStrike)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}