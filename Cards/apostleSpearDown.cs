using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ApostleSpearDown_T0346()
        {
            List<Ability> abilities = new()
            {
                Ability.PreventAttack,
                Apostle.ability
            };

            List<Trait> traits = new()
            {
                Trait.Uncuttable,
                Trait.Pelt
            };

            WstlUtils.Add(
                "wstl_apostleSpearDown", "Spear Apostle",
                "The time has come.",
                0, 6, 0, 0,
                Resources.apostleSpearDown,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits,
                emissionTexture: Resources.apostleSpearDown_emission);
        }
    }
}