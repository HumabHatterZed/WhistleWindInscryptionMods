using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void Porccubus_O0298()
        {
            List<Ability> abilities = new()
            {
                Ability.Deathtouch
            };

            WstlUtils.Add(
                "wstl_porccubus", "Porccubus",
                "A prick of its quills creates a deadly euphoria.",
                1, 2, 0, 5,
                Resources.porccubus,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}