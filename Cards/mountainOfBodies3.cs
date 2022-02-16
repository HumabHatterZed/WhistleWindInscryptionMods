using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MountainOfBodies3_T0175()
        {
            List<Ability> abilities = new()
            {
                Assimilator.ability
            };

            WstlUtils.Add(
                "wstl_mountainOfBodies3", "The Mountain of Smiling Bodies",
                "A mass grave, melted and congealed into one eternally hungry beast.",
                4, 1, 4, 0,
                Resources.mountainOfBodies3,
                abilities: abilities, specialAbilities: new(),
                new List<Tribe>(),
                emissionTexture: Resources.mountainOfBodies3_emission,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}