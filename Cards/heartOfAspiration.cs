﻿using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void HeartOfAspiration_O0977()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.BuffNeighbours
            };

            WstlUtils.Add(
                "wstl_heartOfAspiration", "The Heart of Aspiration",
                "A heart without an owner.",
                2, 1, 2, 0,
                Resources.heartOfAspiration,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}