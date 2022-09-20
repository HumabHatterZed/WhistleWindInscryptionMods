using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void CrumblingArmour_O0561()
        {
            List<Ability> abilities = new()
            {
                Courageous.ability
            };

            CardHelper.CreateCard(
                "wstl_crumblingArmour", "Crumbling Armour",
                "A suit of armour that rewards the brave and punishes the cowardly.",
                0, 3, 0, 5,
                Resources.crumblingArmour, Resources.crumblingArmour_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 2);
        }
    }
}