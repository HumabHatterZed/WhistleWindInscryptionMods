using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void AllAroundHelper_T0541()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe,
                Ability.SplitStrike
            };

            WstlUtils.Add(
                "wstl_allAroundHelper", "All-Around Helper",
                "A machine built to help its owners with housework. It has a few bugs, unfortunately.",
                3, 1, 2, 0,
                Resources.allAroundHelper,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.allAroundHelper_emission);
        }
    }
}