﻿using DiskCardGame;
using WhistleWind.Core.Helpers;
using ModDebuggingMod.Properties;
using System.Collections.Generic;
using InscryptionAPI.Card;
using InscryptionAPI.Pelts;
using WhistleWindLobotomyMod;
using InscryptionAPI.Pelts.Extensions;
using InscryptionAPI.Nodes;
using System;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void CARD_DEBUG()
        {
            List<Ability> abilities = new()
            {
                Ability.GainBattery,
                Ability.DrawCopy
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {

            };
            List<Tribe> tribes = new()
            {
                LobotomyPlugin.TribeDivine
            };
            List<Trait> traits = new()
            {

            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {

            };
            CardHelper.CreateCard(
                "wstl", "wstlcard", "DEBUG CARD",
                "You shouldn't see this.",
                0, 10, 1, 0, 0,
                Resources.fairyFestival, Resources.fairyFestival_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: traits);
        }
    }
}
