using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ApostleSpearDown_T0346()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.PreventAttack,
                Ability.Evolve
            };

            List<Trait> traits = new()
            {
                Trait.Uncuttable,
                Trait.Pelt
            };

            WstlUtils.Add(
                "wstl_apostleSpearDown", "Apostle",
                "The time has come.",
                6, 0, 0, 0,
                Resources.apostleSpearDown,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits,
                evolveId: new EvolveIdentifier("wstl_apostleSpear", 2));
        }
    }
}