﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BloodBath_T0551()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                WristCutter.specialAbility,
            };
            List<Trait> traits = new()
            {
                Trait.Goat
            };
            LobotomyCardHelper.CreateCard(
                "wstl_bloodBath", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.bloodBath, Artwork.bloodBath_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Teth);
        }
    }
}