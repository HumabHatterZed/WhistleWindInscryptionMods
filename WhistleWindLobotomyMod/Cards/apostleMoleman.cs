﻿using DiskCardGame;
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
        private void Card_ApostleMoleman_T0346()
        {
            const string molemanName = "Moleman Apostle";
            const string apostleMoleman = "apostleMoleman";
            const string apostleMolemanDown = "apostleMolemanDown";
            Tribe[] tribes = new[] { TribeDivine };
            Trait[] traits = new[] { TraitApostle };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { ForcedWhiteEmission.appearance };

            NewCard(apostleMoleman, molemanName,
                attack: 1, health: 8)
                .SetPortraits(apostleMoleman)
                .AddAbilities(Ability.Reach, Ability.WhackAMole, Apostle.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .Build(CardHelper.ChoiceType.Rare, cardType: ModCardType.EventCard);

            NewCard(apostleMolemanDown, molemanName,
                attack: 0, health: 1)
                .SetPortraits(apostleMolemanDown)
                .AddAbilities(Apostle.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances)
                .Build(CardHelper.ChoiceType.Rare, cardType: ModCardType.EventCard);
        }
    }
}