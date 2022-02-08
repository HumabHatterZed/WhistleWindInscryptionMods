using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void Spiderling_O0243()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.Evolve
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Insect
            };

            WstlUtils.Add(
                "wstl_spiderling", "Spiderling",
                "Small and defenceless.",
                1, 0, 0, 0,
                Resources.spiderling,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, emissionTexture: Resources.spiderling_emission,
                evolveId: new EvolveIdentifier("wstl_spiderBrood", 1));
        }
    }
}