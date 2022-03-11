﻿using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void WorldPortrait_O0991()
        {
            List<Ability> abilities = new()
            {
               Reflector.ability
            };

            WstlUtils.Add(
                "wstl_worldPortrait", "Portrait of Another World",
                "The portrait captures a moment, one we're destined to lose.",
                0, 4, 1, 0,
                Resources.worldPortrait,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.worldPortrait_emission);
        }
    }
}