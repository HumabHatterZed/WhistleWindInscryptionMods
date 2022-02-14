using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SpiderBrood_O0243()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };

            WstlUtils.Add(
                "wstl_spiderBrood", "Spider Brood",
                "Big and mean.",
                1, 3, 0, 0,
                Resources.spiderBrood,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                tribes: tribes, emissionTexture: Resources.spiderBrood_emission);
        }
    }
}