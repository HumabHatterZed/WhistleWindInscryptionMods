using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void XCard_ReddenedBuddy()
        {
            const string reddenedBuddy = "reddenedBuddy";

            CardManager.New(wonderlabPrefix, reddenedBuddy, "Reddened Buddy",
                attack: 0, health: 0)
                .SetPortraits(ModAssembly, reddenedBuddy)
                .AddAbilities()
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}