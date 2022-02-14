using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void GraveOfBlossoms_O04100()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp,
                Bloodfiend.ability
            };

            WstlUtils.Add(
                "wstl_graveOfBlossoms", "Grave of Cherry Blossoms",
                "A deep sorrow, grown to obsession. Perhaps it's best to leave her be.",
                0, 2, 0, 2,
                Resources.graveOfBlossoms,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.graveOfBlossoms_emission);
        }
    }
}