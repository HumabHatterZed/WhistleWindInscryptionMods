using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void PpodaeBuff_D02107()
        {
            List<Ability> abilities = new()
            {
                Ability.DebuffEnemy
            };

            List<Tribe> tribes = new()
            {
                Tribe.Canine
            };

            WstlUtils.Add(
                "wstl_ppodaeBuff", "Ppodae",
                "",
                3, 2, 0, 8,
                Resources.ppodaeBuff,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, emissionTexture: Resources.ppodaeBuff_emission);
        }
    }
}