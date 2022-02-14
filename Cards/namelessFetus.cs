using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void NamelessFetus_O0115()
        {
            List<Ability> abilities = new()
            {
                Ability.TripleBlood,
                Ability.Sacrificial
            };

            List<SpecialAbilityIdentifier> specialAbilities = new()
            {
                NamelessFetus.GetSpecialAbilityId
            };
            List<Trait> traits = new()
            {
                Trait.Goat
            };
            WstlUtils.Add(
                "wstl_namelessFetus", "Nameless Fetus",
                "A neverending supply a blood. Just don't wake it.",
                0, 1, 0, 5,
                Resources.namelessFetus,
                abilities: abilities, specialAbilities,
                new List<Tribe>(), traits: traits, metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.namelessFetus_emission);
        }
    }
}