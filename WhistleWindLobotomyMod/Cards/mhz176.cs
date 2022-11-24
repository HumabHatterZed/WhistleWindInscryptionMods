﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MHz176_T0727()
        {
            List<Ability> abilities = new()
            {
                Ability.BuffNeighbours,
                Ability.BuffEnemy
            };

            CardHelper.CreateCard(
                "wstl_mhz176", "1.76 MHz",
                "This is a record, a record of a day we must never forget.",
                atk: 0, hp: 3,
                blood: 0, bones: 0, energy: 3,
                Artwork.mhz176, Artwork.mhz176_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}