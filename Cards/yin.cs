﻿using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Yin_O05102()
        {
            List<Ability> abilities = new()
            {
                Ability.Submerge,
                Ability.Strafe
            };
            CardHelper.CreateCard(
                "wstl_yin", "Yin",
                "Now you become [c:bR]the sky[c:], and I the land.",
                2, 3, 2, 0,
                Resources.yin, Resources.yin_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                onePerDeck: true, riskLevel: 4);
        }
    }
}