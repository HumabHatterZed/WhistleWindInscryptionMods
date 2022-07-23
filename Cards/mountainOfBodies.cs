using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void MountainOfBodies_T0175()
        {
            List<Ability> abilities = new()
            {
                Assimilator.ability
            };

            CardHelper.CreateCard(
                "wstl_mountainOfBodies", "The Mountain of Smiling Bodies",
                "A mass grave, melted and congealed into one eternally hungry beast.",
                2, 1, 2, 0,
                Resources.mountainOfBodies, Resources.mountainOfBodies_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isRare: true, riskLevel: 5);
        }
    }
}