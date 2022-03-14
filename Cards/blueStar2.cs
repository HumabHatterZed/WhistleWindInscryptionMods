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
                Assimilator.ability,
                Ability.AllStrike
            };

            WstlUtils.Add(
                "wstl_blueStar2", "Blue Star",
                "When this is over, let's meet again as stars.",
                4, 4, 4, 0,
                Resources.blueStar,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(),
                emissionTexture: Resources.blueStar_emission,
                appearanceBehaviour: CardUtils.getRareAppearance, onePerDeck: true);
        }
    }
}