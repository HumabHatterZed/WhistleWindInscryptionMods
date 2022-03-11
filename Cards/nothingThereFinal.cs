using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
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
                9, 9, 2, 0,
                Resources.nothingThereFinal,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), emissionTexture: Resources.nothingThereFinal_emission,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}