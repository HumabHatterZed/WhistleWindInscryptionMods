using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void NamelessFetus_O0115()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.TripleBlood,
                Ability.Sacrificial
            };

            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                NamelessFetus.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_namelessFetus", "Nameless Fetus",
                "A neverending supply a blood. Just don't wake it.",
                1, 0, 0, 5,
                Resources.namelessFetus,
                abilities: abilities, specialAbilities,
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.namelessFetus_emission);
        }
    }
}