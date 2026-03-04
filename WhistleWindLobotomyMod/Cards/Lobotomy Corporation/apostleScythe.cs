using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string apostleScythe = "wstl_apostleScythe";
        public const string apostleScytheDown = "wstl_apostleScytheDown";
        private static void ApostleScythe_T0346() {
            string scytheName = "Scythe Apostle";
            string textureName = "apostleScythe";
            string textureName2 = "apostleScytheDown";
            Tribe[] tribes = new[] { TribeDivine };
            Trait[] traits = new[] { Apostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            CardManager.New(LobotomyPlugin.pluginPrefix, apostleScythe, scytheName,
                attack: 2, health: 6)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.DoubleStrike, ApostleSigil.ID)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(false)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, apostleScytheDown, scytheName,
                attack: 0, health: 1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(ApostleSigil.ID)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(false)
                .Build();
        }
    }
}