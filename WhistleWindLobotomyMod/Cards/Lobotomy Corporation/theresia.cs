using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Theresia_T0909()
        {
            const string theresia = "theresia";

            CardManager.New(pluginPrefix, theresia, "Theresia",
                attack: 0, health: 2, "An old music box. It plays a familiar melody.")
                .SetEnergyCost(2)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(ModAssembly, theresia)
                .AddAbilities(Healer.ability)
                .AddTribes(TribeMechanical)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}