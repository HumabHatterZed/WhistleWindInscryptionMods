﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_LuminousBracelet_O0995()
        {
            List<Ability> abilities = new()
            {
                GreedyHealing.ability,
                TargetGainSigils.ability
            };

            CardHelper.CreateCard(
                "wstl_luminousBracelet", "Luminous Bracelet",
                "A bracelet that will heal those nearby. It does not forgive the greedy.",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 3,
                Artwork.luminousBracelet, Artwork.luminousBracelet_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Teth,
                spellType: CardHelper.SpellType.TargetedSigils);
        }
    }
}