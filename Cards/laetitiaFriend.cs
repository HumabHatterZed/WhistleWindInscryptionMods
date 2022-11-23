using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void LaetitiaFriend_O0167()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };

            CardHelper.CreateCard(
                "wstl_laetitiaFriend", "Little Witch's Friend",
                "She brought her friends along.",
                2, 2, 0, 4,
                Resources.laetitiaFriend, Resources.laetitiaFriend_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}