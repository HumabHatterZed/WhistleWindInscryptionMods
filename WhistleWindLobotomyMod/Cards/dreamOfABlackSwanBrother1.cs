using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void FirstBrother_F0270()
        {
            List<Ability> abilities = new()
            {
                Ability.DoubleStrike
            };
            CardHelper.CreateCard(
                "wstl_dreamOfABlackSwanBrother1", "First Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                0, 1, 1, 0,
                Resources.dreamOfABlackSwanBrother1, Resources.dreamOfABlackSwanBrother1_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}