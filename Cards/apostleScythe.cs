using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void ApostleScythe_T0346()
        {
            List<Ability> abilities = new()
            {
                Woodcutter.ability,
                Apostle.ability
            };

            List<Trait> traits = new()
            {
                Trait.Uncuttable,
                Trait.Terrain
            };

            WstlUtils.Add(
                "wstl_apostleScythe", "Scythe Apostle",
                "The time has come.",
                3, 6, 0, 0,
                Resources.apostleScythe, Resources.apostleScythe_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits);
        }
    }
}