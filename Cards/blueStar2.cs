using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BlueStar2_O0393()
        {
            List<Ability> abilities = new()
            {
                Idol.ability,
                Ability.Evolve,
            };

            WstlUtils.Add(
                "wstl_blueStar2", "Blue Star",
                "When this is over, let's meet again as stars.",
                4, 3, 4, 0,
                Resources.blueStar2,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(),
                appearanceBehaviour: CardUtils.getRareAppearance,
                evolveId: new EvolveIdentifier("wstl_blueStar3", 1));
        }
    }
}