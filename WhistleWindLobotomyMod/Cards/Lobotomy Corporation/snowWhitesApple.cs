using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 1/1 Roots
        /// </summary>
        public const string snowWhitesApple = "wstl_snowWhitesApple";
        private static void SnowWhitesApple_F0442() {
            string name = "Snow White's Apple";
            string name2 = "Snow White's Rotted Apple";
            string desc = "A poisoned apple brought to life, on a fruitless search for its own happily ever after.";
            string textureName = "snowWhitesApple";
            CardManager.New(LobotomyPlugin.pluginPrefix, snowWhitesApple, name,
                attack: 1, health: 1, desc)
                .SetBonesCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Roots.ability)
                .AddTribes(TribeBotanic)
                .AddTraits(Trait.KillsSurvivors)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 1, health: 1, desc)
                .SetBonesCost(3)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Roots.ability)
                .AddTribes(TribeBotanic)
                .AddTraits(Trait.KillsSurvivors)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}