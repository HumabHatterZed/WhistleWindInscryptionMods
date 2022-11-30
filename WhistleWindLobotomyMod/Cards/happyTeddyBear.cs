﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_HappyTeddyBear_T0406()
        {
            List<Ability> abilities = new()
            {
                Ability.DebuffEnemy
            };
            LobotomyCardHelper.CreateCard(
                "wstl_happyTeddyBear", "Happy Teddy Bear",
                "Its memories began with a warm hug.",
                atk: 2, hp: 3,
                blood: 0, bones: 8, energy: 0,
                Artwork.happyTeddyBear, Artwork.happyTeddyBear_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.He);
        }
    }
}