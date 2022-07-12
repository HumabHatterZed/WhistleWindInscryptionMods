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
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                GiantTreeSap.specialAbility
            };
            WstlUtils.Add(
                "wstl_giantTreeSap", "Giant Tree Sap",
                "The sap of a tree at the end of the world. It is a potent healing agent.",
                0, 2, 0, 4,
                Resources.giantTreeSap, Resources.giantTreeSap_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}