﻿using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_Spiderling_O0243()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedRed.appearance
            };

            CardHelper.CreateCard(
                "wstl_spiderling", "Spiderling",
                "Small and defenceless.",
                0, 1, 0, 0,
                Artwork.spiderling, Artwork.spiderling_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances, evolveName: "wstl_spiderBrood");
        }
    }
}