﻿using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ShelterFrom27March_T0982()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.PreventAttack,
                Aggravating.ability
            };

            WstlUtils.Add(
                "wstl_shelterFrom27March", "Shelter From the 27th of March",
                "It makes itself the safest place in the world by altering the reality around it.",
                1, 0, 0, 3,
                Resources.shelterFrom27March,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.shelterFrom27March_emission);
        }
    }
}