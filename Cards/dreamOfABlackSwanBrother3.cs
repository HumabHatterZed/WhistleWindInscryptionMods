using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void ThirdBrother_F0270()
        {
            List<Ability> abilities = new()
            {
                Reflector.ability
            };

            WstlUtils.Add(
                "wstl_dreamOfABlackSwanBrother3", "Third Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                0, 3, 1, 0,
                Resources.dreamOfABlackSwanBrother3, Resources.dreamOfABlackSwanBrother3_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new()
                );
        }
    }
}