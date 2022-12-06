﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_FifthBrother_F0270()
        {
            List<Ability> abilities = new()
            {
                Burning.ability
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.TerrainLayout
            };
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_dreamOfABlackSwanBrother5", "Fifth Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.dreamOfABlackSwanBrother5,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances);
        }
    }
}