using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WeCanChangeAnything_T0985()
        {
            const string weCanChangeAnything = "weCanChangeAnything";

            CardManager.New(pluginPrefix, weCanChangeAnything, "We Can Change Anything",
                attack: 1, health: 1, "Whatever you're dissatisfied with, this machine will fix it. You just have to step inside.")
                .SetBloodCost(1)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(ModAssembly, weCanChangeAnything)
                .AddAbilities(Grinder.ability)
                .AddTribes(TribeMechanical)
                .SetDefaultEvolutionName("We Will Change Everything")
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}