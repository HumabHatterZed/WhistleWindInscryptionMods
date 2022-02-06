using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void Porccubus_O0298()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.Deathtouch
            };

            WstlUtils.Add(
                "wstl_porccubus", "Porccubus",
                "A prick of its quills creates a deadly euphoria.",
                2, 1, 0, 6,
                Resources.porccubus,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}