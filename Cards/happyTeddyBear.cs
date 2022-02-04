using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void HappyTeddyBear_T0406()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.DebuffEnemy
            };

            WstlUtils.Add(
                "wstl_happyTeddyBear", "Happy Teddy Bear",
                "Its memories began with a warm hug.",
                2, 3, 0, 8,
                Resources.happyTeddyBear,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}