using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BlueStar2_O0393()
        {
            List<Ability> abilities = new List<Ability>
            {
                Idol.ability,
                Ability.Evolve,
            };

            WstlUtils.Add(
                "wstl_blueStar2", "Blue Star",
                "When this is over, let's meet again as stars.",
                2, 0, 4, 0,
                Resources.blueStar2,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(),
                appearanceBehaviour: CardUtils.getRareAppearance,
                evolveId: new EvolveIdentifier("wstl_blueStar3", 1));
        }
    }
}