using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void WillBeBadWolf_F0258()
        {
            List<Ability> abilities = new List<Ability>
            {
                BitterEnemies.ability
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Canine
            };

            WstlUtils.Add(
                "wstl_willBeBadWolf", "Big and Will be Bad Wolf",
                "It is no coincidence that wolves are the villains of so many tales.",
                2, 3, 2, 0,
                Resources.willBeBadWolf,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}