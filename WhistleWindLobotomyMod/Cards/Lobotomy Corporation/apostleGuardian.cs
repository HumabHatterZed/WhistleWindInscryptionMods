using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string apostleGuardian = "wstl_apostleGuardian";
        public const string apostleGuardianDown = "wstl_apostleGuardianDown";
        private static void ApostleGuardian_T0346() {
            string guardianName = "Guardian Apostle";
            string textureName = "apostleGuardian";
            string textureName2 = "apostleGuardianDown";
            Ability[] abilities = new[] { ApostleSigil.ability };
            Tribe[] tribes = new[] { TribeDivine };
            Trait[] traits = new[] { Apostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            CardManager.New(LobotomyPlugin.pluginPrefix, apostleGuardian, guardianName,
                attack: 4, health: 6)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(abilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(false)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, apostleGuardianDown, guardianName,
                attack: 0, health: 1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(abilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(false)
                .Build();
        }
    }
}