using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 0/1 Stinky, Waterborne
        /// </summary>
        public const string drownedSisters = "wstlWonder_drownedSisters";
        private static void DrownedSisters() {
            string textureName = "drownedSisters";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, drownedSisters, "The Drowned Sisters",
                attack: 0, health: 1, "A pair of sisters, condemned for sins they don't know they committed.")
                .SetBonesCost(2).SetEnergyCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Submerge, Ability.DebuffEnemy)
                .AddTribes(AbnormalPlugin.TribeAnthropoid)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth);

            CardManager.New(LobotomyPlugin.pixelPrefix, drownedSisters, "The Drowned Sisters",
                attack: 0, health: 1, "A pair of sisters, condemned for sins they don't know they committed.")
                .SetBonesCost(1).SetEnergyCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Submerge, Ability.DebuffEnemy)
                .AddTribes(AbnormalPlugin.TribeAnthropoid)
                .SetCardTemple(CardTemple.Undead)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}