using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ApostleScytheDown_T0346()
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
                "wstl_apostleScytheDown", "Scythe Apostle",
                "The time has come.",
                0, 6, 0, 0,
                Resources.apostleScytheDown,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits,
                emissionTexture: Resources.apostleScytheDown_emission);
        }
    }
}