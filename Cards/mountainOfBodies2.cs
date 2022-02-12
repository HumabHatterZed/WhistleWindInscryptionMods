using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MountainOfBodies2_T0175()
        {
            List<Ability> abilities = new List<Ability>
            {
                Assimilator.ability
            };

            WstlUtils.Add(
                "wstl_mountainOfBodies2", "The Mountain of Smiling Bodies",
                "A mass grave, melted and congealed into one eternally hungry beast.",
                1, 3, 3, 0,
                Resources.mountainOfBodies2,
                abilities: abilities, specialAbilities: new(),
                new List<Tribe>(),
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}