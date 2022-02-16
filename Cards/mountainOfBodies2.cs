using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MountainOfBodies2_T0175()
        {
            List<Ability> abilities = new()
            {
                Assimilator.ability
            };

            WstlUtils.Add(
                "wstl_mountainOfBodies2", "The Mountain of Smiling Bodies",
                "A mass grave, melted and congealed into one eternally hungry beast.",
                3, 1, 3, 0,
                Resources.mountainOfBodies2,
                abilities: abilities, specialAbilities: new(),
                new List<Tribe>(),
                emissionTexture: Resources.mountainOfBodies2_emission,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}