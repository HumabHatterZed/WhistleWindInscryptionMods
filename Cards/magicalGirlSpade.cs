using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MagicalGirlSpade_O0173()
        {
            List<Ability> abilities = new()
            {
                Protector.ability
            };

            WstlUtils.Add(
                "wstl_magicalGirlSpade", "Magical Girl",
                "A loyal knight fighting to protect those close to her.",
                2, 4, 2, 0,
                Resources.magicalGirlSpade,
                abilities: abilities, new(),
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                appearanceBehaviour: CardUtils.getRareAppearance, onePerDeck: true);
        }
    }
}