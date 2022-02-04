using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SpiderBrood_O0243()
        {
            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Insect
            };

            WstlUtils.Add(
                "wstl_spiderBrood", "Spider Brood",
                "Big and mean.",
                2, 2, 0, 0,
                Resources.spiderBrood,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                tribes: tribes);
        }
    }
}