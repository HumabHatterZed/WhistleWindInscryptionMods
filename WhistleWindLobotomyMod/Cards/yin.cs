﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Yin_O05102()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe,
                Ability.Submerge
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                AlternateBattlePortrait.appearance
            };

            CreateCard(
                "wstl_yin", "Yin",
                "A black pendant in search of its missing half.",
                atk: 2, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.yin, Artwork.yin_emission,
                altTexture: Artwork.yinAlt, emissionAltTexture: Artwork.yinAlt_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw);
        }
    }
}