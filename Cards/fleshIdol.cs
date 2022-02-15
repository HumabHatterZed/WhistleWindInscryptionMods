using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void FleshIdol_T0979()
        {
            List<Ability> abilities = new()
            {
                GroupHealer.ability,
                Irritating.ability
            };

            WstlUtils.Add(
                "wstl_fleshIdol", "Flesh Idol",
                "This is a record, a record of a day we must never forget.",
                0, 4, 0, 6,
                Resources.fleshIdol,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}