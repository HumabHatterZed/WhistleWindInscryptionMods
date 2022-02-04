﻿using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ApostleSpear_T0346()
        {
            List<Ability> abilities = new List<Ability>
            {
                Piercing.ability,
                Apostle.ability
            };

            List<Trait> traits = new()
            {
                Trait.Uncuttable,
                Trait.Pelt
            };

            WstlUtils.Add(
                "wstl_apostleSpear", "Apostle",
                "The time has come.",
                6, 3, 0, 0,
                Resources.apostleSpear,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits);
        }
    }
}