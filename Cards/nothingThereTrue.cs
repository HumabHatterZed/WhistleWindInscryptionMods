using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void NothingThereTrue_O0620()
        {
            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                SpecialEvolve.GetSpecialAbilityId
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Canine,
                Tribe.Hooved,
                Tribe.Reptile
            };
            WstlUtils.Add(
                "wstl_nothingThereTrue", "Nothing There",
                "What is that?",
                3, 3, 0, 0,
                Resources.nothingThereTrue,
                new List<Ability>(), specialAbilities: specialAbilities,
                tribes: tribes,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}