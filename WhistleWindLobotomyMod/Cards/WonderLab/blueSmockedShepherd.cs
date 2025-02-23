using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string blueSmockedShepherd = "wstl_blueSmockedShepherd";
        private static void BlueSmockedShepherd()
        {
            return;
            string textureName = "blueSmockedShepherd";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, blueSmockedShepherd, "Blue-Smocked Shepherd",
                attack: 0, health: 0)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Abusive.ability)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}