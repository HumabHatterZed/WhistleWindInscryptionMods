using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void YouMustBeHappy_T0994()
        {
            List<Ability> abilities = new()
            {
                Scrambler.ability
            };

            WstlUtils.Add(
                "wstl_youMustBeHappy", "You Must be Happy",
                "Those that undergo the procedure find themselves rested and healthy again.",
                0, 1, 0, 2,
                Resources.youMustBeHappy,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.rudoltaSleigh_emission);
        }
    }
}