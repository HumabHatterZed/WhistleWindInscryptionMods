using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void NothingThereFinal_O0620()
        {
            WstlUtils.Add(
                "wstl_nothingThereFinal", "Nothing There",
                "A grotesque attempt at mimicry. Pray it does not improve its disguise.",
                9, 9, 0, 0,
                Resources.nothingThereFinal,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(),
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}