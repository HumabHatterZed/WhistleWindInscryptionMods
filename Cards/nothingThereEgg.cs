using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void NothingThereEgg_O0620()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<SpecialAbilityIdentifier> specialAbilities = new()
            {
                _AbilityHelper.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_nothingThereEgg", "An Egg",
                "What is it doing?",
                0, 4, 4, 0,
                Resources.nothingThereEgg,
                abilities: abilities, specialAbilities: specialAbilities,
                new List<Tribe>(),
                emissionTexture: Resources.nothingThereEgg_emission,
                appearanceBehaviour: CardUtils.getRareAppearance,
                evolveId: new EvolveIdentifier("wstl_nothingThereFinal", 1));
        }
    }
}