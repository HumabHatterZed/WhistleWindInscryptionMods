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
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWind.AbnormalSigils;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void CARD_DEBUG()
        {
            List<Ability> abilities = new()
            {
                RightfulHeir.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {

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
                5, 10, 0, 0, 0,
                null, null,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: traits);
        }
    }
}
