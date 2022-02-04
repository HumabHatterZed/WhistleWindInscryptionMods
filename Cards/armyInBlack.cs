using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ArmyInBlack_D01106()
        {
            List<Ability> abilities = new List<Ability>
            {
                Volatile.ability
            };

            WstlUtils.Add(
                "wstl_armyInBlack", "Army in Black",
                "Duty-bound.",
                2, 2, 2, 0,
                Resources.armyInBlack,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(),
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}