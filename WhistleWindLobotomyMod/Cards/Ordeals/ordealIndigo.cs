using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string sweeper = "wstl_sweeper";
        private static void Cards_IndigoOrdeal() {
            string textureName = "sweeper";
            CardInfo sweeperCard = CardManager.New(LobotomyPlugin.pluginPrefix, sweeper, "Sweeper",
                attack: 2, health: 3)
                .SetBloodCost(2).SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Persistent.ability, Bloodfiend.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission, SweeperAppearance.appearance)
                .AddTribes(TribeFae)
                .AddTraits(Ordeal)
                .Build();
        }
    }
}