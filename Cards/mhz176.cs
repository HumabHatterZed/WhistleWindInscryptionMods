using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MHz176_T0727()
        {
            List<Ability> abilities = new()
            {
                Ability.BuffNeighbours,
                Irritating.ability
            };

            WstlUtils.Add(
                "wstl_mhz176", "1.76 MHz",
                "This is a record, a record of a day we must never forget.",
                0, 3, 1, 0,
                Resources.mhz176,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.mhz176_emission);
        }
    }
}