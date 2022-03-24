using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                Resources.weCanChangeAnything, Resources.weCanChangeAnything_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}