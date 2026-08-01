using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string silentOrchestra = "wstl_silentOrchestra";
        public const string silentEnsemble = "wstl_silentEnsemble";
        private static void SilentOrchestra_T0131() {
            string textureName = "silentEnsemble";
            string textureName2 = "silentOrchestra";
            CardManager.New(LobotomyPlugin.pluginPrefix, silentEnsemble, "Chairs",
                attack: 1, health: 2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.BuffNeighbours)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Orchestral)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, silentOrchestra, "The Silent Orchestra",
                attack: 0, health: 4, "Soon, the song none can hear but all can listen to will begin.")
                .SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Conductor.ID)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Orchestral)
                .SetDefaultEvolutionName("The Grand Silent Orchestra")
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph, true);
        }
    }
}