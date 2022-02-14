using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void WallLady_F0118()
        {
            List<Ability> abilities = new()
            {
                Punisher.ability
            };

            WstlUtils.Add(
                "wstl_wallLady", "The Lady Facing the Wall",
                "A deep sorrow, grown to obsession. Perhaps it's best to leave her be.",
                0, 2, 0, 4,
                Resources.wallLady,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}