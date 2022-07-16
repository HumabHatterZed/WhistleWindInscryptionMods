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
            CardHelper.CreateCard(
                "wstl_giantTreeSap", "Giant Tree Sap",
                "Sap from a tree at the end of the world. It is a potent healing agent.",
                0, 3, 0, 4,
                Resources.giantTreeSap, Resources.giantTreeSap_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 3);
        }
    }
}