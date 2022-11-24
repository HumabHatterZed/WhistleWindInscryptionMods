﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SnowWhitesApple_F0442()
        {
            List<Ability> abilities = new()
            {
                Roots.ability
            };
            List<Trait> traits = new()
            {
                Trait.KillsSurvivors
            };
            CardHelper.CreateCard(
                "wstl_snowWhitesApple", "Snow White's Apple",
                "A poisoned apple brought to life, on a fruitless search for its own happily ever after.",
                atk: 1, hp: 3,
                blood: 0, bones: 3, energy: 0,
                Artwork.snowWhitesApple, Artwork.snowWhitesApple_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}