using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string drownedSisters = "wstl_drownedSisters";
        private static void DrownedSisters() {
            return;
            string textureName = "drownedSisters";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, drownedSisters, "The Drowned Sisters",
                attack: 0, health: 2)
                .SetBonesCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.DebuffEnemy)
                .AddTribes(AbnormalPlugin.TribeAnthropoid)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}