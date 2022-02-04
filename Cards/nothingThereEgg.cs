using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void NothingThereEgg_O0620()
        {
            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                NothingThereEgg.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_nothingThereEgg", "An Egg",
                "What is it doing?",
                5, 0, 0, 0,
                Resources.nothingThereEgg,
                new List<Ability>(), specialAbilities: specialAbilities,
                new List<Tribe>(),
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}