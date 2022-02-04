using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void CrumblingArmour_O0561()
        {
            List<Ability> abilities = new List<Ability>
            {
                Courageous.ability
            };
            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                CrumblingArmour.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_crumblingArmour", "Crumbling Armour",
                "A suit of armour that rewards the brave.",
                2, 0, 0, 6,
                Resources.crumblingArmour,
                abilities: abilities, specialAbilities: specialAbilities,
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}