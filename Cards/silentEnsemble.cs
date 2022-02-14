using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SilentEnsemble_T0131()
        {
            List<Ability> abilities = new()
            {
                Ability.BuffNeighbours
            };

            WstlUtils.Add(
                "wstl_silentEnsemble", "Chairs",
                "The conductor begins to direct the apocalypse.",
                0, 2, 0, 0,
                Resources.silentEnsemble,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>());
        }
    }
}