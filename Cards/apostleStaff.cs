using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ApostleStaff_T0346()
        {
            List<Ability> abilities = new()
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
                3, 6, 0, 0,
                Resources.apostleStaff,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits,
                emissionTexture: Resources.apostleStaff_emission);
        }
    }
}