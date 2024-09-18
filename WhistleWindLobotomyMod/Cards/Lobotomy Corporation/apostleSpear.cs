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
        private void Card_ApostleSpear_T0346()
        {
            const string spearName = "Spear Apostle";
            const string apostleSpear = "apostleSpear";
            const string apostleSpearDown = "apostleSpearDown";
            Tribe[] tribes = new[] { TribeDivine };
            Trait[] traits = new[] { Apostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            CardManager.New(pluginPrefix, apostleSpear, spearName,
                attack: 4, health: 6)
                .SetPortraits(ModAssembly, apostleSpear)
                .AddAbilities(Piercing.ability, ApostleSigil.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(false)
                .Build();

            CardManager.New(pluginPrefix, apostleSpearDown, spearName,
                attack: 0, health: 1)
                .SetPortraits(ModAssembly, apostleSpearDown)
                .AddAbilities(ApostleSigil.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(false)
                .Build();
        }
    }
}