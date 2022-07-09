using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SixthBrother_F0270()
        {
            List<Ability> abilities = new()
            {
                Ability.DebuffEnemy
            };
            WstlUtils.Add(
                "wstl_dreamOfABlackSwanBrother6", "Sixth Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                0, 1, 1, 0,
                Resources.dreamOfABlackSwanBrother6, Resources.dreamOfABlackSwanBrother6_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new()
                );
        }
    }
}