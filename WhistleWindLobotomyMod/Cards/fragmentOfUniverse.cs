using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void FragmentOfUniverse_O0360()
        {
            List<Ability> abilities = new()
            {
                Piercing.ability
            };
            CardHelper.CreateCard(
                "wstl_fragmentOfUniverse", "Fragment of the Universe",
                "You see a song in front of you. It's approaching, becoming more colourful by the second.",
                1, 2, 1, 0,
                Resources.fragmentOfUniverse, Resources.fragmentOfUniverse_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 2);
        }
    }
}