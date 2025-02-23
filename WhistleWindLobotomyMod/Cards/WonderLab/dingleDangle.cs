using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string dingleDangle = "wstl_dingleDangle";
        private static void DingleDangle()
        {
            return;
            string textureName = "dingleDangle";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, dingleDangle, "Dingle Dangle",
                attack: 0, health: 0)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities()
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}