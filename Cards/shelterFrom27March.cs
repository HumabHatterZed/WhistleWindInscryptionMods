using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ShelterFrom27March_T0982()
        {
            List<Ability> abilities = new()
            {
                Ability.PreventAttack,
                Aggravating.ability
            };

            WstlUtils.Add(
                "wstl_shelterFrom27March", "Shelter From the 27th of March",
                "It makes itself the safest place in the world by altering the reality around it.",
                0, 1, 0, 3,
                Resources.shelterFrom27March,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.shelterFrom27March_emission);
        }
    }
}