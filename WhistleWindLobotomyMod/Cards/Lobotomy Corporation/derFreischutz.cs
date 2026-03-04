using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 2/2 Sniper, Persistent
        /// </summary>
        public const string derFreischutz = "wstl_derFreischutz";
        private static void DerFreischutz_F0169() {
            string name = "Der Freischütz";
            string desc = "A friendly hunter to some, a cruel gunsman to others. His magic bullets always hit their mark.";
            string textureName = "derFreischutz";
            CardManager.New(LobotomyPlugin.pluginPrefix, derFreischutz, name,
                attack: 2, health: 2, desc)
                .SetBonesCost(7)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Persistent.ID, ActivatedSniper.ID)
                .AddTribes(TribeFae)
                .SetDefaultEvolutionName("Der Ältere Freischütz")
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Rare, RiskLevel.He);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 1, health: 2, desc)
                .SetGemsCost(GemType.Orange, GemType.Orange)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(ActivatedSniper.ID)
                .AddTribes(TribeFae)
                .SetDefaultEvolutionName("Der Ältere Freischütz")
                .Build(CardHelper.CardType.Rare, RiskLevel.He, true);
        }
    }
}