using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
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
                4, 2, 2, 0,
                Resources.magicalGirlSpade,
                abilities: abilities, new(),
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                appearanceBehaviour: CardUtils.getRareAppearance, onePerDeck: true);
        }
    }
}