using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WarmHeartedWoodsman_F0532()
        {
            const string warmHeartedWoodsman = "warmHeartedWoodsman";

            CardManager.New(pluginPrefix, warmHeartedWoodsman, "Warm-Hearted Woodsman",
                attack: 2, health: 3, "A tin woodsman in search of a heart. Perhaps you can give him yours.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, warmHeartedWoodsman)
                .AddAbilities(Woodcutter.ability)
                .AddTribes(TribeMechanical)
                .AddTraits(EmeraldCity)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}