using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                Resources.apostleScytheDown, Resources.apostleScytheDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits);
        }
    }
}