using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void GiantTreeSap_T0980()
        {
            List<Ability> abilities = new()
            {
                Ability.Morsel,
                Ability.Sacrificial
            };

            WstlUtils.Add(
                "wstl_giantTreeSap", "Giant Tree Sap",
                "An aged storyteller. She can tell you any tale, even those that can't exist.",
                1, 2, 0, 2,
                Resources.giantTreeSap, Resources.giantTreeSap_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}