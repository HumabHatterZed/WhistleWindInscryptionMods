using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_HeartOfAspiration_O0977()
        {
            const string heartOfAspiration = "heartOfAspiration";

            CardManager.New(pluginPrefix, heartOfAspiration, "The Heart of Aspiration",
                attack: 1, health: 2, "A heart without an owner. It emboldens those nearby.")
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, heartOfAspiration)
                .AddAbilities(Ability.BuffNeighbours)
                .SetDefaultEvolutionName("The Elder Heart of Aspiration")
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}