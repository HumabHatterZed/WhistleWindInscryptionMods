using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MeltingLoveMinion_D03109()
        {
            List<Ability> abilities = new()
            {
                Slime.ability
            };

            WstlUtils.Add(
                "wstl_meltingLoveMinion", "Slime",
                "Don't let your beasts get too close now.",
                0, 0, 0, 0,
                Resources.meltingLoveMinion,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(),
                emissionTexture: Resources.meltingLoveMinion_emission);
        }
    }
}