using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void QueenOfHatredExhausted_O0104()
        {
            List<SpecialAbilityIdentifier> specialAbilities = new()
            {
                _AbilityHelper.GetSpecialAbilityId
            };

            List<Tribe> tribes = new()
            {
                Tribe.Reptile
            };

            WstlUtils.Add(
                "wstl_queenOfHatredExhausted", "The Queen of Hatred",
                "Exhaustion: the cost of an all-out attack.",
                0, 2, 1, 0,
                Resources.queenOfHatredExhausted,
                new List<Ability>(), specialAbilities: specialAbilities,
                tribes: tribes,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}