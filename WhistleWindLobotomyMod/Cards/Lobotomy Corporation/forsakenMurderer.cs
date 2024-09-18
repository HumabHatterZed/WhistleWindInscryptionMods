using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ForsakenMurderer_T0154()
        {
            const string forsakenMurderer = "forsakenMurderer";

            CardManager.New(pluginPrefix, forsakenMurderer, "Forsaken Murderer",
                attack: 4, health: 1, "Experimented on then forgotten. What was anger has become abhorrence.")
                .SetBonesCost(8)
                .SetPortraits(ModAssembly, forsakenMurderer)
                .AddTribes(TribeAnthropoid)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}