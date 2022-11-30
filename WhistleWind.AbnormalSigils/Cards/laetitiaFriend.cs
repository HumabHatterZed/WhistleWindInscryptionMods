using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

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
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_laetitiaFriend", "Little Witch's Friend",
                "She brought her friends along.",
                atk: 2, hp: 2,
                blood: 0, bones: 4, energy: 0,
                Artwork.laetitiaFriend, Artwork.laetitiaFriend_emission,
                abilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}