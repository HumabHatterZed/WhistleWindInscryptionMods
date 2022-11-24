﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MountainOfBodies2_T0175()
        {
            List<Ability> abilities = new()
            {
                Assimilator.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                Smile.specialAbility
            };
            CardHelper.CreateCard(
                "wstl_mountainOfBodies2", "The Mountain of Smiling Bodies",
                "A mass grave, melted and congealed into one eternally hungry beast.",
                atk: 3, hp: 1,
                blood: 3, bones: 0, energy: 0,
                Artwork.mountainOfBodies2, Artwork.mountainOfBodies2_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(),
                cardType: CardHelper.CardType.Rare, metaTypes: CardHelper.MetaType.NonChoice);
        }
    }
}