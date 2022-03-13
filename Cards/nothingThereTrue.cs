using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void NothingThereTrue_O0620()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<SpecialAbilityIdentifier> specialAbilities = new()
            {
                _AbilityHelper.GetSpecialAbilityId
            };
            List<Tribe> tribes = new()
            {
                Tribe.Canine,
                Tribe.Hooved,
                Tribe.Reptile
            };
            WstlUtils.Add(
                "wstl_nothingThereTrue", "Nothing There",
                "What is that?",
                3, 3, 2, 0,
                Resources.nothingThereTrue,
                abilities: abilities, specialAbilities: specialAbilities,
                tribes: tribes, emissionTexture: Resources.nothingThereTrue_emission,
                appearanceBehaviour: CardUtils.getRareAppearance,
                evolveId: new EvolveIdentifier("wstl_nothingThereEgg", 1));
        }
    }
}