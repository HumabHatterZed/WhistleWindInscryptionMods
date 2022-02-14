using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void FuneralOfButterflies_T0168()
        {
            List<Ability> abilities = new()
            {
                Ability.SplitStrike
            };

            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };

            WstlUtils.Add(
                "wstl_funeralOfButterflies", "Funeral of the Dead Butterflies",
                "The coffin is a tribute to the fallen. A memorial to those who can't return home.",
                2, 2, 2, 0,
                Resources.funeralOfButterflies,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.Rare,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}