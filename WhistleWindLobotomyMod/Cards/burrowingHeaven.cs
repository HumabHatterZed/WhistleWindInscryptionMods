using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BurrowingHeaven_O0472()
        {
            const string burrowingHeaven = "burrowingHeaven";

            NewCard(burrowingHeaven, "The Burrowing Heaven", "Don't look away. Contain it in your sight.",
                attack: 0, health: 1, blood: 1)
                .SetPortraits(burrowingHeaven)
                .AddAbilities(Ability.GuardDog, Ability.Sentry)
                .AddTribes(TribeDivine)
                .SetDefaultEvolutionName("The Elder Burrowing Heaven")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}