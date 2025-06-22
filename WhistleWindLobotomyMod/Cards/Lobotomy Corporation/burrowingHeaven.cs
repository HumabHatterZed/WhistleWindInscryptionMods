using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string burrowingHeaven = "wstl_burrowingHeaven";
        private static void BurrowingHeaven_O0472() {
            string textureName = "burrowingHeaven";
            CardManager.New(LobotomyPlugin.pluginPrefix, burrowingHeaven, "The Burrowing Heaven",
                attack: 0, health: 1, "Don't look away. Contain it in your sight.")
                .SetBonesCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.GuardDog, Ability.Sentry)
                .AddTribes(TribeDivine)
                .SetDefaultEvolutionName("The Elder Burrowing Heaven")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}