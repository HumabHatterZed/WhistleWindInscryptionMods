using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ParasiteTreeSapling_D04108()
        {
            WstlUtils.Add(
                "wstl_parasiteTreeSapling", "Sapling",
                "They proliferate and become whole. Can you feel it?",
                1, 2, 0, 0,
                Resources.parasiteTreeSapling,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                new List<Tribe>());
        }
    }
}