using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ScorchedGirl_F0102()
        {
            List<Ability> abilities = new()
            {
                Volatile.ability
            };

            WstlUtils.Add(
                "wstl_scorchedGirl", "Scorched Girl",
                "Though there's nothing left to burn, the fire won't go out.",
                1, 1, 0, 3,
                Resources.scorchedGirl,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.scorchedGirl_emission);
        }
    }
}