using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MeatLantern_O0484()
        {
            List<Ability> abilities = new()
            {
                Punisher.ability,
                Ability.Reach
            };

            WstlUtils.Add(
                "wstl_meatLantern", "Meat Lantern",
                "A beautiful flower attached to a mysterious creature.",
                1, 2, 2, 0,
                Resources.meatLantern,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.meatLantern_emission);
        }
    }
}