using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                1, 3, 2, 0,
                Resources.allAroundHelper, Resources.allAroundHelper_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}