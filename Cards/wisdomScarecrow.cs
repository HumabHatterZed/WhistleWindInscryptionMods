using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void WisdomScarecrow_F0187()
        {
            List<Ability> abilities = new List<Ability>
            {
                Bloodfiend.ability
            };

            WstlUtils.Add(
                "wstl_wisdomScarecrow", "Scarecrow Searching for Wisdom",
                "A hollow-headed scarecrow. Blood soaks its straw limbs.",
                2, 1, 0, 5,
                Resources.wisdomScarecrow,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.wisdomScarecrow_emission);
        }
    }
}