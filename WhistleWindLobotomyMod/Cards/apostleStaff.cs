﻿using DiskCardGame;
using InscryptionAPI.Card;
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
            Trait[] traits = new[] { Apostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            NewCard(apostleStaff, staffName,
                attack: 3, health: 6)
                .SetPortraits(apostleStaff)
                .AddAbilities(Ability.Sniper, ApostleSigil.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .Build(cardType: ModCardType.EventCard);

            NewCard(apostleStaffDown, staffName,
                attack: 0, health: 1)
                .SetPortraits(apostleStaffDown)
                .AddAbilities(ApostleSigil.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .Build(cardType: ModCardType.EventCard);
        }
    }
}