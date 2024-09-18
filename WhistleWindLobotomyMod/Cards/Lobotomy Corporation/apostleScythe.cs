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
        private void Card_ApostleScythe_T0346()
        {
            const string scytheName = "Scythe Apostle";
            const string apostleScythe = "apostleScythe";
            const string apostleScytheDown = "apostleScytheDown";
            Tribe[] tribes = new[] { TribeDivine };
            Trait[] traits = new[] { Apostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            CardManager.New(pluginPrefix, apostleScythe, scytheName,
                attack: 2, health: 6)
                .SetPortraits(ModAssembly, apostleScythe)
                .AddAbilities(Ability.DoubleStrike, ApostleSigil.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(false)
                .Build();

            CardManager.New(pluginPrefix, apostleScytheDown, scytheName,
                attack: 0, health: 1)
                .SetPortraits(ModAssembly, apostleScytheDown)
                .AddAbilities(ApostleSigil.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(false)
                .Build();
        }
    }
}