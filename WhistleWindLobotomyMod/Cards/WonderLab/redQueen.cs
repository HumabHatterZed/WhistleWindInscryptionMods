using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string redQueen = "wstl_redQueen";
        private static void RedQueen()
        {
            return;
            string textureName = "redQueen";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, redQueen, "Red Queen",
                attack: 2, health: 2)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.BuffNeighbours)
                .AddTribes(AbnormalPlugin.TribeFae)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}