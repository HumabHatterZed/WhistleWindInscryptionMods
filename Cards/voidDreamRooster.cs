using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void VoidDreamRooster_T0299()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.DebuffEnemy,
                Ability.Flying
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Hooved,
                Tribe.Bird
            };

            WstlUtils.Add(
                "wstl_voidDreamRooster", "Void Dream",
                "Quite the chimera.",
                2, 2, 2, 0,
                Resources.voidDreamRooster,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes);
        }
    }
}