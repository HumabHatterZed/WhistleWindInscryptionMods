using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void WeCanChangeAnything_T0985()
        {
            List<Ability> abilities = new()
            {
                Grinder.ability
            };

            WstlUtils.Add(
                "wstl_weCanChangeAnything", "We Can Change Anything",
                "Whatever you're dissatisfied with, this machine will fix it. You just have to step inside.",
                0, 1, 1, 0,
                Resources.weCanChangeAnything,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.weCanChangeAnything_emission);
        }
    }
}