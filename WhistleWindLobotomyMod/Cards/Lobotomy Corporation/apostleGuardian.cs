using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleGuardian_T0346()
        {
            const string guardianName = "Guardian Apostle";
            const string apostleGuardian = "apostleGuardian";
            const string apostleGuardianDown = "apostleGuardianDown";
            Ability[] abilities = new[] { ApostleSigil.ability };
            Tribe[] tribes = new[] { TribeDivine };
            Trait[] traits = new[] { Apostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            CardManager.New(pluginPrefix, apostleGuardian, guardianName,
                attack: 4, health: 6)
                .SetPortraits(ModAssembly, apostleGuardian)
                .AddAbilities(abilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(false)
                .Build();

            CardManager.New(pluginPrefix, apostleGuardianDown, guardianName,
                attack: 0, health: 1)
                .SetPortraits(ModAssembly, apostleGuardianDown)
                .AddAbilities(abilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(false)
                .Build();
        }
    }
}