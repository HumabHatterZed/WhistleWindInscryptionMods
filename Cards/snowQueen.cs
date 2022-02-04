using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SnowQueen_F0137()
        {
            List<Ability> abilities = new List<Ability>
            {
                FrostRuler.ability
            };

            WstlUtils.Add(
                "wstl_snowQueen", "The Snow Queen",
                "A queen from far away. Those who enter her palace never leave.",
                1, 2, 0, 5,
                Resources.snowQueen,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}