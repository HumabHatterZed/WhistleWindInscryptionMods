using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void FourthBrother_F0270()
        {
            List<Ability> abilities = new()
            {
                Ability.Deathtouch
            };
            CardHelper.CreateCard(
                "wstl_dreamOfABlackSwanBrother4", "Fourth Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                0, 2, 1, 0,
                Resources.dreamOfABlackSwanBrother4, Resources.dreamOfABlackSwanBrother4_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}