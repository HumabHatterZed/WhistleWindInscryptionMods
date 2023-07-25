using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleStaff_T0346()
        {
            const string staffName = "Staff Apostle";
            const string apostleStaff = "apostleStaff";
            const string apostleStaffDown = "apostleStaffDown";
            Tribe[] tribes = new[] { TribeDivine };
            Trait[] traits = new[] { TraitApostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            NewCard(apostleStaff, staffName,
                attack: 3, health: 6)
                .SetPortraits(apostleStaff)
                .AddAbilities(Ability.Sniper, Apostle.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .Build(cardType: ModCardType.EventCard);

            NewCard(apostleStaffDown, staffName,
                attack: 0, health: 1)
                .SetPortraits(apostleStaffDown)
                .AddAbilities(Apostle.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .Build(cardType: ModCardType.EventCard);
        }
    }
}