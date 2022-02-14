using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ApocalypseBird_O0263()
        {
            List<Ability> abilities = new()
            {
                Ability.AllStrike
            };

            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };

            WstlUtils.Add(
                "wstl_apocalypseBird", "Apocalypse Bird",
                "There was no moon, no stars. Just a bird, alone in the Black Forest.",
                10, 3, 4, 0,
                Resources.oneSin,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes,
                appearanceBehaviour: CardUtils.getRareAppearance, onePerDeck: true);
        }
    }
}