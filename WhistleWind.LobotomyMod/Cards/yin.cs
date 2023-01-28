﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
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
                BlackFish.appearance
            };

            LobotomyCardHelper.CreateCard(
                "wstl_yin", "Yin",
                "A black pendant in search of its missing half.",
                atk: 2, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.yin, Artwork.yin_emission,
                altTexture: Artwork.yinAlt, emissionAltTexture: Artwork.yinAlt_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Waw);
        }
    }
}