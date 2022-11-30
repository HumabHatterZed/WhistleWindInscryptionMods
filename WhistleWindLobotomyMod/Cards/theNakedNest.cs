﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheNakedNest_O0274()
        {
            List<Ability> abilities = new()
            {
                SerpentsNest.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            List<Trait> traits = new()
            {
                Trait.KillsSurvivors
            };
            LobotomyCardHelper.CreateCard(
                "wstl_theNakedNest", "The Naked Nest",
                "They can enter your body through any aperture.",
                atk: 0, hp: 2,
                blood: 0, bones: 4, energy: 0,
                Artwork.theNakedNest, Artwork.theNakedNest_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Waw);
        }
    }
}