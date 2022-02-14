using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void DreamingCurrent_T0271()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe,
                Ability.StrafePush
            };

            WstlUtils.Add(
                "wstl_dreamingCurrent", "The Dreaming Current",
                "A sickly child. Everyday it was fed candy that let it see the ocean.",
                3, 2, 2, 0,
                Resources.dreamingCurrent,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}