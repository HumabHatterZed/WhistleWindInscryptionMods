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
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                _AbilityDialogueHelper.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_nothingThereEgg", "An Egg",
                "What is it doing?",
                4, 0, 0, 0,
                Resources.nothingThereEgg,
                abilities: abilities, specialAbilities: specialAbilities,
                new List<Tribe>(),
                emissionTexture: Resources.nothingThereEgg_emission,
                appearanceBehaviour: CardUtils.getTerrainBackgroundAppearance,
                evolveId: new EvolveIdentifier("wstl_nothingThereFinal", 1));
        }
    }
}