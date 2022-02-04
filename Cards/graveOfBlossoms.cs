using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void GraveOfBlossoms_O04100()
        {
            List<Ability> abilities = new List<Ability>
            {
                Bloodfiend.ability,
                Ability.Sharp
            };

            WstlUtils.Add(
                "wstl_graveOfBlossoms", "Grave of Cherry Blossoms",
                "A deep sorrow, grown to obsession. Perhaps it's best to leave her be.",
                2, 0, 1, 0,
                Resources.graveOfBlossoms,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}