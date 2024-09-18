using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DontTouchMe_O0547()
        {
            const string dontTouchMe = "dontTouchMe";

            CardManager.New(pluginPrefix, dontTouchMe, "Don't Touch Me",
                attack: 0, health: 1, "Don't touch it.")
                .SetEnergyCost(2)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(ModAssembly, dontTouchMe)
                .AddAbilities(Punisher.ability)
                .AddTribes(TribeMechanical)
                .SetTerrain()
                .SetDefaultEvolutionName("Please Don't Touch Me")
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}