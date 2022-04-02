using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                Resources.magicalGirlSpade, Resources.magicalGirlSpade_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isRare: true, onePerDeck: true);
        }
    }
}