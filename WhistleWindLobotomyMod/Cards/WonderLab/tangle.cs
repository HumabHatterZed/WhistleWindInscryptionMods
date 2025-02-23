using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string tangle = "wstl_tangle";
        private static void Tangle()
        {
            return;
            string textureName = "tangle";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, tangle, "Tangle",
                attack: 0, health: 0)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities()
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}