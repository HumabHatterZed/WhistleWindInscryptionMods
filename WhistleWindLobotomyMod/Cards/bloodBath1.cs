﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Bloodbath1_T0551()
        {
            List<Ability> abilities = new()
            {
                Ability.TripleBlood
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                WristCutter.specialAbility
            };
            CreateCard(
                "wstl_bloodBath1", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.bloodBath1, Artwork.bloodBath1_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                statIcon: SpecialStatIcon.SacrificesThisTurn);
        }
    }
}