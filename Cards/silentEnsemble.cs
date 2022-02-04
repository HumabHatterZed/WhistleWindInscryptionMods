using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SilentEnsemble_T0131()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.BuffNeighbours
            };

            WstlUtils.Add(
                "wstl_silentEnsemble", "Chairs",
                "The conductor begins to direct the apocalypse.",
                1, 1, 0, 0,
                Resources.silentEnsemble,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>());
        }
    }
}