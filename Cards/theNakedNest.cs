using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void TheNakedNest_O0274()
        {
            List<Ability> abilities = new List<Ability>
            {
                SerpentsNest.ability
            };

            WstlUtils.Add(
                "wstl_theNakedNest", "The Naked Nest",
                "They can enter your body through any aperture.",
                2, 0, 0, 4,
                Resources.theNakedNest,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}