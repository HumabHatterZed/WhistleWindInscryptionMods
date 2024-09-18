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

            CardManager.New(pluginPrefix, burrowingHeaven, "The Burrowing Heaven",
                attack: 0, health: 1, "Don't look away. Contain it in your sight.")
                .SetBonesCost(2)
                .SetPortraits(ModAssembly, burrowingHeaven)
                .AddAbilities(Ability.GuardDog, Ability.Sentry)
                .AddTribes(TribeDivine)
                .SetDefaultEvolutionName("The Elder Burrowing Heaven")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}