﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_GiantTreeSap_T0980()
        {
            List<Ability> abilities = new()
            {
                Ability.Morsel,
                Ability.Sacrificial
            };
            List<Tribe> tribes = new() { TribeBotanic };

            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                Sap.specialAbility
            };
            CreateCard(
                "wstl_giantTreeSap", "Giant Tree Sap",
                "Sap from a tree at the end of the world. It is a potent healing agent.",
                atk: 0, hp: 2,
                blood: 0, bones: 2, energy: 0,
                Artwork.giantTreeSap, Artwork.giantTreeSap_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: RiskLevel.He,
                evolveName: "[name]Giant Elder Tree Sap");
        }
    }
}