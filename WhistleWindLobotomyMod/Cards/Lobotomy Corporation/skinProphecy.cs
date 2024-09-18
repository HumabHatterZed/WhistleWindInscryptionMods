using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SkinProphecy_T0990()
        {
            const string skinProphecy = "skinProphecy";

            CardManager.New(pluginPrefix, skinProphecy, "Skin Prophecy",
                attack: 0, health: 3, "A holy book. Its believers wrapped it in skin to preserve its sanctity.")
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, skinProphecy)
                .AddAbilities(Witness.ability)
                .AddTribes(TribeDivine)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}