using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void MountainOfBodies3_T0175()
        {
            List<Ability> abilities = new()
            {
                Assimilator.ability
            };

            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.RareCardBackground
            };
            WstlUtils.Add(
                "wstl_mountainOfBodies3", "The Mountain of Smiling Bodies",
                "A mass grave, melted and congealed into one eternally hungry beast.",
                4, 1, 4, 0,
                Resources.mountainOfBodies3, Resources.mountainOfBodies3_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances);
        }
    }
}