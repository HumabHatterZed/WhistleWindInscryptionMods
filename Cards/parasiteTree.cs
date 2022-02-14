using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ParasiteTree_D04108()
        {
            List<Ability> abilities = new()
            {
                Gardener.ability
            };

            WstlUtils.Add(
                "wstl_parasiteTree", "Parasite Tree",
                "A beautiful tree. It wants only to help you and your beasts.",
                0, 3, 2, 0,
                Resources.parasiteTree,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}