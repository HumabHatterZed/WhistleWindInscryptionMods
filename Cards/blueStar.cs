using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BlueStar_O0393()
        {
            List<Ability> abilities = new List<Ability>
            {
                Idol.ability,
                Ability.Evolve
            };

            WstlUtils.Add(
                "wstl_blueStar", "Blue Star",
                "When this is over, let's meet again as stars.",
                1, 0, 4, 0,
                Resources.blueStar,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                appearanceBehaviour: CardUtils.getRareAppearance,
                evolveId: new EvolveIdentifier("wstl_blueStar2", 1), onePerDeck: true);
        }
    }
}