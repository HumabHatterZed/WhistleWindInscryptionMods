using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ApostleSpear_T0346()
        {
            List<Ability> abilities = new()
            {
                Piercing.ability,
                Apostle.ability
            };

            List<Trait> traits = new()
            {
                Trait.Uncuttable,
                Trait.Pelt
            };

            WstlUtils.Add(
                "wstl_apostleSpear", "Spear Apostle",
                "The time has come.",
                3, 6, 0, 0,
                Resources.apostleSpear,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits,
                emissionTexture: Resources.apostleSpear_emission);
        }
    }
}