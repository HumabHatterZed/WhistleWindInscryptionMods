using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void LaetitiaFriend_O0167()
        {
            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Insect
            };

            WstlUtils.Add(
                "wstl_laetitiaFriend", "Little Witch's Friend",
                "She brought her friends along.",
                2, 2, 1, 0,
                Resources.laetitiaFriend,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                tribes: tribes);
        }
    }
}