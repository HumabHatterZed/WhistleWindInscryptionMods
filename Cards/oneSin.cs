﻿using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void OneSin_O0303()
        {
            List<Ability> abilities = new()
            {
                Martyr.ability
            };

            WstlUtils.Add(
                "wstl_oneSin", "One Sin and Hundreds of Good Deeds",
                "Its hollow sockets see through you.",
                0, 1, 0, 4,
                Resources.oneSin, Resources.oneSin_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}