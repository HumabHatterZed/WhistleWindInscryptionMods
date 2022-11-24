﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SnowQueen_F0137()
        {
            List<Ability> abilities = new()
            {
                FrostRuler.ability
            };
            CardHelper.CreateCard(
                "wstl_snowQueen", "The Snow Queen",
                "A queen from far away. Those who enter her palace never leave.",
                atk: 2, hp: 1,
                blood: 0, bones: 8, energy: 0,
                Artwork.snowQueen, Artwork.snowQueen_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}