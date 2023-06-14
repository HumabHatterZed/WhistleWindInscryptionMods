﻿using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Bloodbath_T0551()
        {
            const string bathName = "Bloodbath";
            const string bloodBath = "bloodBath";
            const string bloodBath1 = "bloodBath1";
            const string bloodBath2 = "bloodBath2";
            const string bloodBath3 = "bloodBath3";
            Ability[] abilities = new[] { Ability.TripleBlood };
            SpecialTriggeredAbility[] specialAbilities = new[] { WristCutter.specialAbility };

            CardInfo bath = NewCard(
                bloodBath,
                bathName,
                "A tub of blood. The hands of people you once loved wait inside.",
                attack: 0, health: 1, blood: 1)
                .SetPortraits(bloodBath)
                .AddSpecialAbilities(specialAbilities)
                .AddTraits(Trait.Goat);

            CardInfo bath1 = NewCard(
                bloodBath1,
                bathName,
                attack: 0, health: 1, blood: 1)
                .SetPortraits(bloodBath1)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities);

            CardInfo bath2 = NewCard(
                bloodBath2,
                bathName,
                attack: 0, health: 2, blood: 1)
                .SetPortraits(bloodBath2)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetStatIcon(SpecialStatIcon.SacrificesThisTurn);

            CardInfo bath3 = NewCard(
                bloodBath3,
                bathName,
                attack: 1, health: 2, blood: 2)
                .SetPortraits(bloodBath3)
                .AddAbilities(Ability.TripleBlood, Ability.QuadrupleBones)
                .SetStatIcon(SpecialStatIcon.SacrificesThisTurn);

            CreateCard(bath, CardHelper.ChoiceType.Common, RiskLevel.Teth);
            CreateCards(bath1, bath2, bath3);
        }
    }
}