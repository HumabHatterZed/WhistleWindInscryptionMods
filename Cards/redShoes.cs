using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void RedShoes_O0408()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.Sharp,
                Ability.GuardDog
            };

            WstlUtils.Add(
                "wstl_redShoes", "Red Shoes",
                "How pretty. Maybe they'll fit.",
                3, 0, 1, 0,
                Resources.redShoes,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.redShoes_emission);
        }
    }
}