using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void OldLady_O0112()
        {
            List<Ability> abilities = new()
            {
                Ability.DebuffEnemy
            };

            WstlUtils.Add(
                "wstl_oldLady", "Old Lady",
                "An aged storyteller. She can tell you any tale, even those that can't exist.",
                1, 2, 0, 2,
                Resources.oldLady,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}