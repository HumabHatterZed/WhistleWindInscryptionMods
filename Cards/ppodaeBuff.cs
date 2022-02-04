using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void PpodaeBuff_D02107()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.DebuffEnemy
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Canine
            };

            WstlUtils.Add(
                "wstl_ppodaeBuff", "Ppodae",
                "",
                1, 3, 0, 0,
                Resources.ppodaeBuff,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, emissionTexture: Resources.ppodaeBuff_emission);
        }
    }
}