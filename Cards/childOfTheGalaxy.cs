﻿using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void ChildOfTheGalaxy_O0155()
        {
            List<Ability> abilities = new()
            {
                FlagBearer.ability,
                Ability.BoneDigger
            };

            CardHelper.CreateCard(
                "wstl_childOfTheGalaxy", "Child of the Galaxy",
                "A small child longing for a friend. Will you be his?",
                1, 4, 2, 0,
                Resources.childOfTheGalaxy, Resources.childOfTheGalaxy_emission, gbcTexture: Resources.childOfTheGalaxy_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 3);
        }
    }
}