using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void LaetitiaFriend_O0167()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };

            WstlUtils.Add(
                "wstl_laetitiaFriend", "Little Witch's Friend",
                "She brought her friends along.",
                2, 2, 0, 4,
                Resources.laetitiaFriend,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                tribes: tribes, emissionTexture: Resources.laetitiaFriend_emission);
        }
    }
}