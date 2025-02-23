using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string whiteLake = "wstl_whiteLake";
        private static void XWhiteLake()
        {
            return;
            string textureName = "whiteLake";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, whiteLake, "White Lake",
                attack: 1, health: 3)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Damsel.ability)
                .AddTribes(AbnormalPlugin.TribeFae)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}