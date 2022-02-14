using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void CrumblingArmour_O0561()
        {
            List<Ability> abilities = new()
            {
                Courageous.ability
            };

            WstlUtils.Add(
                "wstl_crumblingArmour", "Crumbling Armour",
                "A suit of armour that rewards the brave.",
                0, 2, 0, 6,
                Resources.crumblingArmour,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}