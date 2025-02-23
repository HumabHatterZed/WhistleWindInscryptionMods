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
        public const string apostleStaff = "wstl_apostleStaff";
        public const string apostleStaffDown = "wstl_apostleStaffDown";
        private static void ApostleStaff_T0346()
        {
            string staffName = "Staff Apostle";
            string textureName = "apostleStaff";
            string textureName2 = "apostleStaffDown";
            Tribe[] tribes = new[] { TribeDivine };
            Trait[] traits = new[] { Apostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            CardManager.New(LobotomyPlugin.pluginPrefix, apostleStaff, staffName,
                attack: 3, health: 6)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Sniper, ApostleSigil.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .SetEventCard(false)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, apostleStaffDown, staffName,
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