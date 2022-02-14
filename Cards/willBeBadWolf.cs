using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void WillBeBadWolf_F0258()
        {
            List<Ability> abilities = new()
            {
                BitterEnemies.ability
            };

            List<Tribe> tribes = new()
            {
                Tribe.Canine
            };

            WstlUtils.Add(
                "wstl_willBeBadWolf", "Big and Will be Bad Wolf",
                "It is no coincidence that wolves are the villains of so many tales.",
                3, 2, 2, 0,
                Resources.willBeBadWolf,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}