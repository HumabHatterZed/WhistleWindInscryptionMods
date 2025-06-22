using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string penitentGirl = "wstl_penitentGirl";
        private static void PenitentGirl() {
            return;
            string textureName = "penitentGirl";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, penitentGirl, "The Penitent Girl",
                attack: 0, health: 1)
                .SetBonesCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Sharp, Ability.Sharp)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}