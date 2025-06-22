using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string randomPlaceholder = "wstl_RANDOM_PLACEHOLDER";
        private static void RANDOM_PLACEHOLDER() {
            string textureName = "RANDOM_PLACEHOLDER";
            CardManager.New(LobotomyPlugin.pluginPrefix, randomPlaceholder, randomPlaceholder,
                attack: 0, health: 0)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName, emissionName: "")
                .AddAbilities(DiskCardGame.Ability.RandomAbility)
                .SetStatIcon(SigilPower.Icon)
                .Build();
        }
    }
}