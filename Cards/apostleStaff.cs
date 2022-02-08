using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ApostleStaff_T0346()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.Sniper,
                Apostle.ability
            };

            List<Trait> traits = new()
            {
                Trait.Uncuttable,
                Trait.Pelt
            };

            WstlUtils.Add(
                "wstl_apostleStaff", "Staff Apostle",
                "The time has come.",
                6, 3, 0, 0,
                Resources.apostleStaff,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits,
                emissionTexture: Resources.apostleStaff_emission);
        }
    }
}