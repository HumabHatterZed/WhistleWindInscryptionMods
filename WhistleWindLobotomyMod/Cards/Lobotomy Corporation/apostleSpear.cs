using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string apostleSpear = "wstl_apostleSpear";
        public const string apostleSpearDown = "wstl_apostleSpearDown";
        private static void ApostleSpear_T0346()
        {
            string spearName = "Spear Apostle";
            string textureName = "apostleSpear";
            string textureName2 = "apostleSpearDown";
            Tribe[] tribes = new[] { TribeDivine };
            Trait[] traits = new[] { Apostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            CardManager.New(LobotomyPlugin.pluginPrefix, apostleSpear, spearName,
                attack: 4, health: 6)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Piercing.ability, ApostleSigil.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(false)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, apostleSpearDown, spearName,
                attack: 0, health: 1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(ApostleSigil.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(false)
                .Build();
        }
    }
}