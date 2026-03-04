using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string apostleMoleman = "wstl_apostleMoleman";
        public const string apostleMolemanDown = "wstl_apostleMolemanDown";
        private static void ApostleMoleman_T0346() {
            string molemanName = "Moleman Apostle";
            string textureName = "apostleMoleman";
            string textureName2 = "apostleMolemanDown";
            Tribe[] tribes = new[] { TribeDivine };
            Trait[] traits = new[] { Apostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            CardManager.New(LobotomyPlugin.pluginPrefix, apostleMoleman, molemanName,
                attack: 1, health: 8)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Reach, Ability.WhackAMole, ApostleSigil.ID)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(true)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, apostleMolemanDown, molemanName,
                attack: 0, health: 1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(ApostleSigil.ID)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(true)
                .Build();
        }
    }
}