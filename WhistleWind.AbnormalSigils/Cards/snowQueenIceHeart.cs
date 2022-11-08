using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

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
            AbnormalCardHelper.CreateCard(
                "wstl_snowQueenIceHeart", "Frozen Heart",
                "The palace was cold and lonely.",
                0, 1, 0, 0,
                Artwork.snowQueenIceHeart, Artwork.snowQueenIceHeart_emission,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}