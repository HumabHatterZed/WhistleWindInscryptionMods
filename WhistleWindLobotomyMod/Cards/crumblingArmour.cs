﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_CrumblingArmour_O0561()
        {
            List<Ability> abilities = new()
            {
                Courageous.ability
            };
            CardHelper.CreateCard(
                "wstl_crumblingArmour", "Crumbling Armour",
                "A suit of armour that rewards the brave and punishes the cowardly.",
                atk: 0, hp: 3,
                blood: 0, bones: 5, energy: 0,
                Artwork.crumblingArmour, Artwork.crumblingArmour_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}