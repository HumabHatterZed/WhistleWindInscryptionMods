using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void DontTouchMe_O0547()
        {
            List<Ability> abilities = new List<Ability>
            {
                Punisher.ability,
                Ability.GuardDog
            };

            WstlUtils.Add(
                "wstl_dontTouchMe", "Don't Touch Me",
                "What happens when you press it?",
                2, 0, 1, 0,
                Resources.dontTouchMe,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.dontTouchMe_emission);
        }
    }
}