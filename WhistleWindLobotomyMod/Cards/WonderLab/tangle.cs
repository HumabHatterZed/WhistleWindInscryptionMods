using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void XCard_Tangle()
        {
            const string tangle = "tangle";

            CardManager.New(wonderlabPrefix, tangle, "Tangle",
                attack: 0, health: 0)
                .SetPortraits(ModAssembly, tangle)
                .AddAbilities()
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}