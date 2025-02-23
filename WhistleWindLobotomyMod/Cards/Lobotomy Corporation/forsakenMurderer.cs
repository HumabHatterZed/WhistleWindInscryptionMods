using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string forsakenMurderer = "wstl_forsakenMurderer";
        private static void ForsakenMurderer_T0154()
        {
            string textureName = "forsakenMurderer";
            CardManager.New(LobotomyPlugin.pluginPrefix, forsakenMurderer, "Forsaken Murderer",
                attack: 4, health: 1, "Experimented on then forgotten. What was anger has become abhorrence.")
                .SetBonesCost(8)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddTribes(TribeAnthropoid)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}