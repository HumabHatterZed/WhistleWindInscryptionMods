using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_LaetitiaFriend_O0167()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            AbnormalCardHelper.CreateCard(
                "wstl_laetitiaFriend", "Little Witch's Friend",
                "She brought her friends along.",
                2, 2, 0, 4,
                Artwork.laetitiaFriend, Artwork.laetitiaFriend_emission,
                abilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}