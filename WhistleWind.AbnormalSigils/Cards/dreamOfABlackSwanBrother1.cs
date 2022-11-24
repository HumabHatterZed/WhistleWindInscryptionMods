﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_FirstBrother_F0270()
        {
            List<Ability> abilities = new()
            {
                Ability.DoubleStrike
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.TerrainLayout
            };

            AbnormalCardHelper.CreateCard(
                "wstl_dreamOfABlackSwanBrother1", "First Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                0, 1, 1, 0,
                Artwork.dreamOfABlackSwanBrother1,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances);
        }
    }
}