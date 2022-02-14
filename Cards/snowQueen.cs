using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SnowQueen_F0137()
        {
            List<Ability> abilities = new()
            {
                FrostRuler.ability
            };

            WstlUtils.Add(
                "wstl_snowQueen", "The Snow Queen",
                "A queen from far away. Those who enter her palace never leave.",
                1, 2, 0, 6,
                Resources.snowQueen,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.snowQueen_emission);
        }
    }
}