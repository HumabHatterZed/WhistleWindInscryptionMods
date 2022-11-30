using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_SnowQueenIceHeart_F0137()
        {
            List<Ability> abilities = new()
            {
                FrozenHeart.ability
            };
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_snowQueenIceHeart", "Frozen Heart",
                "The palace was cold and lonely.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.snowQueenIceHeart, Artwork.snowQueenIceHeart_emission,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}