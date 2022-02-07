using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MeltingLoveSlime_D03109()
        {
            List<Ability> abilities = new List<Ability>
            {
                Slime.ability
            };

            WstlUtils.Add(
                "wstl_meltingLoveMinion", "Slime",
                "Don't let your beasts get too close now.",
                1, 1, 0, 0,
                Resources.meltingLoveMinion,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(),
                emissionTexture: Resources.rudoltaSleigh_emission);
        }
    }
}