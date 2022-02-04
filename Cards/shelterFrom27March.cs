using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ShelterFrom27March_T0982()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.PreventAttack,
                Ability.Reach
            };

            WstlUtils.Add(
                "wstl_shelterFrom27March", "Shelter From the 27th of March",
                "A bomb shelter that protects its occupants from anything, even light.",
                1, 0, 0, 6,
                Resources.shelterFrom27March,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.shelterFrom27March_emission, onePerDeck: true);
        }
    }
}