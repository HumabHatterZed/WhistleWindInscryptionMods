using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void QueenOfHatred_O0104()
        {
            List<Ability> abilities = new()
            {
                Ability.Flying,
                Ability.Evolve
            };

            List<SpecialAbilityIdentifier> specialAbilities = new()
            {
                _AbilityHelper.GetSpecialAbilityId
            };

            List<Tribe> tribes = new()
            {
                Tribe.Reptile
            };

            WstlUtils.Add(
                "wstl_queenOfHatred", "The Queen of Hatred",
                "Heroes exist to fight evil. In its absence, they must create it.",
                7, 2, 1, 0,
                Resources.queenOfHatred,
                abilities: abilities, specialAbilities: specialAbilities,
                tribes: tribes,
                emissionTexture: Resources.queenOfHatred_emission,
                appearanceBehaviour: CardUtils.getRareAppearance,
                evolveId: new EvolveIdentifier("wstl_queenOfHatredExhausted", 1));
        }
    }
}