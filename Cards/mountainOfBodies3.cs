using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MountainOfBodies3_T0175()
        {
            List<Ability> abilities = new List<Ability>
            {
                Assimilator.ability
            };

            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                MountainOfBodies3.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_mountainOfBodies3", "The Mountain of Smiling Bodies",
                "A mass grave, melted and congealed into one eternally hungry beast.",
                1, 4, 0, 0,
                Resources.mountainOfBodies3,
                abilities: abilities, specialAbilities: specialAbilities,
                new List<Tribe>(),
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}