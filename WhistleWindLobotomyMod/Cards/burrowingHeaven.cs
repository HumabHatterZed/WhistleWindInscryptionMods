using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
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

            CardInfo heaven = NewCard(
                burrowingHeaven,
                "The Burrowing Heaven",
                "Don't look away. Contain it in your sight.",
                attack: 0, health: 1, blood: 1)
                .SetPortraits(burrowingHeaven)
                .AddAbilities(Ability.GuardDog, Ability.Sentry)
                .AddTribes(TribeDivine)
                .SetEvolveInfo("[name]The Elder Burrowing Heaven");

            CreateCard(heaven, CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}