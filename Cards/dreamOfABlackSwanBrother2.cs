using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SecondBrother_F0270()
        {
            CardHelper.CreateCard(
                "wstl_dreamOfABlackSwanBrother2", "Second Brother",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                0, 1, 1, 0,
                Resources.dreamOfABlackSwanBrother2, Resources.dreamOfABlackSwanBrother2_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}