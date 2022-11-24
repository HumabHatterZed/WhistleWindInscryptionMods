﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DimensionalRefraction_O0388()
        {
            List<Ability> abilities = new()
            {
                Ability.RandomAbility
            };
            CardHelper.CreateCard(
                "wstl_dimensionalRefraction", "Dimensional Refraction Variant",
                "A strange phenomenon. Or rather, the creature is the phenomena in and of itself.",
                atk: 4, hp: 4,
                blood: 3, bones: 0, energy: 0,
                Artwork.dimensionalRefraction, Artwork.dimensionalRefraction_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}