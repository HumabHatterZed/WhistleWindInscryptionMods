﻿using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_JesterOfNihil_O01118()
        {
            List<Ability> abilities = new()
            {
                Nihil.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                EventCard.specialAbility
            };
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.RuinaCard,
                CardHelper.MetaType.EventCard
            };

            CardHelper.CreateCard(
                "wstl_jesterOfNihil", "The Jester of Nihil",
                "",
                0, 15, 4, 0,
                Artwork.jesterOfNihil, Artwork.jesterOfNihil_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Rare, metaTypes: metaTypes);
        }
    }
}