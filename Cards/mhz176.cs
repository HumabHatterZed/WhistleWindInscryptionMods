using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MHz176_T0727()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.BuffNeighbours,
                Ability.BuffEnemy
            };

            WstlUtils.Add(
                "wstl_mhz176", "1.76 MHz",
                "This is a record, a record of a day we must never forget.",
                3, 0, 1, 0,
                Resources.mhz176,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}