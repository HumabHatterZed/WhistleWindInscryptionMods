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

            NewCard(heartOfAspiration, "The Heart of Aspiration", "A heart without an owner. It emboldens those nearby.",
                attack: 1, health: 2, blood: 1)
                .SetPortraits(heartOfAspiration)
                .AddAbilities(Ability.BuffNeighbours)
                .SetDefaultEvolutionName("The Elder Heart of Aspiration")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}