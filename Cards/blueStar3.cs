using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BlueStar3_O0393()
        {
            List<Ability> abilities = new()
            {
                Assimilator.ability,
                Ability.AllStrike
            };

            WstlUtils.Add(
                "wstl_blueStar3", "Blue Star",
                "When this is over, let's meet again as stars.",
                4, 4, 4, 0,
                Resources.blueStar3,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(),
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}