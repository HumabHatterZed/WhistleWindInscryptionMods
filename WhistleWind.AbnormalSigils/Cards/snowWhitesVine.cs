﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_SnowWhitesVine_F0442()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp
            };

            List<Tribe> tribes = new() { TribeBotanic };

            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_snowWhitesVine", "Thorny Vines",
                "A vine.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.snowWhitesVine, pixelTexture: Artwork.snowWhitesVine_pixel,
                abilities: abilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                metaTypes: CardHelper.CardMetaType.Terrain);
        }
    }
}