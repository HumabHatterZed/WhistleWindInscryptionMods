using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MeatLantern_O0484()
        {
            List<Ability> abilities = new List<Ability>
            {
                Punisher.ability,
                Ability.Reach
            };

            WstlUtils.Add(
                "wstl_meatLantern", "Meat Lantern",
                "A beautiful flower attached to a mysterious creature.",
                3, 1, 1, 0,
                Resources.meatLantern,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}