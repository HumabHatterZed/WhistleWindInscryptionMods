using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void QueenOfHatredExhausted_O0104()
        {
            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                _AbilityHelper.GetSpecialAbilityId
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Reptile
            };

            WstlUtils.Add(
                "wstl_queenOfHatredExhausted", "The Queen of Hatred",
                "Exhaustion: the cost of an all-out attack.",
                2, 0, 1, 0,
                Resources.queenOfHatredExhausted,
                new List<Ability>(), specialAbilities: specialAbilities,
                tribes: tribes,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}