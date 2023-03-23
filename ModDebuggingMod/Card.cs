using DiskCardGame;
using WhistleWind.Core.Helpers;
using ModDebuggingMod.Properties;
using System.Collections.Generic;
using InscryptionAPI.Card;
using InscryptionAPI.Pelts;
using WhistleWindLobotomyMod;
using InscryptionAPI.Pelts.Extensions;
using InscryptionAPI.Nodes;
using System;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void CARD_DEBUG()
        {
            List<Ability> abilities = new()
            {
                Ability.TripleBlood,
                SerpentsNest.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                StarSound.specialAbility
            };
            List<Tribe> tribes = new()
            {
                LobotomyCardManager.TribeDivine
            };
            List<Trait> traits = new()
            {

            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {

            };
            CardHelper.CreateCard(
                "wstl", "wstlcard", "Debug",
                "You shouldn't see this.",
                0, 99, 0, 0, 0,
                null, null,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: traits,
                evolveName: "Buggy {0}");
        }
    }
}
