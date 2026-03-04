using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 1/1 Airborne, Punisher
        /// </summary>
        public const string punishingBird = "wstl_punishingBird";
        private static void PunishingBird_O0256() {
            string textureName = "punishingBird";
            CardManager.New(LobotomyPlugin.pluginPrefix, punishingBird, "Punishing Bird",
                attack: 1, health: 1, "A small bird on a mission to punish evildoers.")
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Flying, Punisher.ID)
                .AddTribes(Tribe.Bird)
                .AddTraits(BlackForest)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}