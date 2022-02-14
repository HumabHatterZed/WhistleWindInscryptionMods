using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void HappyTeddyBear_T0406()
        {
            List<Ability> abilities = new()
            {
                Ability.DebuffEnemy
            };

            WstlUtils.Add(
                "wstl_happyTeddyBear", "Happy Teddy Bear",
                "Its memories began with a warm hug.",
                3, 2, 0, 8,
                Resources.happyTeddyBear,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}