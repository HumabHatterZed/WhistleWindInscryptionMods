using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string weCanChangeAnything = "wstl_weCanChangeAnything";
        private static void WeCanChangeAnything_T0985()
        {
            string name = "We Can Change Anything";
            string name2 = "We Will Change Everything";
            string desc = "Whatever you're dissatisfied with, this machine will fix it. You just have to step inside.";
            string textureName = "weCanChangeAnything";
            CardManager.New(LobotomyPlugin.pluginPrefix, weCanChangeAnything, name,
                attack: 1, health: 1, desc)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Grinder.ability)
                .AddTribes(TribeMechanical)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 1, health: 1, desc)
                .SetBloodCost(1)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Grinder.ability)
                .AddTribes(TribeMechanical)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}