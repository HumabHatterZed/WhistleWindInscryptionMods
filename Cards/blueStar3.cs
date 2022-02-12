using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BlueStar3_O0393()
        {
            List<Ability> abilities = new List<Ability>
            {
                Assimilator.ability,
                Ability.AllStrike
            };

            WstlUtils.Add(
                "wstl_blueStar3", "Blue Star",
                "When this is over, let's meet again as stars.",
                3, 2, 4, 0,
                Resources.blueStar3,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(),
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}